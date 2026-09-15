using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RowGeneration : MonoBehaviour
{
    public Button generateButton;
    public TMP_InputField squareNumberInput;
    public float squareSize = 1f;
    public float gap = 0.5f;

    void Start()
    {
        generateButton.onClick.AddListener(GenerateRow);
    }

    void GenerateRow()
    {
        int squareCount = int.Parse(squareNumberInput.text);

        float spacing = squareSize + gap;

        for (int i = 0; i < squareCount; i++)
        {
            Vector2 center = new Vector2(i * spacing, 0);
            DrawSquare(center, Color.white);
        }
    }

    void DrawSquare(Vector2 center, Color color)
    {
        float half = squareSize / 2f;

        Vector3 topLeft = new Vector3(center.x - half, center.y + half, 0);
        Vector3 topRight = new Vector3(center.x + half, center.y + half, 0);
        Vector3 bottomLeft = new Vector3(center.x - half, center.y - half, 0);
        Vector3 bottomRight = new Vector3(center.x + half, center.y - half, 0);

        Debug.DrawLine(topLeft, topRight, color, 5f);
        Debug.DrawLine(topRight, bottomRight, color, 5f);
        Debug.DrawLine(bottomRight, bottomLeft, color, 5f);
        Debug.DrawLine(bottomLeft, topLeft, color, 5f);
    }
}