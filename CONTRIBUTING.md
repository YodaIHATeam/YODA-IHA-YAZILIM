# Contributing — YODA-IHA-YAZILIM

Bu repo üyelerin çalışmalarını paylaştığı ve dokümante ettiği ana merkezi repodur. Aşağıda katkı kuralları, branch isimlendirme, PR açma vs. adım adım anlatılmıştır.

## 1) Genel Kurallar
- Her çalışma `templates/calisma_template.md` kullanılarak doldurulmalı.
- Branch ismi formatı: `githubUsername-<kısa-işlev>`  
  Örnek: `yakupgulcan-ros2-setup`, `sametcanak-ardupilot-test`
- Commit mesajları açık ve anlamlı olmalı
- Her PR açıklamasında **yapılanlar**, **karşılaşılan sorunlar**, **test sonuçları** ve **ekler** (görsel/kod) bulunmalı.

## 2) Yeni bir katkı (tercih edilen - fork & clone)
Bu yol dış katkılar veya ciddi kod değişiklikleri için tavsiye edilir.

1. Local'e klonlayın:
   ```bash
   git clone https://github.com/<sizin-kullanici>/<repo>.git
   cd YODA-IHA-YAZILIM
````

2. Upstream ekleyin (orijinal repoyu takip etmek için):
    
    ```bash
    git remote add upstream https://github.com/YODA-IHA-YAZILIM/YODA-IHA-YAZILIM.git
    git fetch upstream
    git checkout main
    git pull upstream main
    ```
    
2. Yeni branch oluşturun :
    
    ```bash
    git checkout -b yakupgulcan-<kısa-işlev>
    ```
    
3. Değişiklik yapın (ör. `members/yakupgulcan/ros2_kurulum.md` ekle), `templates/calisma_template.md` kullanın.
    
4. Staging & commit:
    
    ```bash
    git add .
    git commit -m "initial ROS2 install doc and tests"
    ```
    
5. Push:
    
    ```bash
    git merge main
    git push origin yakupgulcan-<kısa-işlev>
    ```
    
6.  GitHub sayfasından yeni açılan branch için `Compare & pull request` butonuna tıklayın ve PR açıklamasını doldurun.
    

## 4) PR Açarken / Review için

PR açıklamasında şu bölümler olmalı:

- **Yapılanlar**: ne eklendi/kodlandı/dokümente edildi
- **Checklist**:
    
    -  Template dolduruldu
        
    -  Kod varsa `members/<user>/code` altında
        
    * çalışma adımları eklendi
        

Reviewerlar: en az 1 ekip arkadaşı PR'ı incelemeli ve onaylamalı.

## 7) Issue açma

- Yeni bir hata ya da geliştirme önerisi görürseniz `Issues` bölümünden açın.
    
- Issue şablonunu kullanın (hangi ortam, adımlar, beklenen/gerçek sonuç).

## Yardım / Sorular

Konuşma kanalı: GitHub Discussions veya ekip Discord / Slack kanalınız. PR ya da issue numarası ile paylaşın — ilgili kişiler hızlıca destek sağlar.


## PR Şablonu
````
```markdown
## Kısa Özet
(Ne yapıldı? neden?)

## Yapılanlar
- [ ] Doküman eklendi: `members/<kullanici>/...`
- [ ] Kod eklendi: `members/<kullanici>/code/...`
- [ ] Görseller yüklendi: `members/<kullanici>/images/...`

````