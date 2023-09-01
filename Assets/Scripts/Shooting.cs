using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint; // from where bullet will be spawned
    [SerializeField] private GameObject bulletPrefab; // the bullet actually
    [SerializeField] private Transform playerTransform; // Player

    public float bulletForce = 20f;

    private Vector3 lastPosition;
    private Vector3 currentDirection; // default direction

    private void Start()
    {
        if (playerTransform != null)
        {
            lastPosition = playerTransform.position;
            InvokeRepeating(nameof(Shoot), 0.5f, 0.5f);
        }
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            Vector3 movementDirection = (playerTransform.position - lastPosition).normalized;
            if (movementDirection != Vector3.zero)
            {
                currentDirection = movementDirection;
            }
            lastPosition = playerTransform.position;
        }
    }
    
    private void Shoot()
    {
        if (currentDirection != Vector3.zero) // only fire bullets if player is moving
        {
            GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();

            rb.AddForce(currentDirection * bulletForce, ForceMode2D.Impulse);
            
            // Play Fireball Sound
            //SoundEffectController.Instance.PlayFireBallSound();
        }
    }
}