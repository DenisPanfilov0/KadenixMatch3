using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.BoardBuildFeature.Systems
{
    public sealed class EmptyCellFillingSystem : IInitializeSystem
    {
        private readonly IGroup<GameEntity> _masksEmptyCells;

        private readonly Dictionary<string, byte[,]> _figureMasks = new()
        {
            // Горизонтальные линии
            { "Match3Horizontal", new byte[,] { { 0, 1, 1, 1, 0 } } },
            { "Match4Horizontal", new byte[,] { { 0, 1, 1, 1, 1, 0 } } },
            { "Match5Horizontal", new byte[,] { { 0, 1, 1, 1, 1, 1, 0 } } },

            // Вертикальные линии
            { "Match3Vertical", new byte[,] { { 0 }, { 1 }, { 1 }, { 1 }, { 0 } } },
            { "Match4Vertical", new byte[,] { { 0 }, { 1 }, { 1 }, { 1 }, { 1 }, { 0 } } },
            { "Match5Vertical", new byte[,] { { 0 }, { 1 }, { 1 }, { 1 }, { 1 }, { 1 }, { 0 } } },
            
            // Новая фигура (квадрат из 4 цифр)
            { "Square", new byte[,] { { 0, 0, 0, 0 },
                { 0, 1, 1, 0 },
                { 0, 1, 1, 0 },
                { 0, 0, 0, 0 } } }
        };

        private Dictionary<TileTypeId, int> _tileMaskType = new()
        {
            {TileTypeId.coloredBlue, 1},
            {TileTypeId.coloredRed, 2},
            {TileTypeId.coloredYellow, 3},
            {TileTypeId.coloredGreen, 4},
            {TileTypeId.coloredPurple, 5},
        };

        public EmptyCellFillingSystem(GameContext game)
        {
            _masksEmptyCells = game.GetGroup(GameMatcher.MaskEmptyCellsToFill);
        }

        public void Initialize()
        {
            foreach (var maskEmptyCells in _masksEmptyCells)
            {
                // Получаем исходную маску
                var boardMask = maskEmptyCells.maskEmptyCellsToFill.Value;

                // Создаём 10 копий маски и работаем с ними
                for (int i = 0; i < 20; i++)
                {
                    // Создаём копию исходной маски
                    var boardMaskCopy = CopyBoardMask(boardMask);

                    // Заполняем пустые клетки
                    FillEmptyCells(boardMaskCopy);

                    // Выводим обновленную маску в консоль
                    PrintBoardMask(boardMaskCopy);
                }
            }
        }

        // Метод для копирования маски
        private int[,] CopyBoardMask(int[,] originalMask)
        {
            int width = originalMask.GetLength(0);
            int height = originalMask.GetLength(1);

            int[,] newMask = new int[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    newMask[x, y] = originalMask[x, y];
                }
            }

            return newMask;
        }

        private void FillEmptyCells(int[,] boardMask)
        {
            int width = boardMask.GetLength(0);
            int height = boardMask.GetLength(1);

            List<Vector2Int> visitedCells = new List<Vector2Int>();
            bool anyChanges = true;

            while (anyChanges) // Пытаемся сделать изменения до тех пор, пока есть изменения
            {
                anyChanges = false; // Сначала предполагаем, что изменений не будет

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (boardMask[x, y] == 0)  // Пустая клетка
                        {
                            // 1. Берём случайное число для заполнения
                            int randomValue = GetRandomColor();
                            boardMask[x, y] = randomValue;

                            // 2. Проверяем соседние клетки
                            var sameValueGroup = CheckNeighbors(boardMask, x, y, randomValue, visitedCells);

                            // 3. Если нашли группу одинаковых чисел > 2, проверяем её на принадлежность фигуре
                            if (sameValueGroup.Count > 2)
                            {
                                if (CheckFigureMatch(sameValueGroup, randomValue))
                                {
                                    // 4. Если группа принадлежит фигуре, заменяем текущую цифру
                                    int newValue = GetRandomColorExcluding(boardMask, x, y);
                                    boardMask[x, y] = newValue;

                                    // 5. Проверяем заново только для этой клетки
                                    FillEmptyCellsForSingleCell(boardMask, x, y);
                                    anyChanges = true; // Отметим, что были изменения
                                    break; // Прерываем цикл для повторной проверки
                                }
                                else
                                {
                                    // В случае, если фигура не найдена, просто продолжаем с другим значением
                                    boardMask[x, y] = randomValue;
                                }
                            }
                            else
                            {
                                // Если не было найдено ни одной группы, просто продолжаем
                                boardMask[x, y] = randomValue;
                            }
                        }
                    }
                }
            }
        }

        private void FillEmptyCellsForSingleCell(int[,] boardMask, int x, int y)
        {
            List<Vector2Int> visitedCells = new List<Vector2Int>();

            int randomValue = boardMask[x, y];
            var sameValueGroup = CheckNeighbors(boardMask, x, y, randomValue, visitedCells);

            if (sameValueGroup.Count > 2)
            {
                // Если группа состоит из более чем двух клеток, проверяем на фигуру
                if (CheckFigureMatch(sameValueGroup, randomValue))
                {
                    int newValue = GetRandomColorExcluding(boardMask, x, y);
                    boardMask[x, y] = newValue;

                    // Проводим повторную проверку
                    FillEmptyCellsForSingleCell(boardMask, x, y);
                }
            }
        }

        private bool CheckFigureMatch(List<Vector2Int> group, int value)
        {
            foreach (var figure in _figureMasks)
            {
                var figureMask = figure.Value;
                int width = figureMask.GetLength(0);
                int height = figureMask.GetLength(1);

                foreach (var point in group)
                {
                    bool matchFound = true;
                    for (int dx = 0; dx < width; dx++)
                    {
                        for (int dy = 0; dy < height; dy++)
                        {
                            if (figureMask[dx, dy] == 1)
                            {
                                int nx = point.x + dx - width / 2;
                                int ny = point.y + dy - height / 2;

                                if (!group.Contains(new Vector2Int(nx, ny)))
                                {
                                    matchFound = false;
                                    break;
                                }
                            }
                        }

                        if (!matchFound) break;
                    }

                    if (matchFound)
                    {
                        Debug.Log($"Figure matched: {figure.Key}");
                        return true;  // Фигура найдена
                    }
                }
            }
            return false;  // Фигура не найдена
        }

        private int GetRandomColor()
        {
            return Random.Range(1, 6);  // Рандомный цвет от 1 до 5
        }

        private int GetRandomColorExcluding(int[,] boardMask, int x, int y)
        {
            // Получаем текущий цвет клетки
            int currentValue = boardMask[x, y];
            int newColor;
            do
            {
                newColor = Random.Range(1, 6); // Рандомный цвет от 1 до 5
            } while (newColor == currentValue);  // Не выбираем тот же цвет

            return newColor;
        }

        private List<Vector2Int> CheckNeighbors(int[,] boardMask, int x, int y, int value, List<Vector2Int> visitedCells)
        {
            List<Vector2Int> group = new List<Vector2Int>();
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            List<Vector2Int> localVisited = new List<Vector2Int>();

            queue.Enqueue(new Vector2Int(x, y));
            group.Add(new Vector2Int(x, y));
            localVisited.Add(new Vector2Int(x, y));

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int cx = current.x;
                int cy = current.y;

                foreach (var direction in new Vector2Int[] { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) })
                {
                    int nx = cx + direction.x;
                    int ny = cy + direction.y;

                    if (nx >= 0 && nx < boardMask.GetLength(0) && ny >= 0 && ny < boardMask.GetLength(1))
                    {
                        if (boardMask[nx, ny] == value && !localVisited.Contains(new Vector2Int(nx, ny)))
                        {
                            group.Add(new Vector2Int(nx, ny));
                            queue.Enqueue(new Vector2Int(nx, ny));
                            localVisited.Add(new Vector2Int(nx, ny));
                        }
                    }
                }
            }

            visitedCells.AddRange(localVisited);
            localVisited.Clear();

            return group;
        }

        private void PrintBoardMask(int[,] boardMask)
        {
            int width = boardMask.GetLength(0);
            int height = boardMask.GetLength(1);

            System.Text.StringBuilder output = new System.Text.StringBuilder();

            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    int value = boardMask[x, y];
                    string formattedValue = FormatValue(value);
                    output.Append(formattedValue);
                }
                output.AppendLine();
            }

            Debug.Log(output.ToString());
        }

        // private string FormatValue(int value)
        // {
        //     string color = value switch
        //     {
        //         -1 => "black",
        //         1 => "blue",
        //         2 => "red",
        //         3 => "yellow",
        //         4 => "green",
        //         5 => "purple",
        //         _ => "white",
        //     };
        //
        //     // Используем символ "■" для квадратиков, увеличиваем размер в 3 раза и уменьшаем расстояние
        //     string coloredValue = $"<color={color}><b><size=150%>■</size></b></color>";
        //
        //     // Уменьшаем расстояние между квадратиками в 2 раза
        //     return coloredValue;
        // }
        
        private string FormatValue(int value)
        {
            string color = value switch
            {
                -1 => "black",
                1 => "blue",
                2 => "red",
                3 => "yellow",
                4 => "green",
                5 => "purple",
                _ => "white",
            };

            string coloredValue = $"<color={color}><b>{value,10}</b></color>";
            return $"  {coloredValue}  ";
        }

    }
}
