using UnityEngine;
using System.Collections;

public class platformGenerator : MonoBehaviour
{

    public GameObject[] thePlatforms;
    public GameObject berry;
    public GameObject enemy;
    public Transform freddyPosition;
    public Transform generationPoint;
    public Transform firstGenerationPoint;

    public float distanceBetweenPlatformsMin;
    public float distanceBetweenPlatformsMax;

    public float minPlatformHeight;
    public float maxPlatformHeight;

    private float platformWidth;


    // Update is called once per frame
    void Update()
    {
        // Kun kettu etenee GenerationPointin ohi, luodaan uusi taso
        if (freddyPosition.position.x > firstGenerationPoint.position.x)
        {
            if (transform.position.x < generationPoint.position.x)
            {
                GeneratePlatform();
            }
        }
    }

    void GeneratePlatform()
    {
        // PLATFORM SPAWN
        // Satunnainen platform
        int platformIndex = Random.Range(0, thePlatforms.Length);

        // Luodaan platform satunnaiselle korkeudelle ja etäisyydelle edellisestä
        if (platformIndex > 2.5)
        {
            transform.position = new Vector2(transform.position.x + 150 + Random.Range(distanceBetweenPlatformsMin, distanceBetweenPlatformsMax), Random.Range(minPlatformHeight, (maxPlatformHeight - 5)));
            Instantiate(thePlatforms[platformIndex], transform.position, Quaternion.identity);
        }

        // Luodaan platform satunnaiselle korkeudelle ja etäisyydelle edellisestä
        // Luo platformin matalammalle korkeudelle
        else
        {
            transform.position = new Vector2(transform.position.x + 150 + Random.Range(distanceBetweenPlatformsMin, distanceBetweenPlatformsMax), Random.Range(minPlatformHeight, maxPlatformHeight));
            Instantiate(thePlatforms[platformIndex], transform.position, Quaternion.identity);
        }



        // BERRY SPAWN
        // 40% todennäköisyydellä ei berryä
        int berryRandom = Random.Range(0, 9);

        //40% todennäköisyydellä 1 berry 
        if (berryRandom < 5.5 && berryRandom > 1.5)
        {
            SpawnBerry();
        }
        //20% todennäköisyydellä luodaan 2 berryä
        if (berryRandom < 1.5 && berryRandom > -1)
        {
            SpawnBerry();
            SpawnBerry();
        }

        // ENEMY SPAWN
        // 70 % todennäköisyydellä ei enemyä
        int enemyRandom = Random.Range(0, 9);

        //30% todennäköisyydellä luodaan 1 enemy
        if (enemyRandom < 3.5)
        {
            SpawnEnemy();
        }
    }

    // Luodaan Berry satunnaiseen kohtaan platformille
    void SpawnBerry()
    {
        Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(-60, 60), transform.position.y + 15);
        Instantiate(berry, spawnPosition, Quaternion.identity);
    }

    // Luodaan Enemy satunnaiseen kohtaan platformille
    void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(-60, 60), transform.position.y + 15);
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
}


