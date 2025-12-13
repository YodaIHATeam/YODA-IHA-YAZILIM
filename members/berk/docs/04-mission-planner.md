# Mission Planner Kurulumu ve SITL Bağlantısı

- Sorumlu: WorldOfBerk
- Tarih: 30-11-2025
- Konu: Mission Planner kurulumu, SITL bağlantısı, UDP port doğrulaması

-----------------------------------------------------

#### AMAÇ
Bu belge Mission Planner arayüzünün Linux ortamında çalıştırılması, SITL telemetri bağlantısının
yapılması ve kontrol arayüzü üzerinden uçuş komutlarının doğrulanması için hazırlanmıştır.

-----------------------------------------------------

### MISSION PLANNER KURULUM ADIMLARI

1. Mono yükleme
```
sudo apt update
sudo apt install mono-complete -y
```
2. Mission Planner indirme
```
https://firmware.ardupilot.org/Tools/MissionPlanner/latest
```
3. Dosyanın açılması
MissionPlanner-latest.zip → ev dizinine çıkarılır:
```
unzip MissionPlanner-latest.zip -d ~/MissionPlanner
```
4. Çalıştırma
```
cd ~/MissionPlanner
mono MissionPlanner.exe
```
-----------------------------------------------------

#### SİSTEM ÇIKIŞI

Mission Planner açıldığında:
- Sol üstte Flight Data sekmesi
- Sağ üstte bağlantı seçenekleri (COM/UDP/TCP)
- Harita paneli ve telemetri veri akışı

![Şekil 1](../images/missionplanner.png)  
*Şekil 1. Mission Planner Arayüzü*

-----------------------------------------------------

#### SITL BAĞLANTISI (UDP 14550)

1. SITL başlatılır:
```
cd ~/ardupilot/ArduPlane
sim_vehicle.py --console --map
```
2. Mission Planner → CONNECT bölümüne girilir
```
Bağlantı tipi: UDP
Port: 14550
```
3. Bağlantı kurulması ile telemetri akışı başlar:
- MAVLink heartbeat
- EKF status
- GPS sim verisi
- Flight mode durum bilgisi

-----------------------------------------------------

#### BAĞLANTI DOĞRULAMA

Mission Planner ana ekranında:
- Mode göstergesi → GUIDED, AUTO vb.
- GPS → 3D Fix simülasyon mesajı
- RC input → değişim grafiği
- Attitude → horizon göstergesi

Beklenen telemetri mesaj örnekleri:
- HEARTBEAT
- ATTITUDE
- GPS_RAW_INT
- RC_CHANNELS

-----------------------------------------------------

##### TEMEL KOMUT TESTLERİ

1. Mode değişimi:
```
GUIDED mode
```
→ Flight Mode sekmesinden seçilir

2. ARM komutu:
```
arm throttle
```
veya GUI üzerindeki ARM düğmesi

3. Kalkış komutu:
```
takeoff 50
```
4. Yönlendirme:
RC sekmesinden kumanda sim parametreleri değiştirilebilir

-----------------------------------------------------

#### LOG ALMA VE GÖRÜNTÜLEME

Mission Planner üzerinden:
- DataFlash Log Download
- Telemetry Logs (TLOG)
- log analiz: CTUN, ATT, VIBE

SITL testlerinde log görüntüleme:
mav.tlog ve mav.tlog.raw dosyaları ~/ dizininde oluşabilir.

-----------------------------------------------------

#### HATALAR VE ÇÖZÜMLER

Hata: MissionPlanner.exe açılmıyor
- Çözüm: mono-complete eksik → yeniden kur

Hata: bağlantı kuruluyor fakat veri yok
- Çözüm: SITL default port farklı → Mission Planner port yönlendirmesi 14551 yapılabilir

Hata: harita yüklenmiyor
- Çözüm: internet erişimi gereklidir, DNS ayarları kontrol edilmelidir

-----------------------------------------------------

SONUÇ

- Mission Planner başarıyla çalıştırıldı
- SITL ile UDP 14550 üzerinden bağlantı doğrulandı
- telemetri akışı ve komut seti test edildi
- log görüntüleme fonksiyonları aktif hale getirildi

-----------------------------------------------------

REFERANSLAR

- https://ardupilot.org/planner/
- https://ardupilot.org/dev/docs/using-sitl-for-ardupilot-testing.html
