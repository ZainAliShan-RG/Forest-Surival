using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script moves the 2D player via keyboard as well as mobile phone (joystick)

public class PlayerMovement : MonoBehaviour
{
    
    
    // Speed with which player will move
    [SerializeField] private float speed = 5f;
    
    // RB of Player
    [SerializeField] private Rigidbody2D playerRb;
    // Horizontal Input
    private Vector2 _movement;
    
    // Now we have made the animations:
    // 1. We make animations by selecting player, going into player and then make animations for idle, walk up, down, left and right
    // and then we delete them from animator instead of just idle, then we make a 2d blend tree and add motions by dragging walking animations
    // made float of horizontal and vertical and activate motion of vertical up (1), vertical down (-1), horizontal left (-1), horizontal
    // right (1) and then we make transition from idle to our blend tree and remove transitions and exit times from it
    // then we add another float named speed and make animation going to blend tree when speed > 0.01 (if we move in script)
    // and going back we do condition if speed < 0.01

    
    // Now let's get our animator and and animate our player according to movement against horizontal and vertical
    [SerializeField] private Animator animator;
    
    // Cached field of our animator
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");

    

    private void OnEnable()
    {
        InputController.OnJoystickMoved += HandleMovement;
    }

    private void HandleMovement(Vector2 movement)
    {
        _movement = movement;
        
        animator.SetFloat(Horizontal, _movement.x);
        animator.SetFloat(Vertical, _movement.y);
        animator.SetFloat(Speed, _movement.sqrMagnitude);
    }

    private void OnDisable()
    {
        InputController.OnJoystickMoved -= HandleMovement;
    }


    // To handle physics, doing in Fixed Update
    private void FixedUpdate()
    {
        if (playerRb != null)
        {
            // Move player here
            playerRb.MovePosition(playerRb.position + _movement * (speed * Time.fixedDeltaTime)); // move to current position and add movement and mul by speed
        }
    }
    
    
}
