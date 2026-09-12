using UnityEngine;
using UnityEngine.InputSystem;

public class Pipeline : MonoBehaviour
{
    public bool isDrawing = false;
    public Vector2 previousPoint;
    public float timer = 0f;
    public float totalLength = 0f;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isDrawing = true;
            timer = 0f;
            totalLength = 0f;

            Vector2 screenPos = Mouse.current.position.ReadValue();
            Vector3 worldPos3D = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
            previousPoint = new Vector2(worldPos3D.x, worldPos3D.y);
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDrawing = false;
            Debug.Log("Total pipeline length: " + totalLength);
        }

        if (isDrawing)
        {
            timer = timer + Time.deltaTime;

            if (timer >= 0.1f)
            {
                timer = 0f;

                Vector2 screenPos = Mouse.current.position.ReadValue();
                Vector3 worldPos3D = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
                Vector2 newPoint = new Vector2(worldPos3D.x, worldPos3D.y);

                Debug.DrawLine(new Vector3(previousPoint.x, previousPoint.y, 0), new Vector3(newPoint.x, newPoint.y, 0), Color.white, 1000f);

                // distance formula: sqrt(dx^2 + dy^2)
                float dx = newPoint.x - previousPoint.x;
                float dy = newPoint.y - previousPoint.y;
                float distance = Mathf.Sqrt(dx * dx + dy * dy);
                totalLength = totalLength + distance;

                previousPoint = newPoint;
            }
        }
    }
}