using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameUtils;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
   //public GameObject enemy;
    public List<GameObject> allEnemies;

    public GameObject[] spawnPoints;
    float spawnPointOffset = 1.5f;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;

    Wave currentWave;
    int currentWaveNumber;

    private void Start()
    {
        NextWave(); //Begin wave 1...        
    }
    private void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
            enemiesRemainingToSpawn--;

            SpawnEnemiesForWave(enemiesRemainingToSpawn);
        }
    }

    void SpawnEnemiesForWave(int count)
    {

        GameObject spawnedEnemy = Instantiate(currentWave.enemyTypes[count], GetRandomSpawnPoints(), Quaternion.identity);
        allEnemies.Add(spawnedEnemy);
        spawnedEnemy.GetComponent<DamageableEntity>().OnDeath += OnEnemyDeath;
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;
        if (enemiesRemainingAlive == 0)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        Debug.Log("Starting wave: " + currentWaveNumber);


        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];
            allEnemies = new List<GameObject>();
            enemiesRemainingToSpawn = currentWave.enemyCount;
            Debug.Log("Enemies to spawn: " + currentWave.enemyCount);
            enemiesRemainingAlive = enemiesRemainingToSpawn;
        }
    }

    Vector3 GetRandomSpawnPoints()
    {
        int random = Random.Range(0, spawnPoints.Length);

        float spawnX = Random.Range(spawnPoints[random].transform.position.x - spawnPointOffset, spawnPoints[random].transform.position.x + spawnPointOffset);
        float spawnY = Random.Range(spawnPoints[random].transform.position.y - spawnPointOffset, spawnPoints[random].transform.position.y + spawnPointOffset);

        Vector3 spawnPoint = new Vector3(spawnX, spawnY, 0);

        return spawnPoint;
    }

    // D E P R E C A T E D . . .

    //Vector3 GetRandomSpawnPoint()
    //{
    //    float spawnY = Random.Range
    //            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
    //    float spawnX = Random.Range
    //        (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

    //    Vector3 spawnPoint = new Vector3(spawnX, spawnY, 0f);



    //    return spawnPoint;
    //}
}
