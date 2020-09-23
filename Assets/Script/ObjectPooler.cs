﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    //Unutk memuat prefab pool;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Singleton untuk object pooler
    public static ObjectPooler Instance;

    // Singleton untuk object pooler
    private void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        AddIntoPool();
    }

    void AddIntoPool()
    {
        pools.ForEach((pool) =>
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            int i = 0;
            while (i < pool.size)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                i++;
            }

            poolDictionary.Add(pool.tag, objectPool);

        });
    }

    // Spawn from Pool digunakna untuk mengambil object dari Pool
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        // Cek jika dalam Pool dictionary terdapat object yang direquest
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Object with tag " + tag + " doesnt exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
