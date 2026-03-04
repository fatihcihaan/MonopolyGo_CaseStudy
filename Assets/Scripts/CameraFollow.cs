using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Takip Ayarlarý")]
    public Transform target; // Kameranýn takip edeceði obje (Arabamýz)
    public float smoothSpeed = 5f; // Kameranýn yumuþak kayma hýzý
    
    [Header("Mesafe Ayarlarý")]
    // Kameranýn arabadan ne kadar yukarýda ve geride duracaðý (X, Y, Z)
    public Vector3 offset = new Vector3(0f, 5f, -8f); 

    void LateUpdate()
    {
        // Eðer takip edilecek bir hedef yoksa hata verme, bekle
        if (target == null) return;

        // Kameranýn gitmek istediði asýl hedef pozisyon
        Vector3 desiredPosition = target.position + offset;
        
        // Kamerayý þu anki yerinden, hedef yere doðru "yumuþakça" kaydýr (Lerp)
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}