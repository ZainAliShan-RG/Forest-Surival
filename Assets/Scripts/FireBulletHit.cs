using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletHit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject hitEffect;
    
    public float firePower = 10f; // power with which bullet will kill the boss

    private void OnCollisionEnter2D(Collision2D other) // If my FireBullet collides with anything
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f); // Delayed Destroying Effect
        Destroy(gameObject); // Destroying Bullet
    }
}
