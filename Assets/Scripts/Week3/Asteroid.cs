using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float maxFloatDistance = 5f;
    public float moveSpeed = 2f;
    public float arrivalDistance = 0.5f;

    private Vector3 targetPosition;

    void Start()
    {
        ChooseNewTarget();
    }

    void Update()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance <= arrivalDistance)
        {
            ChooseNewTarget();
        }
    }

    void ChooseNewTarget()
    {
        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            0f
        ).normalized;

        targetPosition = transform.position + randomDirection * maxFloatDistance;
    }
}