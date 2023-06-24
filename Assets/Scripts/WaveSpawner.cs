using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable] // a class or a struct can be serialized

public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public CustomerData[] typeOfCustomers;
    public FoodData[] typesOfFood;
    public float spawnInterval; // how long it takes for enemy to spawn
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    [SerializeField] private Customer customerPrefab;

    private Wave currentWave; // current wave and int (1st, 2nd, or 3rd wave..)
    private int currentWaveNumber;
    private float nextSpawnTime; // time it takes to spawn next enem

    private bool canSpawn = true; // helps stop spawning for a while, go to the next wave

    private void Update()
    {
        // takes a wave from waves and makes it currentWave
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        // 13:00 keeps track of how many enemies been spawn
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Customer");
        // diff
        if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 != waves.Length) 
        {
            currentWaveNumber++;
            canSpawn = true;
        }
    }

    // takes random enemy and spawns at random position
    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time) //Time.time = time passed in seconds, since game start [to check time interval of spawn]
        {
            // Data Chosen At Random
            CustomerData randomCustomerType = currentWave.typeOfCustomers[Random.Range(0, currentWave.typeOfCustomers.Length)];
            FoodData randomFoodChoice = currentWave.typesOfFood[Random.Range(0, currentWave.typesOfFood.Length)];

            // Attach Data to customerPrefab
            customerPrefab.CustomerData = randomCustomerType;
            customerPrefab.FoodChoice = randomFoodChoice;

            // Choose a random spawn point
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(customerPrefab.gameObject, randomPoint.position, Quaternion.identity);

            // only spawn the num we want to spawn
            currentWave.noOfEnemies--;
            // 12:03 timer for spawning next enem
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
            }
        }
    }
}
