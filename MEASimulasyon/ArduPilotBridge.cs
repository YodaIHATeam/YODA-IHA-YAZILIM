using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

// ArduPilot'un Beklediği Veri Yapıları
[Serializable]
public class ArduPilotInput
{
    public double timestamp;
    public double[] position;   // x, y, z (NED)
    public double[] velocity;   // vx, vy, vz (NED)
    public double[] attitude;   // roll, pitch, yaw (Rad)
    public ImuData imu;
}

[Serializable]
public class ImuData
{
    public double[] gyro;       // x, y, z (Rad/s)
    public double[] accel_body; // x, y, z (m/s^2)
}

public class ArduPilotBridge : MonoBehaviour
{
    [Header("Ayarlar")]
    public int listenPort = 9002;
    public float motorThrustMultiplier = 15.0f; // Motor gücü çarpanı (Drone ağırlığına göre ayarlayın)

    [Header("Motorlar (Sadece İzleme)")]
    public float[] motorValues = new float[4]; // ArduPilot'tan gelen 0.0 - 1.0 arası değerler

    // Ağ Değişkenleri
    UdpClient udpServer;
    IPEndPoint remoteEP;
    IPEndPoint activeClientEP; // Bize veri gönderen ArduPilot adresi
    bool hasConnection = false;

    // Fizik
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Fiziğin sapıtmaması için Unity zamanını SITL hızına (400Hz) yaklaştırıyoruz
        Time.fixedDeltaTime = 0.0025f; 

        StartUDPServer();
    }

    void StartUDPServer()
    {
        try
        {
            udpServer = new UdpClient(listenPort);
            remoteEP = new IPEndPoint(IPAddress.Any, 0);
            udpServer.BeginReceive(new AsyncCallback(ReceiveCallback), null);
            Debug.Log($"<color=green>UÇUŞ SİSTEMİ HAZIR:</color> Port {listenPort} dinleniyor.");
        }
        catch (Exception e)
        {
            Debug.LogError("UDP Başlatılamadı: " + e.Message);
        }
    }

    // ARKA PLAN (VERİ ALMA)
    void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            byte[] receivedBytes = udpServer.EndReceive(ar, ref remoteEP);
            activeClientEP = remoteEP; // ArduPilot'un adresini kaydet
            hasConnection = true;

            // ArduPilot'tan gelen Binary Motor Verisini Çöz (Parse)
            // Veri Yapısı: Magic(2) + FrameRate(2) + FrameCount(4) + PWM(16*2)
            if (receivedBytes.Length >= 36)
            {
                ushort magic = BitConverter.ToUInt16(receivedBytes, 0);
                // Magic Number 18458 (0x481A) ise veri geçerlidir
                if (magic == 18458) 
                {
                    // İlk 4 motoru al
                    for (int i = 0; i < 4; i++)
                    {
                        // Byte offset 8'den başlar. Her motor 2 byte (ushort)
                        ushort pwmVal = BitConverter.ToUInt16(receivedBytes, 8 + (i * 2));
                        
                        // PWM (1000-2000) -> 0.0 - 1.0 arasına normalize et
                        // Unity main thread'de kullanabilsin diye diziye atıyoruz
                        motorValues[i] = Mathf.Clamp01((pwmVal - 1000f) / 1000f);
                    }
                }
            }

            // Dinlemeye devam et
            udpServer.BeginReceive(new AsyncCallback(ReceiveCallback), null);
        }
        catch
        {
            // Hata olsa da devam et
             if (udpServer != null) udpServer.BeginReceive(new AsyncCallback(ReceiveCallback), null);
        }
    }

    // ANA DÖNGÜ (FİZİK GÖNDERME & MOTOR UYGULAMA)
    void FixedUpdate()
    {
        if (hasConnection && activeClientEP != null)
        {
            // 1. ArduPilot'a Mevcut Durumu Gönder
            SendPhysicsState();

            // 2. ArduPilot'tan Gelen Motor Güçlerini Uygula
            ApplyMotorForces();
        }
    }

    void SendPhysicsState()
    {
        // Unity (Sol El) -> ArduPilot (NED - Sağ El) Dönüşümü
        Vector3 pos = rb.position;
        Vector3 vel = rb.linearVelocity;
        Vector3 angVel = rb.angularVelocity;
        Quaternion rot = rb.rotation;
        Vector3 euler = rot.eulerAngles;

        // Pozisyon: Unity(x,y,z) -> NED(z, x, -y)
        // NOT: Unity'de Z ileri ise böyledir. Projenize göre X ve Z yer değiştirebilir.
        double[] posNED = new double[] { pos.z, pos.x, -pos.y };
        double[] velNED = new double[] { vel.z, vel.x, -vel.y };

        // Açı (Radyan)
        double[] attNED = new double[] {
            rot.eulerAngles.x * Mathf.Deg2Rad, 
            rot.eulerAngles.z * Mathf.Deg2Rad, 
            -rot.eulerAngles.y * Mathf.Deg2Rad 
        };

        // İvme (Yerçekimsiz saf ivme)
        Vector3 localAccel = transform.InverseTransformDirection(Physics.gravity) * -1;

        ArduPilotInput input = new ArduPilotInput();
        input.timestamp = Time.time;
        input.position = posNED;
        input.velocity = velNED;
        input.attitude = attNED;
        input.imu = new ImuData() {
            gyro = new double[] { angVel.x, angVel.z, -angVel.y },
            accel_body = new double[] { 0, 0, -9.81 } // Geçici olarak basit yerçekimi
        };

        string json = JsonUtility.ToJson(input) + "\n";
        byte[] bytes = Encoding.ASCII.GetBytes(json);
        
        try {
            udpServer.Send(bytes, bytes.Length, activeClientEP);
        } catch {}
    }

    void ApplyMotorForces()
    {
        // Basit Dikey Kuvvet Uygulama (Geliştirilebilir)
        // Motor 1, 2, 3, 4'ten gelen gücün toplamını yukarı doğru itme gücü yapıyoruz.
        // Gerçek simülasyonda her motor pervanesinin kendi konumunda kuvvet uygulaması gerekir.
        
        float totalThrottle = 0;
        for(int i=0; i<4; i++) totalThrottle += motorValues[i];
        
        // Ortalama güç
        float averagePower = totalThrottle / 4.0f;

        // Yukarı itme kuvveti (Local Up)
        // Eğer drone ters dönerse yere çakılsın diye "transform.up" kullanıyoruz.
        Vector3 force = transform.up * averagePower * motorThrustMultiplier * 9.81f; // 9.81 yerçekimini yenmek için
        
        rb.AddForce(force);
    }

    void OnApplicationQuit()
    {
        if (udpServer != null) udpServer.Close();
    }
}