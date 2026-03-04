using UnityEngine;

public class MeyveDondur : MonoBehaviour
{
    // Dönüþ hýzýný Unity içinden ayarlayabilmen için bir ayar açýyoruz
    public float donmeHizi = -100f; 

    void Update()
    {
        // Meyveyi her saniye Z ekseninde (kendi etrafýnda) döndür
        transform.Rotate(0f, 0f, donmeHizi * Time.deltaTime);
    }
}