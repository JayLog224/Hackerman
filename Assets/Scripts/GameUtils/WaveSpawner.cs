using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameUtils;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public GameObject enemy;
    public List<GameObject> allEnemies;

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


            GameObject spawnedEnemy = Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
            allEnemies.Add(spawnedEnemy);
            spawnedEnemy.GetComponent<DamageableEntity>().OnDeath += OnEnemyDeath;
        }
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
}
