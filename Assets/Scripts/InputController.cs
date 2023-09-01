using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;

    // Delegate and event for broadcasting joystick movement
    public delegate void JoystickInputDelegate(Vector2 movement); // Delegate to Store our function that will be fired if joyStick is moved
    public static event JoystickInputDelegate OnJoystickMoved; // Refer the function

    private void Update()
    {
        Vector2 movement = new Vector2(joystick.Horizontal, joystick.Vertical);
        
        // Broadcast movement
        OnJoystickMoved?.Invoke(movement);
    } 
}