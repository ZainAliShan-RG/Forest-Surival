using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsToast : MonoBehaviour
{
    // Get the Power Ups Toast to disable it at first
    [SerializeField] private GameObject powerUpToast;

    #region Singleton
    
    // Using this so that we can access the inactive toast inside the prefab of our enemy

        public static PowerUpsToast Instance;

        private void Awake()
        {
            Debug.Log("PowerUpsToast Awake called!");
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

    #endregion

    private void Start()
    {
        powerUpToast.SetActive(false);
    }
    public void ShowPowerUpsToast()
    {
        powerUpToast.SetActive(true);
    }

    public void HidePowerUpsToast()
    {
        powerUpToast.SetActive(false);
    }
}
