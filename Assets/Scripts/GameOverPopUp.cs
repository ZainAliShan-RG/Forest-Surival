using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverPopUp : MonoBehaviour
{
    // Get the PopUp Menu to disable here and then enable it when play dies
    [SerializeField] private GameObject gameOverPopUpMenu;
    // Reference the Text Mesh that will be updated
    [SerializeField] private TMP_Text gameOverScoreTextHolder;
    
    // Start is called before the first frame update
    private void Start()
    {
        gameOverPopUpMenu.SetActive(false);
    }
    
    // Function to show pop up
    public void ShowGameOverMenu(float survivalTime)
    {
        float minutes = Mathf.FloorToInt(survivalTime / 60);
        float seconds = Mathf.FloorToInt(survivalTime % 60);
        
        // Update the text mesh
        gameOverScoreTextHolder.text = $"Your Survival Time is: {minutes:00} : {seconds:00}";
        gameOverPopUpMenu.SetActive(true);
    }
}
