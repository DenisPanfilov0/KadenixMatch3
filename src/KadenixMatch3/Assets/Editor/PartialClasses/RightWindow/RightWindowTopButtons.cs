using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.BoardBuildFeature;
using CustomEditor;
using UnityEditor;
using UnityEngine;

public partial class LevelDesignerWindow : EditorWindow
{
    private void OnBoardClearButtonClicked()
    {
        gridData.Clear();

        selectedTile = null;
        selectedHotbarIndex = -1;

        Repaint();
    }

    private void OnFillBoardTilesButtonClicked()
    {
        bool isBoardEmpty = true;

        foreach (var cell in gridData)
        {
            if (cell.Value.Count > 0)
            {
                isBoardEmpty = false;
                break;
            }
        }

        if (isBoardEmpty)
        {
            TileItem commonLightTile = config.Tiles.FirstOrDefault(tile => tile.Type == TileTypeId.commonLight);
            TileItem commonDarkTile = config.Tiles.FirstOrDefault(tile => tile.Type == TileTypeId.commonDark);

            for (int y = 0; y < gridSize; y++)
            {
                bool isCommonLightTurn =
                    (y % 2 == 0);

                for (int x = 0; x < gridSize; x++)
                {
                    Vector2Int cellPos = new Vector2Int(x, y);

                    if (!gridData.ContainsKey(cellPos) || gridData[cellPos].Count == 0)
                    {
                        if (!gridData.ContainsKey(cellPos))
                        {
                            gridData[cellPos] = new List<TileItem>();
                        }

                        TileItem selectedTile = isCommonLightTurn ? commonLightTile : commonDarkTile;
                        gridData[cellPos].Add(selectedTile);

                        isCommonLightTurn = !isCommonLightTurn;
                    }
                }
            }
        }

        Repaint();
    }
}
