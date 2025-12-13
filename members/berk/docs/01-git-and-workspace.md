# Git Kurulumu ve Workspace Standartları

**Sorumlu:** @WorldOfBerk  
**Tarih:** 24-10-2025  
**Konu:** Git Kurulumu  

-----------------------------------------------------

AMAÇ
Bu doküman, geliştirme ortamının standart Git kurulumu, kullanıcı kimlik tanımlaması,
SSH anahtar oluşturulması ve workspace dizin modelinin hazırlanması için referans niteliğindedir.

-----------------------------------------------------

### Git Kurulumu

Komutlar:
```bash
sudo apt update
sudo apt install git -y
```

Versiyon kontrolü:
```bash
git --version
```
-----------------------------------------------------

### Global Kimlik Tanımlama

Commit doğrulaması için ad ve mail adresi girilmelidir:
```bash
git config --global user.name "github-username"
git config --global user.email "github_email@domain.com"
```
```bash
Config görüntüleme:
git config --global --list
```
-----------------------------------------------------

### SSH Anahtarı Oluşturma

GitHub bağlantısı için güvenli SSH tavsiye edilir:
```bash
ssh-keygen -t ed25519 -C "github_email@domain.com"
```

Public key görüntüleme:
cat ~/.ssh/id_ed25519.pub

Anahtar GitHub’a eklenir:
GitHub → Settings → SSH and GPG Keys → New Key

Bağlantı testi:
```bash
ssh -T git@github.com
```
Beklenen çıktı:
```bash
Hi @username! You've successfully authenticated.
```
-----------------------------------------------------

Workspace Dizini

Standart workspace açılması:
```bash
mkdir -p ~/YODA_ws
cd ~/YODA_ws
```

Repository klonlama:
```bash
git clone git@github.com:MehmetEmreAksu/YODA-IHA-YAZILIM.git
cd YODA-IHA-YAZILIM
```
-----------------------------------------------------

Branch Kullanımı

Main kesinlikle development için kullanılmaz.

Yeni branch açma:
```bash
git checkout -b berk/sim-bridge-setup
```

Branch push:
```bash
git push -u origin berk/sim-bridge-setup
```
-----------------------------------------------------

Sık Karşılaşılan Hatalar ve Çözümler


<span style="color:red">Hata:</span> Permission denied (publickey)
- Çözüm: SSH anahtarı GitHub’a ekli değil → tekrar ekle

<span style="color:red">Hata:</span> fatal: not a git repository
- Çözüm: Yanlış konumda → repo dizinine geç

<span style="color:red">Hata:</span> push yetkin yok
- Çözüm: doğru branch’e geç, main’e yazma

-----------------------------------------------------

Sonuç

- Git kurulumu tamamlandı
- SSH bağlanması yapıldı
- Workspace dizini oluşturuldu
- Branch akışı ekip standardına uygun hale getirildi

-----------------------------------------------------

Referanslar
- https://docs.github.com/en/authentication/connecting-to-github-with-ssh
- https://git-scm.com/book/en/v2


