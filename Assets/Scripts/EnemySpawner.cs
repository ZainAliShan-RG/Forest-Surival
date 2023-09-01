using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    // Get Enemy Prefab from Inspector
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bossPrefab;
    
    // Target our player for the enemy to chase him
    private Transform playerTransform;
    
    // Radius around player in which enemies will be spawned
    [SerializeField] private float enemySpawnRadius;
    
    private float spawnTime = 0.0f;
    private float whenToSpawn = 0.0f;
    

    private void Start()
    {
        whenToSpawn = Random.Range(1, 5); // Spawn enemies in 1-5 secs of range randomly
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform == null)
            return;
        spawnTime += Time.deltaTime; // increase time with each frame so that it reaches threshold (when to spawn)

        if (spawnTime >= whenToSpawn)
        {
            // Call Spawn Enemies Function here
            SpawnEnemies(enemyPrefab.tag);
            // set spawn time to zero for next iteration
            spawnTime = 0;
            whenToSpawn = Random.Range(1, 5); // Calculate spawn wait threshold for next iteration
        }
    }

    private void SpawnEnemies(string id)
    {
        // Stop spawning enemies if we have some boss available/active in the scene
        var isInActive = EnemyPooler.Instance.CanSpawn(bossPrefab.tag);

        if (isInActive) // If my boss prefab is inactive in the scene, Simple Enemy then can spawn
        {
            var enemyClone = EnemyPooler.Instance.SpawnFromEnemyPool(id);
            if (enemyClone != null)
            {
                Vector3 spawnPosition = GetRandomPositionAroundPlayer(playerTransform.position, enemySpawnRadius);
        
                // Adding distance check
                while (!IsValidSpawnPosition(spawnPosition))
                {
                    spawnPosition = GetRandomPositionAroundPlayer(playerTransform.position, enemySpawnRadius);
                }

                Vector3 updatedSpawnPosition = enemyClone.transform.position;
                updatedSpawnPosition.z = 0;
                updatedSpawnPosition.y = enemyClone.transform.position.y;
                enemyClone.transform.position = spawnPosition;

            }
            else
            {   
                Debug.LogWarning("Can't Clone More, Limit Reached!");
            } 
        }
    }

// Check if the spawn position is valid i.e. not too close to other enemies
    private bool IsValidSpawnPosition(Vector3 position)
    {
        foreach (var enemy in EnemyPooler.Instance.DictPool[enemyPrefab.tag])
        {
            if (enemy.activeInHierarchy && Vector3.Distance(position, enemy.transform.position) < 6f)
            {
                return false;
            }
        }
        return true;
    }


    // To get random position in a given radius around the player
    private Vector3 GetRandomPositionAroundPlayer(Vector3 playerPosition, float radius)
    {
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        float x = radius * Mathf.Cos(randomAngle);
        float y = radius * Mathf.Sin(randomAngle); // Since it's 2D, I assume y is what you want.
        return new Vector3(playerPosition.x + x, playerPosition.y + y, 0); // Here the z value is set to 0
    }
    
    // ********************************* SPAWN BOSS *********************************

    #region BossSpawn

        // Let's Spawn Boss Here
        private void OnEnable()
        {
            EventController.oneMinutePassed += HandleOneMinutePassed;
        }

        private void HandleOneMinutePassed()
        {
            SpawnBoss();
        }

        private void SpawnBoss()
        {
            var bossClone = EnemyPooler.Instance.SpawnFromEnemyPoolBoss(bossPrefab.tag); // assuming the ID for the boss prefab is "Boss"
        
            if (bossClone != null)
            {
                bossClone.GetComponent<BoundaryScript>().isBoss = true;
                Vector3 spawnPosition = GetRandomPositionAroundPlayer(playerTransform.position, enemySpawnRadius);

                spawnPosition.z = 0;
                bossClone.transform.position = spawnPosition;
            }
            else
            {   
                Debug.LogWarning("Cannot spawn the boss, limit reached!");
            } 
        }
        
        private void OnDisable()
        {
            
            EventController.oneMinutePassed -= HandleOneMinutePassed;
        }

    #endregion
    
    
}
