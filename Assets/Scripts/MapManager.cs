using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public int id;
    public string rewardType;
    public int rewardAmount;
}

[System.Serializable]
public class MapDataWrapper
{
    public TileData[] tiles;
}

public class MapManager : MonoBehaviour
{
    [Header("Harita Ayarları")]
    public GameObject tilePrefab; 
    public float tileSpacing = 1.5f; 

    [Header("Meyve Kalıpları (Prefabs)")]
    public GameObject applePrefab;
    public GameObject pearPrefab;
    public GameObject strawberryPrefab;

    public List<Vector3> tilePositions = new List<Vector3>(); 
    public List<TileData> mapTiles = new List<TileData>();

    void Start()
    {
        LoadMapData();
        GenerateMap();
    }

    void LoadMapData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("MapData");
        
        if (jsonFile != null)
        {
            MapDataWrapper mapData = JsonUtility.FromJson<MapDataWrapper>(jsonFile.text);
            mapTiles = new List<TileData>(mapData.tiles);
        }
    }

    void GenerateMap()
    {
        for (int i = 0; i < mapTiles.Count; i++)
        {
            // 1. Zemin karesini (Tile) oluştur
            Vector3 spawnPosition = new Vector3(i * tileSpacing, 0, 0);
            tilePositions.Add(spawnPosition); 

            GameObject newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
            newTile.name = "Tile_" + mapTiles[i].id;

            // 2. Meyveyi karenin tam üstüne yerleştir (Clean Code Prensibi)
            SpawnFruitOnTile(mapTiles[i].rewardType, newTile.transform);
        }
    }

    // Meyve oluşturma mantığını modüler olarak ayırdık
    void SpawnFruitOnTile(string rewardType, Transform parentTile)
    {
        GameObject fruitToSpawn = null;

        // JSON'dan gelen veriye göre hangi kalıbı (Prefab) seçeceğimize karar veriyoruz
        if (rewardType == "Apple") fruitToSpawn = applePrefab;
        else if (rewardType == "Pear") fruitToSpawn = pearPrefab;
        else if (rewardType == "Strawberry") fruitToSpawn = strawberryPrefab;

        // Eğer o karede bir meyve varsa (Boş değilse)
        if (fruitToSpawn != null)
        {
            // Meyveyi karenin biraz yukarısına (Y ekseninde 0.75f) koyuyoruz ki küpün içine gömülmesin
            Vector3 fruitPosition = parentTile.position + new Vector3(0, 0.75f, 0);
            
            // Meyveyi sahneye yarat ve karenin "alt objesi" (child) yap ki liste spagettiye dönmesin
            GameObject spawnedFruit = Instantiate(fruitToSpawn, fruitPosition, Quaternion.identity, parentTile);
            spawnedFruit.name = rewardType; 
        }
    }
}