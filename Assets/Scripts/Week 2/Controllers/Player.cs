using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public GameObject powerupPrefab;
    public List<Transform> asteroidTransforms;

    public float bombTrailSpacing = 0.5f;
    public int numberOfTrailBombs = 5;

    public float moveSpeed = 1f;
    public float maxSpeed = 5f;
    public float accelerationTime = 2f;

    public float radarRadius = 3f;
    public int radarSideCount = 8;

    public float powerupRadius = 2f;
    public int powerupCount = 5;

    private Vector3 velocity;
    
    void Start()
    {
        Debug.Log(NormalizeVector(new Vector2(3, 4)));
        Debug.Log(NormalizeVector(new Vector2(-3, 2)));
        Debug.Log(NormalizeVector(new Vector2(1.5f, -3.5f)));
    }
    void Update()
    {
        EnemyRadar(radarRadius, radarSideCount);
        PlayerMovement();

        if (Keyboard.current.bKey.wasPressedThisFrame)
           StartCoroutine(SpawnBombAtOffset(Vector3.up));

        if (Keyboard.current.tKey.wasPressedThisFrame)
    
        SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs);

        if (Keyboard.current.cKey.wasPressedThisFrame)

        SpawnBombOnRandomCorner(3f);

        if (Keyboard.current.wKey.wasPressedThisFrame)

        WarpPlayer(enemyTransform, 0.5f);

        if (Keyboard.current.rKey.wasPressedThisFrame) 
        {
            Debug.Log("R pressed");
            DetectAsteroids(5f, asteroidTransforms);
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
            SpawnPowerups(powerupRadius, powerupCount);

    }

    private void PlayerMovement()
    {
        Vector3 input = Vector3.zero;

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            input += Vector3.left;
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            input += Vector3.right;
        }

        if (Keyboard.current.upArrowKey.isPressed)
        {
            input += Vector3.up;
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            input += Vector3.down;
        }

        float acceleration = maxSpeed / accelerationTime;
        if (input != Vector3.zero)
        {
            velocity += input.normalized * acceleration * Time.deltaTime;

            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        else
        {
            if(velocity != Vector3.zero)
            {
                velocity -= velocity.normalized * acceleration * Time.deltaTime;
            }
        }

        transform.position += velocity * Time.deltaTime;
    }


    IEnumerator SpawnBombAtOffset (Vector3 inOffset)
    {
        yield return new WaitForSeconds(3f);
            
        Instantiate (bombPrefab, transform.position + inOffset, Quaternion.identity);
    }

    Vector2 NormalizeVector(Vector2 inVector)
    {
        float magnitude = inVector.magnitude;
        Vector2 outVector = new Vector2(inVector.x / magnitude, inVector.y / magnitude);
        return outVector; 
    }

    //void SpawnBombAtOffset(Vector3 inOffset)
    //{
    //    Vector3 playerPosition = transform.position;

    //    Vector3 SpawnPosition = playerPosition + inOffset;

    //    Instantiate(bombPrefab, SpawnPosition, Quaternion.identity);
    //}

    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
    {
        for (int i = 1; i <= inNumberOfBombs; i++)
        {
            Vector3 offset = -transform.up * inBombSpacing * i;

            Instantiate(bombPrefab, transform.position + offset, Quaternion.identity);
        }
    }

    public void SpawnBombOnRandomCorner(float inDistance)
    {
        int randomCorner = Random.Range(0, 4);

        Vector3 direction;

        if (randomCorner == 0)
        {
            direction = transform.up + transform.right;
        }
        else if (randomCorner == 1)
        {
            direction = transform.up - transform.right;
        }
        else if (randomCorner == 2)
        {
            direction = -transform.up + transform.right;
        }
        else
        {
            direction = -transform.up - transform.right;
        }

        direction = direction.normalized;

        Vector3 spawnPosition = transform.position + direction * inDistance;

        Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
    }

    public void WarpPlayer(Transform target, float ratio)
    {
        ratio = Mathf.Clamp01(ratio);

        transform.position = Vector3.Lerp(transform.position, target.position, ratio);
    }

    public void DetectAsteroids(float inMaxRange, List<Transform> inAsteroids)
    {
        foreach (Transform asteroid in inAsteroids)
        {
            float distance = Vector3.Distance(transform.position, asteroid.position);

            if (distance <= inMaxRange)
            {
                Vector3 direction = (asteroid.position - transform.position).normalized;

                Vector3 endPosition = transform.position + direction * 2.5f;

                Debug.DrawLine(transform.position, endPosition, Color.green, 2f);
            }
        }
    }

    public void EnemyRadar(float radius, int circlePoints)
    {
        // I pick the colour first by checking the enemy's distance
        float distance = Vector3.Distance(transform.position, enemyTransform.position);
        Color circleColour = Color.green;

        if (distance <= radius)
        {
            circleColour = Color.red;
        }

        float stepAngle = 360f / circlePoints * Mathf.Deg2Rad;

        for (int i = 0; i < circlePoints; i++)
        {
            float startAngle = stepAngle * i;
            float endAngle = stepAngle * (i + 1);

            Vector3 startPoint = transform.position + new Vector3(Mathf.Cos(startAngle), Mathf.Sin(startAngle), 0f) * radius;
            Vector3 endPoint = transform.position + new Vector3(Mathf.Cos(endAngle), Mathf.Sin(endAngle), 0f) * radius;

            Debug.DrawLine(startPoint, endPoint, circleColour);
        }
    }

    public void SpawnPowerups(float radius, int numberOfPowerups)
    {
        // I split 360 degrees evenly so every powerup is the same angle apart
        float stepAngle = 360f / numberOfPowerups * Mathf.Deg2Rad;

        for (int i = 0; i < numberOfPowerups; i++)
        {
            float angle = stepAngle * i;

            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;

            Instantiate(powerupPrefab, transform.position + offset, Quaternion.identity);
        }
    }
}