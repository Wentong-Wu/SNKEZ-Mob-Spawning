using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnClick : MonoBehaviour
{
    [SerializeField] private int NumberOfEnemies;
    [SerializeField] Transform[] EnemyPrefab;
    [SerializeField] GameObject[] spawnPoints;
    private ObjectPool pool;
    // Start is called before the first frame update
    void Start()
    {
        pool = FindObjectOfType<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            EnemySpawn();
        }
    }
    public void EnemySpawn()
    {
        int randomSpawn = Random.Range(0, spawnPoints.Length);
        int randomEnemy = Random.Range(0, EnemyPrefab.Length);
        for (int i = 0; i < NumberOfEnemies; i++)
        {
            Enemy p = null;
            if (pool == null)
                p = Instantiate(EnemyPrefab[randomEnemy]).gameObject.GetComponent<Enemy>();
            else
            {
                GameObject newObj = pool.GetObject();
                if (newObj != null)
                    p = newObj.GetComponent<Enemy>();
            }
            if (p != null)
            {
                p.Init(new Vector3(spawnPoints[randomSpawn].transform.position.x, spawnPoints[randomSpawn].transform.position.y, spawnPoints[randomSpawn].transform.position.z + 10));
            }
        }
    }
}
