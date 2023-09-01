using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator; // will enable anim on attack
    [SerializeField] private float swordSlashSpeed;
    [SerializeField] private float demage;

    private float _timeUntilSlash; // will tell us how much time gap for next slash
    
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int AttackUp = Animator.StringToHash("AttackUp");

    private void Update()
    {
        if (_timeUntilSlash <= 0f && transform.position.x < 0) // if player is moving left
        {
            animator.SetTrigger(Attack);
            _timeUntilSlash = swordSlashSpeed; // resetting the time
        }

        if (_timeUntilSlash <= 0f && transform.position.y > 0) // player is moving upward
        {
            animator.SetTrigger(AttackUp);
            _timeUntilSlash = swordSlashSpeed; // resetting the time
        }
        else
        {
            _timeUntilSlash -= Time.deltaTime; // lower the time with each frame.
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("99"))
        {
            Debug.Log($"Enemy Hit, Tag is: {other.tag}");
        }
    }*/
}
