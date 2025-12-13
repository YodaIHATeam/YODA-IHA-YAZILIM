# ROS2 Humble Kurulumu ve Test

Sorumlu: @WorldOfBerk
Tarih: 24-10-2025
Konu: ROS2 Kurulumu, Workspace Oluşturma, Talker/Listener Testleri

-----------------------------------------------------

### AMAÇ
Bu çalışmanın amacı ROS2 Humble dağıtımının kurulması, temel pub/sub testlerinin çalıştırılması
ve ROS2 geliştirme ortamının standart dizin modeli içerisinde hazırlanmasıdır.

-----------------------------------------------------

#### ROS2 HUMBLE KURULUM ADIMLARI

1. Lokal depo güncellemesi:
```bash
sudo apt update
sudo apt upgrade -y
```
2. Gerekli paketlerin kurulması:
```bash
sudo apt install locales curl gnupg lsb-release -y
```
3. Locale yapılandırması:
```bash
sudo locale-gen en_US en_US.UTF-8
sudo update-locale LC_ALL=en_US.UTF-8 LANG=en_US.UTF-8
```

4. Depo anahtarının eklenmesi:
```bash
sudo curl -sSL https://raw.githubusercontent.com/ros/rosdistro/master/ros.asc -o /usr/share/keyrings/ros-archive-keyring.gpg
```

5. ROS2 yazılım deposunun eklenmesi:
```bash
echo "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/ros-archive-keyring.gpg] http://packages.ros.org/ros2/ubuntu $(. /etc/os-release && echo $UBUNTU_CODENAME) main" | sudo tee /etc/apt/sources.list.d/ros2.list > /dev/null
```
6. ROS2 Humble kurulumu:
```bash
sudo apt update
sudo apt install ros-humble-desktop -y
```
7. Çevre değişkenlerinin yüklenmesi:
```bash
source /opt/ros/humble/setup.bash
```
-----------------------------------------------------

#### WORKSPACE OLUŞTURULMASI

1. Ana workspace:
```bash
mkdir -p ~/ros2_ws/src
cd ~/ros2_ws
```

2. Build işlemi:
```bash
colcon build
```

3. Setup yükleme:
```bash
source install/setup.bash
```
-----------------------------------------------------

#### BASİT PUB/SUB TESTİ

Talker başlatma:
```bash
ros2 run demo_nodes_cpp talker
```

Listener başlatma:
```bash
ros2 run demo_nodes_cpp listener
```
Beklenen çıktı:
- talker → sürekli mesaj üretir
- listener → aynı mesajları terminalde görüntüler

-----------------------------------------------------

#### ORTAM DEĞİŞKENLERİNİN KALICI HALE GETİRİLMESİ

1. Bashrc içerisine ekle:
```bash
echo "source /opt/ros/humble/setup.bash" >> ~/.bashrc
echo "source ~/ros2_ws/install/setup.bash" >> ~/.bashrc
```

2. Shell yenileme:
```bash
source ~/.bashrc
```
-----------------------------------------------------

SİSTEM TEST DOĞRULAMASI

Test 1: ros2 komut doğrulama
```
ros2 --help
```

Test 2: paket listesi
```
ros2 pkg list
```
Test 3: nodo çalıştırma
```
ros2 run demo_nodes_cpp talker
```

Test 4: DDS haberleşme doğrulama
- talker ve listener arasında mesaj kaybı olmamalıdır

-----------------------------------------------------

HATALAR VE ÇÖZÜMLER

Hata: "ros2: command not found"
- Çözüm: bashrc içi source satırları eksik, ekle ve shell yenile

Hata: talker çalışıyor ama listener veri almıyor
- Çözüm: farklı DDS switch → Fast-DDS yerine CycloneDDS test edilir

Hata: build sırasında colcon hatası
- Çözüm: workspace klasöründe misconfig, src içeriği kontrol edilir

-----------------------------------------------------

SONUÇ

- ROS2 Humble kurulumu başarıyla tamamlandı
- Workspace yapısı oluşturuldu
- Pub/Sub testleri başarılı sonuç verdi
- Sistem ortamı bashrc ile kalıcı hale getirildi

-----------------------------------------------------

REFERANSLAR

- https://docs.ros.org/en/humble/index.html
- https://docs.ros.org/en/humble/Installation.html
