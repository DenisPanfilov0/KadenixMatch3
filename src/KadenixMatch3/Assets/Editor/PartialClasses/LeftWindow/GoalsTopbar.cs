using System.Collections.Generic;
using CustomEditor;
using UnityEditor;
using UnityEngine;

public partial class LevelDesignerWindow : EditorWindow
{
    // Переменная для смещения инпут полей
    private float inputFieldVerticalOffset = -15f;

    // Индекс текущей добавленной ячейки
    private int currentGoalCount = 1;

    // Расстояние между ячейками
    private float cellSpacing = 20;

    // Список для хранения значений инпут полей
    private List<string> topBarGoalsValues = new List<string>();
    // private List<string> GeneratorValues = new List<string>();

    private void DrawGoalsTopBar(float startX, float startY)
    {
        float topBarWidth = 5 * cellSize;
        float topBarHeight = cellSize;
        float inputFieldHeight = 20f; // Высота для инпут полей

        float offsetX = (gridSize * cellSize - topBarWidth) / 2f;

        // Рисуем ячейки на основе currentGoalCount
        for (int i = 0; i < currentGoalCount; i++)
        {
            // Смещаем ячейку с учётом inputFieldVerticalOffset
            Rect topBarCellRect = new Rect(startX + offsetX + i * (cellSize + cellSpacing),
                startY + inputFieldVerticalOffset, cellSize, topBarHeight);

            // Рисуем ячейку
            if (openedHotbarIndices.Contains(i))
            {
                EditorGUI.DrawRect(topBarCellRect, new Color(0.8f, 0.8f, 0.95f, 1f));
            }
            else
            {
                EditorGUI.DrawRect(topBarCellRect, new Color(0.9f, 0.9f, 0.9f, 1f));
            }

            Handles.DrawSolidRectangleWithOutline(topBarCellRect, Color.clear, Color.black);

            if (i < topBarGoals.Count && topBarGoals[i] != null)
            {
                GUI.DrawTexture(topBarCellRect, topBarGoals[i].Sprite.texture);
            }

            // Кнопка "+"
            if (GUI.Button(topBarCellRect, "+", new GUIStyle()
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 18,
                    normal = { textColor = Color.white },
                    fontStyle = FontStyle.Bold
                }))
            {
                HandleGoalsTopBarClick(i);
            }

            // Крестик в правом верхнем углу ячейки
            Rect closeButtonRect = new Rect(topBarCellRect.x + topBarCellRect.width + cellSpacing - 18,
                topBarCellRect.y, 20, 20);

            GUIStyle closeButtonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 32, // Увеличиваем размер шрифта для крестика
                fontStyle = FontStyle.Bold, // Жирный шрифт
                normal =
                {
                    background = MakeTexture(1, 1, Color.white), textColor = Color.red
                }, // Белый фон и красный текст
                alignment = TextAnchor.MiddleCenter, // Выравнивание по центру
                stretchWidth = true, // Чтобы кнопка занимала всю ширину
                stretchHeight = true // Чтобы кнопка занимала всю высоту
            };

            // Рисуем кнопку крестика
            if (GUI.Button(closeButtonRect, "×", closeButtonStyle))
            {
                HandleRemoveGoal(i); // Обработчик удаления цели
            }

            // Создаем инпут поле под ячейкой
            Rect inputFieldRect = new Rect(startX + offsetX + i * (cellSize + cellSpacing),
                startY + topBarHeight + inputFieldVerticalOffset, cellSize, inputFieldHeight);
            string inputValue =
                topBarGoalsValues.Count > i ? topBarGoalsValues[i] : "0"; // Получаем текущее значение или 0

            // Ограничиваем ввод только положительными числами
            string newInputValue = EditorGUI.TextField(inputFieldRect, inputValue);
            if (float.TryParse(newInputValue, out float result) && result > 0)
            {
                // Если введенное значение положительное, сохраняем его
                if (topBarGoalsValues.Count <= i)
                {
                    topBarGoalsValues.Add(newInputValue);
                }
                else
                {
                    topBarGoalsValues[i] = newInputValue;
                }
            }
            else if (newInputValue != inputValue) // Если введено невалидное значение, оставляем старое
            {
                // Можно добавить сообщение об ошибке, если нужно
            }
        }
    }

    private void HandleRemoveGoal(int index)
    {
        if (index < 0 || index >= currentGoalCount || topBarGoals.Count <= 1)
            return;

        // Удаляем цель (объект) из списка
        if (index < topBarGoals.Count)
            topBarGoals.RemoveAt(index);

        // Удаляем значение инпут поля из списка
        if (index < topBarGoalsValues.Count)
            topBarGoalsValues.RemoveAt(index);

        // Уменьшаем количество отображаемых ячеек
        currentGoalCount--;

        while (topBarGoalsValues.Count < currentGoalCount)
        {
            topBarGoalsValues.Add("0"); // Добавляем значение по умолчанию
        }

        // Обновляем UI
        Repaint();
    }


    private void HandleGoalsTopBarClick(int index)
    {
        selectedGoalsBarIndex = index;
        openedGoalsBarIndices.Add(index);
        Repaint();
        OpenWindow(new Vector2Int(index, 0));
    }

    private Texture2D MakeTexture(int width, int height, Color color)
    {
        Texture2D texture = new Texture2D(width, height);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    private void OpenWindow(Vector2Int cellPosition)
    {
        selectedCell = cellPosition;
        windowRect.x = selectedCell.x * cellSize + 10;
        windowRect.y = selectedCell.y * cellSize + 10;

        isWindowOpen = true;
    }

    private Vector2 scrollPosition;

    private void DrawWindow()
    {
        if (isWindowOpen)
        {
            // Увеличиваем размеры окна
            Rect expandedWindowRect =
                new Rect(windowRect.x, windowRect.y, windowRect.width + 200, windowRect.height + 300);

            // Рисуем фон окна
            EditorGUI.DrawRect(expandedWindowRect, new Color(0f, 0f, 0f, 0.8f));
            Handles.DrawSolidRectangleWithOutline(expandedWindowRect, new Color(0f, 0f, 0f, 0.8f), Color.white);

            // Кнопка закрытия
            Rect closeButtonRect = new Rect(expandedWindowRect.x + expandedWindowRect.width - 30,
                expandedWindowRect.y + 10, 20, 20);
            if (GUI.Button(closeButtonRect, "X",
                    new GUIStyle() { fontSize = 16, normal = { textColor = Color.white } }))
            {
                CloseWindow();
            }

            // Скроллируемая область
            Rect scrollRect = new Rect(expandedWindowRect.x + 10, expandedWindowRect.y + 40,
                expandedWindowRect.width - 20, expandedWindowRect.height - 50);
            Rect contentRect =
                new Rect(0, 0, expandedWindowRect.width - 40, 1000); // Высота контента увеличена для скроллинга

            scrollPosition = GUI.BeginScrollView(scrollRect, scrollPosition, contentRect, true, true);

            DrawTileSelection(contentRect);

            GUI.EndScrollView();
        }
    }

    private void DrawTileSelection(Rect contentRect)
    {
        float tileSize = 50f;
        float spacing = 10f;
        float currentX = 10f;
        float currentY = 10f;

        // Получение тайлов из конфигурации
        if (config == null || config.Tiles == null || config.Tiles.Count == 0)
        {
            GUI.Label(new Rect(currentX, currentY, contentRect.width - 20, 50f),
                "Конфиг не загружен или отсутствуют элементы!", new GUIStyle()
                {
                    fontSize = 14,
                    normal = { textColor = Color.red },
                    alignment = TextAnchor.MiddleCenter
                });
            return;
        }

        TileItem[] tiles = config.Tiles.ToArray();

        // Отрисовка тайлов
        for (int i = 0; i < tiles.Length; i++)
        {
            if (currentX + tileSize + spacing > contentRect.width)
            {
                currentX = 10f;
                currentY += tileSize + spacing;
            }

            Rect tileRect = new Rect(currentX, currentY, tileSize, tileSize);
            GUI.DrawTexture(tileRect, tiles[i].Sprite.texture);

            if (Event.current.type == EventType.MouseDown && tileRect.Contains(Event.current.mousePosition))
            {
                AddTileToTopBar(tiles[i]);
                CloseWindow();
            }

            currentX += tileSize + spacing;
        }
    }


    private void AddTileToTopBar(TileItem tile)
    {
        // Проверяем значение инпут поля перед добавлением
        string inputValue = topBarGoalsValues.Count > selectedGoalsBarIndex
            ? topBarGoalsValues[selectedGoalsBarIndex]
            : "0";
        if (float.TryParse(inputValue, out float result) && result > 0)
        {
            // Увеличиваем количество ячеек при добавлении нового объекта
            currentGoalCount++;

            if (topBarGoals.Count - 1 > selectedGoalsBarIndex || topBarGoals.Count >= 4)
            {
                currentGoalCount--;
            }

            // Добавляем новый объект в список
            if (selectedGoalsBarIndex < 0 || selectedGoalsBarIndex >= 5)
            {
                return;
            }

            while (topBarGoals.Count <= selectedGoalsBarIndex)
            {
                topBarGoals.Add(null);
            }

            topBarGoals[selectedGoalsBarIndex] = tile;
            Repaint();
            CloseWindow();
        }
        else
        {
            // Добавьте сообщение о том, что значение невалидно
            Debug.LogWarning("Invalid input value: " + inputValue);
        }
    }

    private void CloseWindow()
    {
        isWindowOpen = false;
        Repaint();
    }
}
