using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDie : MonoBehaviour
{
    private const string EnemyId = "99";
    private const string ChickenCollectableId = "CollectableChicken";
    private const string BossId = "88";
    
    private readonly float chickenCollectableHealth = 25f;
    
    // Referencing the Timer Script to reset the timer when player dies
    [SerializeField] private HandleTimer timer;
    // Referencing the GameOverPopUp Script to show the menu and timer
    [SerializeField] private GameOverPopUp gameOverPopUp;

    public float healthAmount = 100f;

    public const float TotalHealthAmount = 100f;
    
    private void OnTriggerEnter2D(Collider2D other) // If my player touches the enemy bird
    {
        if (other.gameObject.CompareTag(EnemyId))
        {
            var enemyBirdHit = other.gameObject.GetComponent<EnemyDeath>().damage;
            healthAmount -= enemyBirdHit;
            Debug.Log($"!!! Player Health After Getting Hit: {healthAmount}");
            EventController.RaiseManageHealthFunction(healthAmount / TotalHealthAmount);
            KillPlayerAndResetTimer();
        }

        if (other.gameObject.CompareTag(ChickenCollectableId)) // If my player goes over the chicken collectable
        {
            if (healthAmount < 100)
            {
                healthAmount += chickenCollectableHealth;
                healthAmount = Mathf.Clamp(healthAmount, 0, 100f);
                Debug.Log($"!!! Player Health After Picking Chicken: {healthAmount}");
                EventController.RaiseManageHealthFunction(healthAmount / TotalHealthAmount);
                
                Destroy(other.gameObject); // Destroy that chicken piece after collecting
            }
            else
            {
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag(BossId)) // If my player triggered with boss
        {
            var bossHit = other.gameObject.GetComponent<Boss>().damage;
            healthAmount -= bossHit;
            Debug.Log($"!!! Player Health After Getting Hit From Boss: {healthAmount}");
            EventController.RaiseManageHealthFunction(healthAmount / TotalHealthAmount);
            KillPlayerAndResetTimer();
        }
        
    }

    private void KillPlayerAndResetTimer()
    {
        if (healthAmount <= 0)
        {
            gameOverPopUp.ShowGameOverMenu(timer.TimeAlive); // Show the pop up when player dies
            timer.ResetTimerAndStop(); // reset the timer and stop it when player dies
            Destroy(gameObject);
        }
    }
}

// Health is related to enemy, it should be inside player
// Heath of player should be inside player
