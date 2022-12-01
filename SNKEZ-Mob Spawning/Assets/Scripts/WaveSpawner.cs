using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int numberOfEnemies;
    public float waitBeforeSpawn;
}
public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    [SerializeField] Transform[] spawnPoints;
    private ObjectPool pool;
    bool canSpawn = true;
    private Wave currentWave;
    private int currentWaveNumber = 0;
    private float nextSpawnTime;
    void Start()
    {
        currentWaveNumber = 0;
        pool = FindObjectOfType<ObjectPool>();
    }

    void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(!canSpawn && totalEnemies.Length == 0 && currentWaveNumber+1 != waves.Length)
        {
            
            SpawnNextWave();
        }
    }

    void SpawnNextWave()
    {
        currentWaveNumber++;
    }
    void SpawnWave()
    {
        if (currentWave.numberOfEnemies != 0 && nextSpawnTime < Time.time)
        {
            int randomSpawn = Random.Range(0, spawnPoints.Length);
            Enemy p = null;
            if (pool != null)
            {
                GameObject newObj = pool.GetObject();
                if (newObj != null)
                    p = newObj.GetComponent<Enemy>();
            }
            if (p != null)
            {
                p.Init(new Vector3(spawnPoints[randomSpawn].transform.position.x, spawnPoints[randomSpawn].transform.position.y, spawnPoints[randomSpawn].transform.position.z + 10));
            }
            currentWave.numberOfEnemies--;
            nextSpawnTime = Time.time + currentWave.waitBeforeSpawn;
        }
        if (currentWave.numberOfEnemies == 0)
            canSpawn = false;
    }
}
