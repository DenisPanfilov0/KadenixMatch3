using UnityEditor;
using UnityEngine;

public partial class LevelDesignerWindow : EditorWindow
{
    private void DrawHotBarTools(float startX, float startY)
    {
        float hotBarWidth = 9 * cellSize;
        float hotBarHeight = cellSize;

        for (int i = 0; i < 9; i++)
        {
            Rect hotBarCellRect = new Rect(startX + i * cellSize, startY, cellSize, hotBarHeight);

            if (i == selectedHotbarIndex)
            {
                EditorGUI.DrawRect(hotBarCellRect, Color.red);
            }
            else
            {
                EditorGUI.DrawRect(hotBarCellRect, Color.clear);
            }

            Handles.color = Color.blue;
            Handles.DrawLine(new Vector3(hotBarCellRect.x, hotBarCellRect.y),
                new Vector3(hotBarCellRect.x + hotBarCellRect.width, hotBarCellRect.y));
            Handles.DrawLine(new Vector3(hotBarCellRect.x, hotBarCellRect.y),
                new Vector3(hotBarCellRect.x, hotBarCellRect.y + hotBarCellRect.height));
            Handles.DrawLine(new Vector3(hotBarCellRect.x + hotBarCellRect.width, hotBarCellRect.y),
                new Vector3(hotBarCellRect.x + hotBarCellRect.width,
                    hotBarCellRect.y + hotBarCellRect.height));
            Handles.DrawLine(new Vector3(hotBarCellRect.x, hotBarCellRect.y + hotBarCellRect.height),
                new Vector3(hotBarCellRect.x + hotBarCellRect.width,
                    hotBarCellRect.y + hotBarCellRect.height));

            if (Event.current.type == EventType.MouseDown && hotBarCellRect.Contains(Event.current.mousePosition))
            {
                HandleHotbarToolsClick(i);
            }

            GUI.Label(new Rect(hotBarCellRect.x, hotBarCellRect.y, cellSize, cellSize), (i + 1).ToString(), new GUIStyle
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 14,
                normal = { textColor = Color.white }
            });
        }
    }
    
    private void HandleKeyboardInput()
    {
        Event currentEvent = Event.current;

        if (currentEvent.type == EventType.KeyDown)
        {
            if (currentEvent.keyCode >= KeyCode.Alpha1 && currentEvent.keyCode <= KeyCode.Alpha9)
            {
                int hotbarIndex = currentEvent.keyCode - KeyCode.Alpha1;
                HandleHotbarToolsClick(hotbarIndex);
                currentEvent.Use();
            }
        }
    }
    
    private void HandleHotbarToolsClick(int index)
    {
        selectedHotbarIndex = index;
        Repaint();
    }
}