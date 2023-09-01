using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandleTimer : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 0f;
    [SerializeField] private bool timeIsRunning = true;
    [SerializeField] private TMP_Text timeTextHolder;
    
    public float TimeAlive => timeRemaining;

    // Start is called before the first frame update
    private void Start()
    {
        timeIsRunning = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (timeIsRunning && timeRemaining >= 0)
        {
            timeRemaining += Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        // Check If One Minute is passed to Stop Other enemies from spawning and To Spawn the Boss!
        if (Mathf.FloorToInt(timeRemaining) == 5)
        {
            // Let's Invoke 
            EventController.oneMinutePassed?.Invoke();
        }
        
    }

    private void DisplayTime(float time)
    {
        time += 1;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeTextHolder.text = $"{minutes:00} : {seconds:00}";
    }
    
    // When player dies, we need to reset the timer
    public void ResetTimerAndStop()
    {
        timeRemaining = 0;
        timeIsRunning = false;
        timeTextHolder.text = "00 : 00";
    }
}
