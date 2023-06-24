using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable] // a class or a struct can be serialized

public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval; // how long it takes for enemy to spawn
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;

    private Wave currentWave; // current wave and int (1st, 2nd, or 3rd wave..)
    private int currentWaveNumber;

    private void Update()
    {
        // takes a wave from waves and makes it currentWave
        currentWave = waves[currentWaveNumber];
        SpawnWave();
    }

    // takes random enemy and spawns at random position
    void SpawnWave()
    {
        GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
        // choose a random spawnpt
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
    }
}
