using UnityEngine;

public class EnemyGraphicsRotation : MonoBehaviour
{
    [SerializeField] private EnemyAiMovement enemyAiMovement;
    [SerializeField] private float rotationSpeed = 5f;

    private void Update()
    {
        AdjustEnemyGfx();
    }

    private void AdjustEnemyGfx()
    {
        // Determine the target angle based on the TargetDirection
        float targetAngleZ = Mathf.Atan2(enemyAiMovement.TargetDirection.y, enemyAiMovement.TargetDirection.x) * Mathf.Rad2Deg;

        // Rotate the bird to face the direction of the TargetDirection
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngleZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if the bird is moving leftward or rightward and adjust the Y-scale accordingly for flipping
        if (enemyAiMovement.TargetDirection.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
        }
    }
}