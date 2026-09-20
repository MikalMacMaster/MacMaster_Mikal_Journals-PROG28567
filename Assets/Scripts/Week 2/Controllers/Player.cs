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
}
