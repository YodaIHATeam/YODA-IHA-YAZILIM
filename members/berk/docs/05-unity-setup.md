# Unity Hub ve Unity Editor Kurulumu (Linux .deb)

- Sorumlu: @WorldOfBerk
- Tarih: 24-10-2025
- Konu: Unity Hub kurulumu (.deb), Editor indirme, temel proje yapısı

-----------------------------------------------------

#### AMAÇ
Bu doküman Unity Hub’ın resmi .deb paketi ile kurulumu, Unity Editor versiyon seçimi
ve simülasyon geliştirme ortamının hazırlanması için referans niteliğindedir.

-----------------------------------------------------

#### İNDİRME

Unity Hub Linux .deb paketi resmi döküman bağlantısı:
```
https://docs.unity3d.com/hub/manual/InstallHub.html#install-hub-linux
```

İndirme:
- UnityHubSetup.deb dosyası indirildikten sonra varsayılan Konum klasöründe bulunur:
~/Downloads/UnityHubSetup.deb

-----------------------------------------------------

#### KURULUM ADIMLARI

1. .deb paketinin yüklenmesi:
```
sudo dpkg -i ~/Downloads/UnityHubSetup.deb
```

2. Bağımlılık hatası oluşursa düzeltme:
```
sudo apt --fix-broken install
```

3. Unity Hub çalıştırma:
```
unityhub
```
-----------------------------------------------------

#### EDITOR KURULUMU (LTS TAVSİYE)

Unity Hub açıldıktan sonra:

- Installs sekmesine geçilir
- Add butonuna tıklanır
- Tavsiye edilen LTS sürüm: 2022 LTS
- Platform build destekleri seçilir:
  - Linux Build Support -> Önemli
  - Windows Build Support (opsiyonel)
  - WebGL (opsiyonel)

NOT:
SİHA simülasyon projelerinde LTS stabil sürüm tercih edilir
(tech stream sürümleri mimari kararsızlık ve network plugin hatası verebilir).

-----------------------------------------------------

#### PROJE OLUŞTURMA

Unity Hub → Projects sekmesi → New

Template:
3D Core (HDPR/URP şimdilik gerekmez)

Oluşturulan klasör:
~/UnityProjects/proje_ismi

-----------------------------------------------------

HATALAR VE ÇÖZÜMLER

Hata: dpkg dependency error
- Çözüm: sudo apt --fix-broken install

Hata: Unity Hub butonları görünmüyor
- Çözüm: kütüphane eksikliklerinde “gtk” paketleri yeniden yüklenebilir

Hata: Editor indirme sırasında network timeout
- Çözüm: VPN kapatma, proxy kontrolü

-----------------------------------------------------

SONUÇ

- Unity Hub .deb ile başarıyla kuruldu
- Editor LTS sürümü yüklendi
- sim_bridge workspace oluşturuldu
- Network tabanlı connector geliştirmesi için temel yapı hazırlandı

-----------------------------------------------------
#### Gelecek Çalışmalar

- Unity ve ROS 2 arasında bağlantı kurulacak
- Unity ve Ardupilot arasında bağlantı kurulacak
-----------------------------------------------------

REFERANSLAR

- https://docs.unity3d.com/hub/manual/InstallHub.html#install-hub-linux
- https://unity.com/releases/editor
