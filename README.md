# 🎲 Monopoly & Fruit Catcher 3D (Mülakat/Örnek Projesi)

Bu proje, Monopoly tarzı grid tabanlı bir haritada zar atarak ilerlenen ve 3D dünyada dinamik 2D objelerin (meyvelerin) toplandığı bir hyper-casual oyun demosudur. 

## 🎮 Oyunu Hemen Oyna (Playable Build)
Unity'yi açmakla uğraşmadan oyunu direkt bilgisayarınızda test etmek için:
👉 **[Oynanabilir Sürümü Buradan İndirin (ZIP)](https://drive.google.com/file/d/1mSijJ4_QjpTFLBa2MEJ13kivYzXN39rp/view?usp=sharing)**
*(İndirdiğiniz ZIP dosyasını klasöre çıkartın ve `.exe` dosyasına çift tıklayarak oynayın.)*
👉 **[Oynanış Videosunu Buradan İzleyebilirsiniz](https://drive.google.com/file/d/11jbjWigJsje3iBCrCFu6wvFuHldnoFAL/view?usp=sharing)**

## 🕹️ Nasıl Oynanır?
* **Oyun Başlangıcı:** Ana menüden "Oyunu Başlat" butonuna basarak Monopoly tahtasına giriş yapın.
* **Hareket (Zar Atma):** Ekranda beliren **ZAR AT** butonuna tıklayın (veya klavyeden **BOŞLUK (Space)** tuşuna basın). 
* **Toplama Sistemi:** Karakteriniz zardaki sayı kadar otomatik ilerler. Yol üzerindeki Elma, Armut ve Çilekleri topladığınızda arayüz (UI) eşzamanlı güncellenir ve görsel patlama efektleri (Particle System) devreye girer.
* **Çıkış:** Oyun içindeyken `ESC` tuşuna basarak jilet gibi çalışan geri sayım sistemiyle ana menüye dönebilirsiniz.

## 🛠️ Kullanılan Teknolojiler & Sistemler
* **Oyun Motoru:** Unity 6 (6000.0.68f1) *(Kodu inceleyecekler için çok önemli bilgi)*
* **Dil:** C#
* **Öne Çıkan Mekanikler:**
  * Coroutine tabanlı Slot Makinesi tarzı zar animasyonu.
  * Grid tabanlı hedef bulma (Vector3.MoveTowards) sistemi.
  * 3D dünya içine yedirilmiş Canvas ve TextMeshPro UI entegrasyonu.
  * Dinamik obje üretimi (Prefab) ve yok etme (Destroy) mantığı.

## 👨‍💻 Kurulum (Geliştiriciler İçin)
Projeyi Unity'de açıp kodları incelemek isterseniz:
1. Bu repoyu bilgisayarınıza klonlayın (`git clone ...`).
2. **Unity Hub** üzerinden "Add" diyerek klasörü seçin (Unity 6 sürümü kurulu olmalıdır).
3. `Assets/Scenes` klasöründeki `Opening` veya `SampleScene` sahnelerinden birini açarak projeyi inceleyebilirsiniz.
