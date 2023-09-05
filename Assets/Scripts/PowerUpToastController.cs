using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpToastController : MonoBehaviour
{

    private readonly float newSpeedIncrement = 5f;
    private float currentPlayerSpeed;

    private readonly float newHealthIncrement = 40f;
    private float currentHealth;
    
    // Refer the PlayerMoveScript to get the current speed of player
    [SerializeField] private PlayerMovement playerMovement;
    
    // Refer the PlayerHealthDie Script to access the amount of health
    [SerializeField] private PlayerHealthDie playerHealthDie;
    
    public void IncreaseSpeedPowerUp()
    {
        currentPlayerSpeed = playerMovement.speed;
        
        Debug.Log($"Current Speed is: {currentPlayerSpeed}");
        
        Debug.Log("Increasing Speed!");
        currentPlayerSpeed += newSpeedIncrement;
        
        Debug.Log($"Updated Speed is: {currentPlayerSpeed}");
        
        // Let's assign it back to player
        playerMovement.speed = currentPlayerSpeed;
        
        // After Incrementing Speed Let's Turn off the Toast
        PowerUpsToast.Instance.HidePowerUpsToast();
    }

    public void IncreaseHealth()
    {
        currentHealth = playerHealthDie.healthAmount; // store the current of health
        Debug.Log($"Current Health is: {currentHealth}");
        currentHealth += newHealthIncrement;
        Debug.Log($"Updated Health is: {currentHealth}");
        // Let's assign new health
        playerHealthDie.healthAmount = currentHealth;

        EventController.RaiseManageHealthFunction(playerHealthDie.healthAmount / playerHealthDie.totalHealthAmount);
        PowerUpsToast.Instance.HidePowerUpsToast();
    }
}
