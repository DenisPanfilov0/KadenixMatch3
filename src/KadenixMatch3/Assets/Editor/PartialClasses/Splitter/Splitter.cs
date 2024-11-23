using UnityEditor;
using UnityEngine;

public partial class LevelDesignerWindow : EditorWindow
{
    private void ResizeSplitter()
    {
        Rect splitterRect = new Rect(splitterPosition, 0, 5, position.height);

        EditorGUI.DrawRect(new Rect(splitterPosition - 2, 0, 5, position.height), Color.gray);
        EditorGUI.DrawRect(splitterRect, Color.white);

        EditorGUIUtility.AddCursorRect(splitterRect, MouseCursor.ResizeHorizontal);

        if (Event.current.type == EventType.MouseDown && splitterRect.Contains(Event.current.mousePosition))
        {
            isDraggingSplitter = true;
        }

        if (isDraggingSplitter && Event.current.type == EventType.MouseDrag)
        {
            splitterPosition += Event.current.delta.x;
            splitterPosition = Mathf.Clamp(splitterPosition, 150f, position.width - 150f);

            if (Mathf.Abs(splitterPosition - lastSplitterPosition) > 1f)
            {
                lastSplitterPosition = splitterPosition;
                Repaint();
            }
        }

        if (Event.current.type == EventType.MouseUp)
        {
            isDraggingSplitter = false;
        }

        GUILayout.Box("", GUILayout.Width(5), GUILayout.ExpandHeight(true));
    }
}