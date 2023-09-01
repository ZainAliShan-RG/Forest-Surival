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

    [SerializeField] private GameObject boundaryPrefab;

    // The boundary collider
    private CircleCollider2D boundaryCollider;  // Directly assign in Unity inspector


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
        BoundaryScript.OnBoundaryCreated += UpdateBoundary;
    }

    private void UpdateBoundary(GameObject newBoundary)
    {
        boundaryCollider = newBoundary.GetComponent<CircleCollider2D>();
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
        BoundaryScript.OnBoundaryCreated -= UpdateBoundary;
    }
    

    private void Start()
    {
        boundaryCollider = boundaryPrefab.GetComponent<CircleCollider2D>();
        // Now we're not fetching it using a tag, but if you wanted to, you can add an additional check.
        if (!boundaryCollider)
        {
            Debug.Log("Unable to Find Boundary Collider!");
        }

        if (!playerRb)
        {
            Debug.Log("Player RB not found!");
        }
    }
    private void FixedUpdate()
    {
        Vector2 intendedPosition = playerRb.position + _movement * (speed * Time.fixedDeltaTime);

        if (boundaryCollider && boundaryCollider.gameObject.activeInHierarchy)
        {
            float distanceFromBoundaryCenter = Vector2.Distance(intendedPosition, boundaryCollider.transform.position);
            float maxAllowedDistance = boundaryCollider.radius - playerRb.GetComponent<CircleCollider2D>().radius;

            if (distanceFromBoundaryCenter > maxAllowedDistance)
            {
                Vector2 fromBoundaryToPlayer = intendedPosition - (Vector2)boundaryCollider.transform.position;
                fromBoundaryToPlayer.Normalize();

                intendedPosition = (Vector2)boundaryCollider.transform.position + fromBoundaryToPlayer * maxAllowedDistance;
            }
        }

        if (playerRb != null)
        {
            playerRb.MovePosition(intendedPosition);
        }
    }
}
