using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float damage = 25f;
    private const float TotalHealthAmount = 200f;
    private float healthAmount = 200f;
    [SerializeField] private BossHealthUI bossHealthUI;
    
    private const string KillerId = "Killer";
    
    // Let's See if boss is triggered with Bullet
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(KillerId))
        {
            Debug.Log("Bullet Just Hit Boss!");
            var firePowerHit = other.gameObject.GetComponent<FireBulletHit>().firePower;
            Debug.Log($"Fire Power is: {firePowerHit}");
            healthAmount -= firePowerHit;
            Debug.Log($"*** Boss Health After Getting Hit: {healthAmount}");
            
            // Refer the boss health UI abd update the health
            bossHealthUI.UpdateHealth(healthAmount / TotalHealthAmount);
            
            if (healthAmount <= 0)
            {
                // kill boss and reset
                Debug.Log("(( Boss is Dying ))");
                
                BoundaryScript.Instance.DeactivateBoundary();
                
                // We need to show pop over menu here
                PowerUpsToast.Instance.ShowPowerUpsToast(); // When the boss is dead let's give player this toast

                ReturnToPool();
            }
        }
    }
    
    private void ReturnToPool()
    {
        var gameObjectReturn = gameObject;
        EnemyPooler.Instance.ReturnEnemyToPool(gameObjectReturn.tag, gameObjectReturn);
    }
    
}
