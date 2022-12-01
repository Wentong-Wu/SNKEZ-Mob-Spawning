using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public int poolSize;
    public GameObject[] prefabs;
    
    public List<GameObject> pooledObjects;
    public static ObjectPool SharedInstance;

    private List<int> randomObjList = new List<int>();

    private void Awake()
    {
        SharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i =0; i < poolSize; i++)
        {
            GameObject obj = (GameObject)Instantiate(prefabs[Random.Range(0,prefabs.Length)]);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        /*
        for(int i = 0; i <pooledObjects.Count;i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
        */
        randomObjList.Clear();
        for (int i =0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                randomObjList.Add(i);
            }
        }
        if (randomObjList != null)
        {
            int random = Random.Range(0, randomObjList.Count);
            return pooledObjects[randomObjList[random]];
        }
        return null;
        
    }

    public void ReturnObject(GameObject o)
    {
        o.SetActive(false);
    }
}
