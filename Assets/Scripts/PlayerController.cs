using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Bağlantılar")]
    public MapManager mapManager;

    public TMP_Text elmaText;
    public TMP_Text armutText;
    public TMP_Text cilekText;

    public GameObject diceButton;
    public TMP_Text diceText;

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

    private List<GameObject> toplananMeyveler = new List<GameObject>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // YENİ: Oyun açıldığında bilgisayarın hafızasına bak, kayıt varsa sayıları geri çek!
        appleCount = PlayerPrefs.GetInt("Kayıt_Elma", 0);
        pearCount = PlayerPrefs.GetInt("Kayıt_Armut", 0);
        strawberryCount = PlayerPrefs.GetInt("Kayıt_Cilek", 0);

        UpdateInventoryUI();

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

        float rollDuration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < rollDuration)
        {
            if (diceText != null) diceText.text = Random.Range(1, 7).ToString();
            elapsedTime += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        int finalDiceRoll = Random.Range(1, 7);
        Debug.Log("🎲 Zar Geldi: " + finalDiceRoll);

        if (diceText != null)
        {
            diceText.text = finalDiceRoll.ToString();
            diceText.color = Color.yellow;
        }

        yield return new WaitForSeconds(0.5f);

        if (diceText != null) diceText.color = Color.white;

        yield return StartCoroutine(MovePlayer(finalDiceRoll));

        if (diceButton != null) diceButton.SetActive(true);
        if (diceText != null) diceText.text = "";

        isMoving = false;
    }

    IEnumerator MovePlayer(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            currentTileIndex++;

            if (currentTileIndex >= mapManager.tilePositions.Count)
            {
                currentTileIndex = 0;

                Vector3 startPosition = mapManager.tilePositions[0];
                startPosition.y += 0.5f;
                transform.position = startPosition;

                ResetMapFruits();
            }

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

    void ResetMapFruits()
    {
        foreach (GameObject fruit in toplananMeyveler)
        {
            if (fruit != null)
            {
                fruit.SetActive(true);
            }
        }
        toplananMeyveler.Clear();
    }

    void CollectReward(string type, int amount)
    {
        // YENİ: Meyveyi topladığımız an anında bilgisayarın hafızasına (PlayerPrefs) kaydediyoruz!
        if (type.Contains("Apple"))
        {
            appleCount += amount;
            PlayerPrefs.SetInt("Kayıt_Elma", appleCount);
        }
        else if (type.Contains("Pear"))
        {
            pearCount += amount;
            PlayerPrefs.SetInt("Kayıt_Armut", pearCount);
        }
        else if (type.Contains("Strawberry"))
        {
            strawberryCount += amount;
            PlayerPrefs.SetInt("Kayıt_Cilek", strawberryCount);
        }

        PlayerPrefs.Save(); // Kaydı kalıcı olarak mühürle
        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
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

            if (!toplananMeyveler.Contains(other.gameObject))
            {
                toplananMeyveler.Add(other.gameObject);
            }

            other.gameObject.SetActive(false);
        }
    }
}