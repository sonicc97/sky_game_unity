using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawnEnemy;
    public GameObject powerupSpawn;
    private float spawnPos = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(1);
        Instantiate(powerupSpawn, SpawnPosition(), powerupSpawn.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupSpawn, SpawnPosition(), powerupSpawn.transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(spawnEnemy, SpawnPosition(), spawnEnemy.transform.rotation);
        }
    }

    //private Vector3 SpawnPosition(): Definition of a private method that will return a vector (position) in three-dimensional space (Vector3).
    private Vector3 SpawnPosition()
    {
        float spawnRandomX = Random.Range(-spawnPos, spawnPos);
        float spawnRandomZ = Random.Range(-spawnPos, spawnPos);

        Vector3 randomPos = new Vector3(-spawnRandomX, 0, spawnRandomZ);

        return randomPos;
    }
}
