using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    public float bombTrailSpacing = 0.5f;
    public int numberOfTrailBombs = 5;

    
    void Start()
    {
        Debug.Log(NormalizeVector(new Vector2(3, 4)));
        Debug.Log(NormalizeVector(new Vector2(-3, 2)));
        Debug.Log(NormalizeVector(new Vector2(1.5f, -3.5f)));
    }
    void Update()
    {
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
}
