using UnityEditor;
using UnityEngine;

public partial class LevelDesignerWindow : EditorWindow
{
    private void DrawMovesField(float posX, float posY)
    {
        Rect fieldArea = new Rect(posX, posY, cellSize, cellSize);

        // Рисуем белый фон ячейки
        EditorGUI.DrawRect(fieldArea, Color.white);

        // Рисуем текст "Moves" чёрным цветом
        GUIStyle labelStyle = new GUIStyle(EditorStyles.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 12,
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.black }
        };
        EditorGUI.LabelField(new Rect(fieldArea.x, fieldArea.y, fieldArea.width, fieldArea.height / 2), "Moves", labelStyle);

        // Поле ввода для количества ходов
        movesCount = EditorGUI.IntField(new Rect(fieldArea.x, fieldArea.y + fieldArea.height / 2, fieldArea.width, fieldArea.height / 2), movesCount);
    }
}