using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyerBoundary : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Killer"))
        {
            other.gameObject.GetComponent<FireBulletHit>().KillBullet();
        }
    }
}
