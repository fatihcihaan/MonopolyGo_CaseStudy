using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Bağlantılar")]
    public MapManager mapManager;
    
    // YENİ: Eski inventoryText'i çöpe attık, 3 ayrı UI kutumuzu ekledik!
    public TMP_Text elmaText;
    public TMP_Text armutText;
    public TMP_Text cilekText;

    public GameObject diceButton; 
    public TMP_Text diceText; // Ekranda hızlıca değişecek zar sayımız

    [Header("Hareket Ayarları")]
    public float moveSpeed = 5f;

    [Header("Ses ve Efektler")]
    public AudioClip collectSound; 
    private AudioSource audioSource;
    public GameObject collectEffect;

    // Oyuncunun Cebindeki Meyveler
    private int appleCount = 0;
    private int pearCount = 0;
    private int strawberryCount = 0;

    private int currentTileIndex = 0;
    private bool isMoving = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateInventoryUI();
        
        // Başlangıçta zar yazısını boş bırakalım ki ekranda anlamsız durmasın
        if (diceText != null) diceText.text = ""; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            StartCoroutine(DiceRollAnimation());
        }
    }

    public void RollDiceFromButton()
    {
        if (!isMoving)
        {
            StartCoroutine(DiceRollAnimation());
        }
    }

    IEnumerator DiceRollAnimation()
    {
        isMoving = true;
        
        if (diceButton != null) diceButton.SetActive(false);

        // 1. ZAR ANİMASYONU (Slot Makinesi Etkisi)
        float rollDuration = 0.5f; // Animasyonun süresi (Yarım saniye)
        float elapsedTime = 0f;

        while (elapsedTime < rollDuration)
        {
            // Ekrana 1 ile 6 arası rastgele hızlı sayılar bas
            if (diceText != null) diceText.text = Random.Range(1, 7).ToString();
            
            elapsedTime += 0.05f; // Her 0.05 saniyede bir değiştir
            yield return new WaitForSeconds(0.05f);
        }

        // 2. GERÇEK ZARI BELİRLE VE EKRANA YAZ
        int finalDiceRoll = Random.Range(1, 7); 
        Debug.Log("🎲 Zar Geldi: " + finalDiceRoll);
        
        if (diceText != null) 
        {
            diceText.text = finalDiceRoll.ToString(); // Gerçek zarı yaz
            diceText.color = Color.yellow; // Dikkat çeksin diye sarı yap
        }

        // Oyuncunun zarı görmesi için yarım saniye bekle
        yield return new WaitForSeconds(0.5f);

        // Yazı rengini eski haline (beyaz) al
        if (diceText != null) diceText.color = Color.white;

        // 3. ASIL HAREKET KODUNU ÇALIŞTIR
        yield return StartCoroutine(MovePlayer(finalDiceRoll));
        
        // Hareket bitince butonu geri getir ve yazıyı temizle
        if (diceButton != null) diceButton.SetActive(true);
        if (diceText != null) diceText.text = ""; 
        
        isMoving = false; 
    }

    IEnumerator MovePlayer(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            if (currentTileIndex >= mapManager.tilePositions.Count - 1)
            {
                Debug.Log("Haritanın sonuna ulaşıldı!");
                break; 
            }

            currentTileIndex++;
            Vector3 targetPosition = mapManager.tilePositions[currentTileIndex];
            targetPosition.y += 0.5f; 

            while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetPosition;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void CollectReward(string type, int amount)
    {
        if (type.Contains("Apple")) appleCount += amount;
        else if (type.Contains("Pear")) pearCount += amount;
        else if (type.Contains("Strawberry")) strawberryCount += amount;

        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        // Artık "Elma: " yazısı yok, sadece sayı var. İkonlar zaten ne olduklarını anlatacak.
        if (elmaText != null) elmaText.text = appleCount.ToString();
        if (armutText != null) armutText.text = pearCount.ToString();
        if (cilekText != null) cilekText.text = strawberryCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            string fruitName = other.gameObject.name;
            CollectReward(fruitName, 1); 

            if (audioSource != null && collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }

            if (collectEffect != null)
            {
                Instantiate(collectEffect, other.transform.position, Quaternion.identity);
            }

            Destroy(other.gameObject);
        }
    }
}