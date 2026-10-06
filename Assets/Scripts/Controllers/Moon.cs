using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;

    public float orbitRadius = 3f;
    public float orbitSpeed = 90f;

    private float angle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(orbitRadius, orbitSpeed, planetTransform);
    }

    public void OrbitalMotion(float radius, float speed, Transform target)
    {
        // I keep a running angle in degrees and turn it into a point on the circle
        angle += speed * Time.deltaTime;

        float radians = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f) * radius;

        transform.position = target.position + offset;
    }
}