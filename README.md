# Tanıtım

Bu repo, **YODA İHA Takımı Yazılım Ekibi** üyelerinin çalışmalarını, öğrenim süreçlerini ve yapılan testleri tek bir merkezde toplamak için oluşturulmuştur.  

Amaç, takım içi bilgi paylaşımını kolaylaştırmak, herkesin kendi ilerlemesini belgelemek ve gelecekte gelen yeni üyelere güçlü bir bilgi temeli sunmaktır.

---

## Amaç
- Her üyenin kendi çalışma alanındaki ilerlemelerini düzenli şekilde dokümante etmesi  
- ROS2, Ardupilot, Yapay Zeka, RL, Görüntü İşleme, Simülasyon vb. konularda yapılan çalışmaları kayıt altına almak  
- Ekip içi standardizasyon sağlamak (ortak şablon, isimlendirme, raporlama biçimi)  
- Yeni gelen ekip üyelerinin sürece hızla adapte olmasını sağlamak  
- Geçmişte yaşanan sorunların ve çözümlerin kolayca bulunabilir olması

Hedefimiz:
- Karşılaşılan hataların/sorumların çözümünü belgelemek
- Öğrenilen şeyleri ekibe kazandırmak
- İlerleme adımlarını açık ve anlaşılır biçimde saklamak

Eğer bir kurulum, test, hatayla uğraştıysanız mutlaka yazın.  
Gelecekte biri aynı sorunu yaşadığında **5 dakikada çözüme ulaşabilir.**

---

## 📂 Repo Yapısı

```

YODA-IHA-YAZILIM/  
│  
├── README.md # Genel açıklama (bu dosya)  
├── CONTRIBUTING.md # Katkı rehberi (yapılan çalışmaların repoya nasıl ekleneceği)  
│  
├── docs/ # Ortak dokümantasyon (örn. genel kılavuzlar)  
│ ├── ros2_genel_kurulum.md  
│ ├── ArdupilotKurulumları
│ ├── └── kurulumlar.md  
│ └── ...  
│  
├── members/ # Üyelerin kişisel çalışmaları  
│ ├── yakupgulcan/  
│ │ ├── ros2_kurulum.md  
│ │ ├── rl_pytorch_deneme.md  
│ │ └── images/  
│ │ └── rviz_test.png  
│ ├── sametcanak/  
│ └── ...  
│  
└── templates/ # Şablonlar  
└── calisma_template.md # Herkesin kullanması gereken çalışma şablonu

```

---

## Çalışma Nasıl Yapılır?

Her ekip üyesi kendi alanında yaptığı çalışmaları Markdown formatında belgelemelidir.  
Bu belgeler, daha sonra takımın bilgi havuzuna katkı sağlar.  

### 1. Yeni bir çalışma başlatmadan önce
- `CONTRIBUTING.md` dosyasını okuyun.  
  > Burada branch oluşturma, dosya ekleme, PR açma adımları detaylı şekilde anlatılmıştır.

### 2. Branch oluşturma
Branch adınızı şu şekilde belirleyin:

```

yakupgulcan-<kısa-işlev>

```

Örneğin:
- `yakupgulcan-ros2-kurulum`
- `sametcanak-ardupilot-testleri`
- `mertcolpan-rl-pytorch`

### 3. Template kullanımı
Yeni bir çalışma eklemeden önce `templates/calisma_template.md` dosyasını açın ve **kendi çalışmanıza göre doldurun.**

Örneğin:
```

members/yakupgulcan/ros2_kurulum.md

````

dosyasını oluşturup template içeriğini açtığınız dosyaya yapıştırın ve düzenleyin.

---

## Şablon

> Detaylı şablon için: [`templates/calisma_template.md`](./templates/calisma_template.md)

---

## 🔁 Katkı Süreci (Contributing)

Tüm adımlar [CONTRIBUTING.md](./CONTRIBUTING.md) dosyasında detaylı biçimde anlatılmıştır.  
Kısaca özet:

1. **Yeni branch oluşturun** (örnek: `yakupgulcan-ros2`).
2. **Çalışmanızı ekleyin** (örn. `members/yakupgulcan/ros2_kurulum.md`).
3. **Template’e uygun şekilde açıklayın.**
4. **Değişiklikleri commit edip push edin.**
5. **Pull Request (PR)** açın.
6. **Takımdaki sorumluyu haberdar edip pull requesti onaylamasını isteyin.**

---

## Önemli Dosyalar

| Dosya                                                                                  | Açıklama                                      |
| -------------------------------------------------------------------------------------- | --------------------------------------------- |
| `README.md`                                                                            | Bu ana rehber                                 |
| [`CONTRIBUTING.md`](https://chatgpt.com/c/CONTRIBUTING.md)                             | Katkı kuralları ve adım adım anlatım          |
| [`templates/calisma_template.md`](https://chatgpt.com/c/templates/calisma_template.md) | Her çalışmada kullanılacak Markdown şablonu   |
| `docs/`                                                                                | Ortak takım dokümantasyonları                 |
| `members/`                                                                             | Üyelerin bireysel çalışmaları ve ilerlemeleri |
|                                                                                        |                                               |
