using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahneler arasý geçiþ için en hayati kütüphane!

public class MainMenuController : MonoBehaviour
{
    // BAÞLA butonuna týklanýnca çalýþacak fonksiyon
    public void StartGame()
    {
        // 1. sýradaki sahneyi (Asýl Oyun sahnemizi) yükle
        SceneManager.LoadScene(1); 
    }

    // Ýstersen ileride ÇIKIÞ butonu da ekleyebilirsin, hazýr bulunsun
    public void QuitGame()
    {
        Debug.Log("Oyundan Çýkýldý!");
        Application.Quit();
    }
}