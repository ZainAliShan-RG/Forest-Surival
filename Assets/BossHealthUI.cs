using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealthUI : MonoBehaviour
{
    
    [SerializeField] private Image heathBarBoss;
    // Update is called once per frame
    public void UpdateHealth(float value)
    {
        Debug.Log("Updating Boss Health!");
        heathBarBoss.fillAmount = value;
    }
}
