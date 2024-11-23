using UnityEditor;
using UnityEngine;

public partial class LevelDesignerWindow : EditorWindow
{
    private int movesCount = 0; // Переменная для количества ходов

    private void DrawGrid()
    {
        Rect gridArea = GUILayoutUtility.GetRect(splitterPosition, position.height);
        float gridWidth = gridSize * cellSize;
        float gridHeight = gridSize * cellSize;

        float startX = gridArea.x + (gridArea.width - gridWidth) / 2f;
        float startY = gridArea.y + (gridArea.height - gridHeight) / 2f;

        // Отрисовка ячейки Moves выше сетки
        DrawMovesField(startX - 50f, startY - cellSize);

        Handles.color = Color.white;
        for (int x = 0; x <= gridSize; x++)
        {
            float lineX = startX + x * cellSize;
            Handles.DrawLine(new Vector3(lineX, startY, 0), new Vector3(lineX, startY + gridHeight, 0));
        }

        for (int y = 0; y <= gridSize; y++)
        {
            float lineY = startY + y * cellSize;
            Handles.DrawLine(new Vector3(startX, lineY, 0), new Vector3(startX + gridWidth, lineY, 0));
        }

        DrawBoardGridCells(startX, startY);

        DrawHotBarTools(startX, startY + gridHeight + 10f);

        DrawGoalsTopBar(startX, startY - cellSize - 10f);
    }
}
