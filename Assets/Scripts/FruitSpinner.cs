using UnityEngine;

public class FruitSpinner : MonoBehaviour
{
    [Header("Dönüþ Hýzý")]
    public float spinSpeed = 150f;

    void Update()
    {
        // Meyveyi Y ekseni etrafýnda (kendi etrafýnda) sürekli döndürür
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
}