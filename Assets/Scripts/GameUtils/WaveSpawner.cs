using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameUtils;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public EnemyAI enemy;

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
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            EnemyAI spawnedEnemy = Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
            spawnedEnemy.OnDeath += OnEnemyDeath;
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
            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
        }
    }
}
