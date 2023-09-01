using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    // Lets make and isntance of this pooler to be used as singleton
    public static EnemyPooler Instance;

    // We have to take list of Elements/Objects from the user
    // Will have a tag, prefab and the size to be spawned
    // We will make a class for that and will then make list of these objects

    [System.Serializable]
    public class Pools
    {
        public string id;
        public GameObject Prefab;
        public int PoolSize;
    }

    // Let's make List of these Pools
    public List<Pools> EnemyList;

    // Let's make a dictionary of having key (string/tag), value (queue of prefabs)
    
    public Dictionary<string, Queue<GameObject>> DictPool;

    #region Singleton

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #endregion


    // This method is just used to add the take pools from the inspector, then seeing the
    // pools size, instantiating them to that amount and then adding in the queue, and finally
    // populating our Dictionary with that data.
    private void Start()
    {
        DictPool = new Dictionary<string, Queue<GameObject>>();

        // Let's retrieve pools from inspector and insert them into queues
        foreach (var pool in EnemyList)
        {
            // make a queue
            Queue<GameObject> enemyQueue = new Queue<GameObject>();
            for (int i = 0; i < pool.PoolSize; i++)
            {
                GameObject obj = Instantiate(pool.Prefab);
                obj.SetActive(false);
                enemyQueue.Enqueue(obj);
            }
            DictPool.Add(pool.id, enemyQueue);
        }
    }
    // this method will be used, where we recycle enemies from this pool and show it to them. Instantiating is just done at first and then their
    // activity is set to false, whenever we need them we active them, we can also resize the pool upon our choice
    public GameObject SpawnFromEnemyPool(string id)
    {
        if (!CanSpawn(id))
        {
            Debug.LogWarning("Cannot Spawn More Enemies, Limit Exhausted");
            return null;
        }
        if (!DictPool.ContainsKey(id))
        {
            Debug.LogWarning($"Enemy with tag: {id} does not exit!");
            return null;
        }
        // Let's take enemy from our dic and dequeue it from dict
        GameObject enemyToSpawn = DictPool[id].Dequeue();
        
        enemyToSpawn.SetActive(true);
        
        // We also need to enqueue this GO back to dict so that dict does not get empty eventually
        
        //DictPool[id].Enqueue(enemyToSpawn);

        return enemyToSpawn; // let's catch this inside our EnemySpawner
    }
    
    // Function to get enemies back from the killer and into the dict of queue
    public void ReturnEnemyToPool(string id, GameObject enemy)
    {
        if (!DictPool.ContainsKey(id))
        {
            Debug.LogWarning($"This Enemy Object That you are trying to return is not managed by this pool -> {id}");
            return;
        }
        enemy.SetActive(false);
        DictPool[id].Enqueue(enemy);
    }
    
    
    // to check if you can spawn more enemies based on the current PoolSize for each enemy type.
    // need to check if there's any inactive (i.e., non-spawned) enemy in your pool for a given enemy type (id).
    // One way to approach this is to loop through the queue for that enemy type and count the inactive ones.
    // If you find at least one inactive enemy, you can spawn.

    public bool CanSpawn(string id)
    {
        // Check if the given enemy type exists in the pool dictionary
        if (!DictPool.ContainsKey(id))
        {
            Debug.LogWarning($"Enemy with tag: {id} does not exist!");
            return false;
        }

        // Get the enemy queue for the given enemy type
        Queue<GameObject> enemyQueue = DictPool[id];
    
        // Loop through the queue and check for inactive enemies
        int inactiveCount = 0;
        foreach(GameObject enemy in enemyQueue)
        {
            if (!enemy.activeInHierarchy)
            {
                inactiveCount++;
            }
        }

        // If we find at least one inactive enemy, return true. Else, return false.
        return inactiveCount > 0;
    }
    
    public GameObject SpawnFromEnemyPoolBoss(string id)
    {
        if (!CanSpawn(id))
        {
            Debug.LogWarning("Cannot Spawn More Enemies, Limit Exhausted");
            return null;
        }
        if (!DictPool.ContainsKey(id))
        {
            Debug.LogWarning($"Enemy with tag: {id} does not exit!");
            return null;
        }
        // Let's take enemy from our dic and dequeue it from dict
        GameObject enemyToSpawn = DictPool[id].Dequeue();
        
        enemyToSpawn.SetActive(true);
        
        // We also need to enqueue this GO back to dict so that dict does not get empty eventually
        
        DictPool[id].Enqueue(enemyToSpawn);

        return enemyToSpawn; // let's catch this inside our EnemySpawner
    }
}