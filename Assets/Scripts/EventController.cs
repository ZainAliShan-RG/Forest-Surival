using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    // ***** Delegate and Event to handle player health *****
    
    public delegate void ManageHealthDelegate(float totalHealthAmount);
    public static event Action <float> ManageHealthPlayer;

    public static void RaiseManageHealthFunction(float totalHealthAmount)
    {
        ManageHealthPlayer?.Invoke(totalHealthAmount);
    }

    // After Certain Time is Passed, Boss must be spawned
    public static Action oneMinutePassed;
}
