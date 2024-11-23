using System;
using System.Collections.Generic;
using CustomEditor;
using UnityEditor;
using UnityEngine;

public partial class LevelDesignerWindow : EditorWindow
{
    private void DrawTilesEditorTab()
    {
        EditorGUILayout.LabelField("Tiles Editor", EditorStyles.boldLabel);

        if (config == null || config.Tiles == null || config.Tiles.Count == 0)
        {
            EditorGUILayout.HelpBox("Конфиг не загружен или отсутствуют элементы!", MessageType.Warning);
            return;
        }

        var tilesByType = new Dictionary<TileType, List<TileItem>>();
        foreach (TileItem tile in config.Tiles)
        {
            if (!tilesByType.ContainsKey(tile.TileType))
            {
                tilesByType[tile.TileType] = new List<TileItem>();
            }

            tilesByType[tile.TileType].Add(tile);
        }

        foreach (TileType tileType in Enum.GetValues(typeof(TileType)))
        {
            if (!tilesByType.ContainsKey(tileType))
                continue;

            GUILayout.Space(10);
            Rect labelRect = GUILayoutUtility.GetRect(GUIContent.none, EditorStyles.boldLabel, GUILayout.Height(40));
            EditorGUI.DrawRect(labelRect, new Color(0.8f, 0.8f, 1f));
            EditorGUI.LabelField(new Rect(labelRect.x + 10, labelRect.y + 5, labelRect.width, 30), tileType.ToString(),
                new GUIStyle(EditorStyles.boldLabel)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 14,
                    normal = { textColor = Color.black }
                });

            Rect blockRect = EditorGUILayout.BeginVertical("box");
            EditorGUI.DrawRect(blockRect, new Color(0.95f, 0.95f, 0.95f));

            DrawAdaptiveTileGrid(tilesByType[tileType]);

            EditorGUILayout.EndVertical();
        }
    }

    private void DrawAdaptiveTileGrid(List<TileItem> tiles)
    {
        const float cellSize = 60f;
        const float spacing = 5f;

        float availableWidth = position.width - splitterPosition - 40f;
        int columns = Mathf.FloorToInt(availableWidth / (cellSize + spacing));

        if (columns < 1) columns = 1;

        int rowIndex = 0;
        EditorGUILayout.BeginHorizontal();

        foreach (TileItem tile in tiles)
        {
            GUILayout.BeginVertical(GUILayout.Width(cellSize), GUILayout.Height(cellSize));

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleCenter,
                normal = { background = CreateTexture(2, 2, tile == selectedTile ? Color.red : Color.grey) }
            };

            if (GUILayout.Button(new GUIContent(tile.Sprite?.texture), buttonStyle, GUILayout.Width(cellSize),
                    GUILayout.Height(cellSize - 20)))
            {
                selectedTile = tile;
            }

            EditorGUILayout.LabelField(tile.Name, new GUIStyle(EditorStyles.miniLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 15
            }, GUILayout.Width(cellSize));

            GUILayout.EndVertical();

            rowIndex++;
            if (rowIndex % columns == 0)
            {
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    private Texture2D CreateTexture(int width, int height, Color color)
    {
        Texture2D texture = new Texture2D(width, height);
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; i++)
        {
            pix[i] = color;
        }

        texture.SetPixels(pix);
        texture.Apply();
        return texture;
    }
}
