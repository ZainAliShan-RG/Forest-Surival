
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image healthBarPlayer;

    private void OnEnable()
    {
        EventController.ManageHealthPlayer += UpdateHealth; ;
    }
    
    private void UpdateHealth(float value)
    {
        Debug.Log("In Health Updater");
        healthBarPlayer.fillAmount = value;
    }

    private void OnDisable()
    {
        EventController.ManageHealthPlayer -= UpdateHealth;

    }
}
