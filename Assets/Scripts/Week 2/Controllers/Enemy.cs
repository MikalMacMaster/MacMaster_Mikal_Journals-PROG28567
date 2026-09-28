using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;

    public float moveSpeed = 1f;
    public float maxSpeed = 5f;
    public float accelerationTime = 2f;

    private Vector3 velocity;

    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        float acceleration = maxSpeed / accelerationTime;

        if (direction != Vector3.zero)
        {
            velocity += direction * acceleration * Time.deltaTime;

            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        else
        {
            if (velocity != Vector3.zero)
            {
                velocity -= velocity.normalized * acceleration * Time.deltaTime;
            }
        }

        transform.position += velocity * Time.deltaTime;
    }
}