using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDeath : MonoBehaviour
{
    private const string KillerId = "Killer";
    [SerializeField] private GameObject chickenCollectablePrefab;
    
    [Range(0f, 1f)] // Adding a slider in inspector
    // Let's make the random spawn for chicken
    [SerializeField] private float spawnChance = 0.2f; // % spawn chance
    
    public float damage = 20f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(KillerId))
        {

            ReturnToPool();
            SoundEffectController.Instance.PlayBirdDeathSound();

            // If the random value that is generated is less than the spawnChance
            
            if (Random.value <= spawnChance) 
                Instantiate(chickenCollectablePrefab, other.gameObject.transform.position, Quaternion.identity);
        }
    }
    
    private void ReturnToPool()
    {
        var gameObjectReturn = gameObject;
        EnemyPooler.Instance.ReturnEnemyToPool(gameObjectReturn.tag, gameObjectReturn);
    }
}
