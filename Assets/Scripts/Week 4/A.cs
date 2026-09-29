using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class A : MonoBehaviour
{
    public List<float> angles = new();
    private int currentAngle = 0;

    private void Start()
    {
        for (int i = 0; i < 10; i++) 
        {
            float randomAngle = Random.Range(0f, 360f);
            angles.Add(randomAngle);

        }
    }

    public float radius = 3f;
    public Vector3 circlePosition = Vector3.zero;
    public float duration = 2f;

    private float timer = 0f;

    private void Update()
    {
        // Press Space to move to the next angle
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            currentAngle = (currentAngle + 1) % angles.Count;
        }

        // Automatically change angle after the duration
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            currentAngle = (currentAngle + 1) % angles.Count;

            timer = 0f;
        }

        // Get the current angle
        float angle = angles[currentAngle];

        // Convert degrees to radians
        float radians = angle * Mathf.Deg2Rad;

        // Calculate the point on the circle
        float x = Mathf.Cos(radians) * radius;
        float y = Mathf.Sin(radians) * radius;

        Vector3 point = circlePosition + new Vector3(x, y, 0f);

        // Draw a line from the origin to the point
        Debug.DrawLine(circlePosition, point, Color.green);
    }
}
