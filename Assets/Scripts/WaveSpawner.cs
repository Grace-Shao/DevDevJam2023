using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable] // a class or a struct can be serialized

public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public List<CustomerData> typeOfCustomers;
    public List<FoodData> typesOfFood;
    public List<int> foodWeights;
    public float spawnInterval; // how long it takes for enemy to spawn

    public Wave() {
        typeOfCustomers = new List<CustomerData>();
        typesOfFood = new List<FoodData>();
        foodWeights = new List<int>();
    }
}

public class WaveSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    [SerializeField] private Customer customerPrefab;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private List<CustomerData> possibleCustomers;
    [SerializeField] private List<FoodData> possibleFood;

    private int waveCount;
    private int enemiesPerWave;
    private float spawnMultiplier;
    private Wave currentWave; // current wave and int (1st, 2nd, or 3rd wave..)
    private float nextSpawnTime; // time it takes to spawn next enem

    private bool canSpawn = true; // helps stop spawning for a while, go to the next wave
    private float nextWaveWait;

    private void Start()
    {
        GenerateWave();
    }

    private void Update()
    {
        SpawnWave();
        // 13:00 keeps track of how many enemies been spawn
        
        // diff

        if (nextWaveWait <= 0) {
            GenerateWave();
            canSpawn = true;
        } else if (!canSpawn) {
            nextWaveWait -= Time.deltaTime;
        }
    }

    // takes random enemy and spawns at random position
    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time) //Time.time = time passed in seconds, since game start [to check time interval of spawn]
        {
            Transform randomPoint;
            //if (waveCount % 5 == 0)
            //{
            //    //Boss Wave
            //    DemandRandomFoods(Random.Range(9, 12), bossPrefab.GetComponent<Boss>().FoodRequirements);
            //    randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            //    bossPrefab.GetComponent<BossMovement>().rushPointObject = CarScript.Instance.gameObject;
            //    Instantiate(bossPrefab.gameObject, randomPoint.position, Quaternion.identity, transform.parent);
                
            //}
            //else
            //{
                // Data Chosen At Random
                CustomerData randomCustomerType = currentWave.typeOfCustomers[Random.Range(0, currentWave.typeOfCustomers.Count)];
                FoodData randomFoodChoice = DemandRandomFood();

                // Attach Data to customerPrefab
                customerPrefab.CustomerData = randomCustomerType;
                customerPrefab.FoodChoice = randomFoodChoice;

                // Choose a random spawn point
                randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(customerPrefab.gameObject, randomPoint.position, Quaternion.identity, transform.parent);
            //}

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

    private void GenerateWave() {
        waveCount++;
        var wave = new Wave();
        wave.waveName = "Wave " + waveCount;
        enemiesPerWave += waveCount < 3 ? 3 : waveCount < 6 ? 2 : 1;
        wave.noOfEnemies = enemiesPerWave;
        spawnMultiplier += 0.1f;
        wave.spawnInterval = Mathf.Max(0.5f, 1.5f - 1f * spawnMultiplier);
        for (int i = 0; i < possibleCustomers.Count; i++)
        {
            if (waveCount + 1 >= i) wave.typeOfCustomers.Add(possibleCustomers[i]);
            else break;
        }
        for (int i = 0; i < possibleFood.Count; i++)
        {
            if (waveCount + 2 >= i) wave.typesOfFood.Add(possibleFood[i]);
            else break;
        }
        for (int i = 0; i < wave.typesOfFood.Count; i++)
        {
            wave.foodWeights.Add(Random.Range(1, 5));
        }
        for (int i = 1; i < wave.foodWeights.Count; i++)
        {
            wave.foodWeights[i] += wave.foodWeights[i - 1];
        }
        //if (waveCount % 5 == 0)
        //{
        //    //Boss Wave
        //    wave.waveName = "Boss Wave " + (waveCount / 5);
        //    wave.noOfEnemies = (waveCount / 5);
        //}
        nextWaveWait = wave.noOfEnemies * 0.65f;
        currentWave = wave;
        if (waveCount > 1) WaveBroadcast.Instance.BroadcastWave(currentWave.waveName);
    }
    private void DemandRandomFoods(int size, Dictionary<FoodData, int> foodRequire)
    {
        for (int i = 0; i < size; i++)
        {
            var food = DemandRandomFood();
            if (!foodRequire.ContainsKey(food)) 
            {
                foodRequire.Add(food, 0);
            }
            foodRequire[food]++;
        }
    }
    private FoodData DemandRandomFood()
    {
        int randomVal = Random.Range(0, currentWave.foodWeights[currentWave.foodWeights.Count - 1]);
        for (int i = 0; i < currentWave.foodWeights.Count; i++)
        {
            if (randomVal <= currentWave.foodWeights[i]) return currentWave.typesOfFood[i];
        }
        return currentWave.typesOfFood[currentWave.foodWeights.Count - 1];
    }
}
