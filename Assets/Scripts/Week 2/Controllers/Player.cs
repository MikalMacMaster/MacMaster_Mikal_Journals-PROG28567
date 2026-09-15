using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;
    
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
           StartCoroutine(SpawnBombAtOffset(Vector3.up));
    }

    IEnumerator SpawnBombAtOffset (Vector3 inOffset)
    {
        yield return new WaitForSeconds(3f);
            
        Instantiate (bombPrefab, transform.position + inOffset, Quaternion.identity);
    }

    //void SpawnBombAtOffset(Vector3 inOffset)
    //{
    //    Vector3 playerPosition = transform.position;

    //    Vector3 SpawnPosition = playerPosition + inOffset;

    //    Instantiate(bombPrefab, SpawnPosition, Quaternion.identity);
    //}
}
