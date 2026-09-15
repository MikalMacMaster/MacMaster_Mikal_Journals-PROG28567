using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class SquareSpawner : MonoBehaviour
{
    public float squareSize = 1f;
    public List<Vector2> spawnedSquares = new List<Vector2>();

    void Update()
    {
        Debug.Log("running");

       if (Mouse.current.leftButton.wasPressedThisFrame)
       {

        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector3 worldPos3D = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
        Vector2 clickPos = new Vector2(worldPos3D.x, worldPos3D.y);

        Debug.Log("clicked");
        spawnedSquares.Add(clickPos);

        DrawSquare(clickPos, Color.white);
       } 
    }

    void DrawSquare(Vector2 center, Color color)
    {
        float half = squareSize / 2f;

        Vector3 topLeft = new Vector3(center.x - half, center.y + half, 0);
        Vector3 topRight = new Vector3(center.x + half, center.y + half, 0);
        Vector3 bottomLeft = new Vector3(center.x - half, center.y - half, 0);
        Vector3 bottomRight = new Vector3(center.x + half, center.y - half, 0);

        Debug.DrawLine(topLeft, topRight, color);
        Debug.DrawLine(topRight, bottomRight, color);
        Debug.DrawLine(bottomRight, bottomLeft, color);
        Debug.DrawLine(bottomLeft, topLeft, color);
    }
}