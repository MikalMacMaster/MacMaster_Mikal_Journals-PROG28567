using UnityEngine;
using UnityEngine.InputSystem;

public class DotProductExercise : MonoBehaviour
{
    public float redAngle = 0f;
    public float blueAngle = 90f;

    Vector3 AngleToVector(float degrees)
    {
        // I convert to radians because Mathf.Cos and Mathf.Sin expect them
        float rad = degrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    void Update()
    {
        Vector3 redVector = AngleToVector(redAngle);
        Vector3 blueVector = AngleToVector(blueAngle);

        // both vectors have length 1, so the lines end on the unit circle
        Debug.DrawLine(Vector3.zero, redVector, Color.red);
        Debug.DrawLine(Vector3.zero, blueVector, Color.blue);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // dot product by hand: A.x * B.x + A.y * B.y
            float dot = redVector.x * blueVector.x + redVector.y * blueVector.y;
            Debug.Log("Dot: " + dot);
        }
    }


}


