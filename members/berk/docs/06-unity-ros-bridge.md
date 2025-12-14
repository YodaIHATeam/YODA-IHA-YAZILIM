# Unity – ROS 2 Bağlantısı (ROS-TCP Connector + ROS-TCP Endpoint)

**Sorumlu:** [@WorldOfBerk](https://github.com/WorldOfBerk)		
**Tarih:** 24-10-2025		
**Konu:** Unity ile ROS 2 Humble arasında TCP tabanlı çift yönlü iletişim kurulumu

-----------------------------------------------------
## AMAÇ
Bu doküman Unity sahnesi ile ROS 2 (Humble) arasında TCP üzerinden çift yönlü veri iletişimi sağlamak için:
- Unity tarafında: ROS-TCP Connector (Unity package)
- ROS 2 tarafında: ROS-TCP Endpoint (ros_tcp_endpoint node)
kurulum + test adımlarını içerir.

Bu yapı ile:
- Unity -> ROS 2 (publish)
- ROS 2 -> Unity (subscribe)
iletişimi kurulabilir.

-----------------------------------------------------

## 1) ROS 2 HUMBLE (DEB) KURULUMU (ÖZET)
(ROS 2 zaten kuruluysa bu kısmı atla)

1. Universe repo:
```
$ sudo apt install software-properties-common -y
$ sudo add-apt-repository universe
```
2. ROS 2 apt source:
```
$ sudo apt update && sudo apt install curl -y
$ export ROS_APT_SOURCE_VERSION=$(curl -s https://api.github.com/repos/ros-infrastructure/ros-apt-source/releases/latest | grep -F "tag_name" | awk -F\" '{print $4}')
$ curl -L -o /tmp/ros2-apt-source.deb "https://github.com/ros-infrastructure/ros-apt-source/releases/download/${ROS_APT_SOURCE_VERSION}/ros2-apt-source_${ROS_APT_SOURCE_VERSION}.$(. /etc/os-release && echo ${UBUNTU_CODENAME:-${VERSION_CODENAME}})_all.deb"
$ sudo dpkg -i /tmp/ros2-apt-source.deb
```
3. ROS 2 Desktop:
```
$ sudo apt update
$ sudo apt install ros-humble-desktop -y
```
4. Terminal açınca otomatik tanısın:
```
$ echo "source /opt/ros/humble/setup.bash" >> ~/.bashrc
$ source ~/.bashrc
```
Kontrol:
```
$ ros2 --version
$ ros2 topic list
```
-----------------------------------------------------
## 2) ROS-TCP ENDPOINT (ROS 2 TARAFI) KURULUMU
Bu paket Unity ile ROS 2 arasında TCP köprüsünü sağlar.

1) Workspace oluştur:
```
$ mkdir -p ~/ros2_ws/src
$ cd ~/ros2_ws/src
```
2) Repo’yu klonla (ROS 2 branch):
```
$ git clone https://github.com/Unity-Technologies/ROS-TCP-Endpoint.git
$ cd ROS-TCP-Endpoint
$ git fetch --all
$ git checkout main-ros2
```
3) Bağımlılık + derleme:
```
$ cd ~/ros2_ws
$ sudo apt install python3-colcon-common-extensions python3-rosdep -y
$ sudo rosdep init 2>/dev/null || true
$ rosdep update
$ rosdep install --from-paths src -i -r -y
$ colcon build
```
4) Workspace’i her terminalde otomatik yükle:
```
$ echo "source ~/ros2_ws/install/setup.bash" >> ~/.bashrc
$ source ~/.bashrc
```
-----------------------------------------------------
## 3) ROS-TCP ENDPOINT ÇALIŞTIRMA
Bu terminal Unity açıkken çalışır durumda kalmalı.
```
$ ros2 run ros_tcp_endpoint default_server_endpoint
```
Beklenen çıktı:
[INFO] [UnityEndpoint]: Starting server on 0.0.0.0:10000

-----------------------------------------------------
## 4) UNITY TARAFI (ROS-TCP CONNECTOR) KURULUMU
1) Unity Hub -> Unity Editor: 2022.3 LTS önerilir
2) Unity projesi: 3D (Built-in) ile başlamak yeterli

3) ROS-TCP Connector import:
- Unity Package Manager üzerinden “Add package from git URL” kullanılabilir
- Alternatif: Unity Robotics “ROS-TCP-Connector” package’ını indirip import edin

4) Scene’e bir boş GameObject ekle: “ROS”
- Üzerine “ROSConnection” component ekle
- ROS IP: 127.0.0.1 (aynı bilgisayarda ise)
- Port: 10000
- Play Mode’da bağlanınca ROS terminalinde “Connection from 127.0.0.1” görülür

Not: Unity'de üst menüde Robotics isimli bir kısım belirecek buraya tıklanarak ROS 2 seçilmeli.

-----------------------------------------------------
## 5) HIZLI TEST (UNITY -> ROS 2)
A) ROS endpoint terminali açık kalsın:
```
$ ros2 run ros_tcp_endpoint default_server_endpoint
```
B) Unity’de örnek publisher script çalıştır (chatter gibi)

```csharp
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class ChatterPublisher : MonoBehaviour
{
    ROSConnection ros;
    float timer;

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<StringMsg>("/chatter");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            timer = 0f;
            ros.Publish("/chatter", new StringMsg("hello from Unity"));
        }
    }
}
```

C) Başka terminalde ROS tarafında dinle:
```
$ ros2 topic list | grep chatter
$ ros2 topic echo /chatter
```
Beklenen:
data: hello from Unity ...
---

-----------------------------------------------------
## 6) HIZLI TEST (ROS 2 -> UNITY)
A) Unity’de bir subscriber örneği (örn. /cmd_vel veya /hello_control) çalışsın
```csharp
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class ChatterSubscriber : MonoBehaviour
{
    void Start()
    {
        ROSConnection.GetOrCreateInstance()
            .Subscribe<StringMsg>("/hello", msg =>
            {
                Debug.Log("From ROS: " + msg.data);
            });
    }
}
```
B) ROS tarafında publish et (Twist örneği - doğru YAML formatı):
```
$ ros2 topic pub /uav/cmd_vel geometry_msgs/Twist "{linear: {x: 0.0, y: 0.0, z: 1.0}, angular: {x: 0.0, y: 0.0, z: 0.3}}" -r 20
```
Not: “z:1.0” yazarken boşluk/format bozulursa hata verir. Doğru format: "z: 1.0"

-----------------------------------------------------
## SIK HATALAR / ÇÖZÜMLER
1) “E: Unable to locate package ros-humble-ros-tcp-endpoint”
- Bu paket apt’ta yok; kaynak koddan (git clone + colcon build) kurulur.

2) “file 'endpoint.launch.py' was not found…”
- ROS 2 branch/versiyonda launch dosyası olmayabilir.
- Çalıştırma yöntemi:
  $ ros2 run ros_tcp_endpoint default_server_endpoint

3) “sequence size exceeds remaining buffer / No more data available”
- Unity play-stop veya sahne reload olduğunda bağlantı kopabilir.
- Endpoint’i yeniden çalıştır:
  Ctrl+C -> tekrar:
  $ ros2 run ros_tcp_endpoint default_server_endpoint

4) Unity’de ikinci monitör / ekran siyah vb.
- NVIDIA driver / Wayland-Xorg kaynaklı olabilir.
- Gerekirse Xorg ile giriş yaparak test edin.

-----------------------------------------------------
## SONUÇ
- ROS 2 Humble kuruldu ve terminale eklendi
- ROS-TCP Endpoint source’tan kuruldu ve 0.0.0.0:10000 üzerinde çalışıyor
- Unity ROSConnection ile endpoint’e bağlandı
- Unity -> ROS 2 ve ROS 2 -> Unity temel mesaj akışı doğrulandı

-----------------------------------------------------
## REFERANSLAR
- ROS 2 Humble Deb Install: https://docs.ros.org/en/humble/Installation/Ubuntu-Install-Debs.html
- ROS-TCP-Connector: https://github.com/Unity-Technologies/ROS-TCP-Connector
- ROS-TCP Endpoint: https://github.com/Unity-Technologies/ROS-TCP-Endpoint
- Unity Hub Linux: https://docs.unity3d.com/hub/manual/InstallHub.html#install-hub-linux

