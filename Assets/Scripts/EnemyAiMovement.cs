using UnityEngine;

public class EnemyAiMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    /*[SerializeField] private float rotationSpeed;*/
    [SerializeField] private Rigidbody2D enemyRigidbody2D;

    private Transform playerTransform;
    public Vector2 TargetDirection { get; private set; }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        SetVelocityEnemy();
    }

    private void UpdateTargetDirection()
    {
        if (playerTransform != null)
        {
            Vector2 tempDistanceDiff = playerTransform.position - transform.position;
            TargetDirection = tempDistanceDiff.normalized;
        }
        else
        {
            TargetDirection = Vector2.zero;
        }
    }

    private void SetVelocityEnemy()
    {
        if (TargetDirection == Vector2.zero)
        {
            enemyRigidbody2D.velocity = Vector2.zero; // if they are at the same spot, don't apply velocity
        }
        else
        {
            enemyRigidbody2D.velocity = TargetDirection * speed;
        }
    }
}