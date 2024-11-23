using System.Collections.Generic;
using CustomEditor;
using UnityEditor;
using UnityEngine;

public partial class LevelDesignerWindow : EditorWindow
{
    private void DrawBoardGridCells(float startX, float startY)
    {
        for (int y = gridSize - 1; y >= 0; y--) // Изменение направления: от нижней строки к верхней
        {
            for (int x = 0; x < gridSize; x++)
            {
                Vector2Int cellPos = new Vector2Int(x, y);
                Rect cellRect = new Rect(startX + x * cellSize, startY + (gridSize - 1 - y) * cellSize, cellSize, cellSize);

                if (gridData.TryGetValue(cellPos, out List<TileItem> tiles) && tiles != null)
                {
                    foreach (TileItem tile in tiles)
                    {
                        GUI.DrawTexture(cellRect, tile.Sprite.texture);
                    }
                }

                if (Event.current.type == EventType.MouseDown && cellRect.Contains(Event.current.mousePosition))
                {
                    HandleCellClick(cellPos);
                    isMousePressed = true;
                    lastCellPos = cellPos;
                }

                if (isMousePressed && selectedHotbarIndex == 1 && Event.current.type == EventType.MouseDrag)
                {
                    if (cellRect.Contains(Event.current.mousePosition) && !cellPos.Equals(lastCellPos))
                    {
                        HandleCellClick(cellPos);
                        lastCellPos = cellPos;
                    }
                }

                if (Event.current.type == EventType.MouseUp)
                {
                    isMousePressed = false;
                }
            }
        }
    }

    
    private void HandleCellClick(Vector2Int cellPos)
    {
        if (selectedTile == null || selectedHotbarIndex == -1)
            return;

        if (!gridData.ContainsKey(cellPos))
        {
            gridData[cellPos] = new List<TileItem>();
        }

        List<TileItem> tiles = gridData[cellPos];

        if (Event.current.button == 0) // Левая кнопка мыши
        {
            // Проверка, может ли быть добавлен новый объект
            if (selectedTile.TileType != TileType.Tile)
            {
                if (!tiles.Exists(tile => tile.TileType == TileType.Tile))
                {
                    return;
                }
            }

            if (selectedTile.TileType == TileType.Tile)
            {
                if (tiles.Exists(tile => tile.TileType == TileType.Tile))
                {
                    return;
                }
            }

            if (selectedTile.TileType == TileType.TileContent)
            {
                if (tiles.Exists(tile => tile.TileType == TileType.TileContent))
                {
                    return;
                }
            }

            // Проверка на дублирование модификатора с одинаковой прочностью
            if (tiles.Exists(tile => 
                    tile.TileType == selectedTile.TileType && 
                    tile.Durability == selectedTile.Durability))
            {
                return; // Если такой объект уже существует, ничего не делать
            }

            // Добавление нового объекта
            tiles.Add(selectedTile);
        }
        else if (Event.current.button == 1) // Правая кнопка мыши
        {
            if (gridData.ContainsKey(cellPos))
            {
                gridData[cellPos].Clear(); // Очистить список объектов
                if (gridData[cellPos].Count == 0) // Проверить, пуст ли список
                {
                    gridData.Remove(cellPos); // Удалить ключ из Dictionary
                }
            }
        }

        Repaint();
    }

}