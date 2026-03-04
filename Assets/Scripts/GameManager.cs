using System.Collections;
using UnityEngine;
using TMPro; // Yazý için gerekli kütüphane

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI uyariYazisi; // Ekrana çýkacak yazýmýz
    private bool kapaniyorMu = false; // Oyuncunun art arda basmasýný engellemek için kilit

    void Update()
    {
        // Eðer ESC'ye basýlýrsa ve oyun zaten kapanmýyorsa
        if (Input.GetKeyDown(KeyCode.Escape) && !kapaniyorMu)
        {
            StartCoroutine(KapanisGeriSayim()); // Geri sayým motorunu ateþle
        }
    }

    // Geri sayým motorumuz
    IEnumerator KapanisGeriSayim()
    {
        kapaniyorMu = true; // Kilidi kapat ki oyuncu bir daha ESC'ye basamasýn
        uyariYazisi.gameObject.SetActive(true); // Görünmez yazýyý görünür yap!

        uyariYazisi.text = "Oyun Kapanýyor... 3";
        yield return new WaitForSeconds(1f); // 1 saniye bekle

        uyariYazisi.text = "Oyun Kapanýyor... 2";
        yield return new WaitForSeconds(1f); // 1 saniye bekle

        uyariYazisi.text = "Oyun Kapanýyor... 1";
        yield return new WaitForSeconds(1f); // 1 saniye bekle

        Application.Quit(); // Oyunu kapat (Build alýnca masaüstüne atar)
        
        // (Unity içindeyken kapandýðýný anlaman için ekstra log)
        Debug.Log("Oyun kapandý!"); 
    }
}