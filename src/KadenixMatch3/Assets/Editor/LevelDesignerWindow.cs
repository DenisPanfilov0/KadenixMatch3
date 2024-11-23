using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.BoardBuildFeature;
using CustomEditor;
using UnityEditor;
using UnityEngine;

public partial class LevelDesignerWindow : EditorWindow
{
    private int selectedTab = 0;
    private readonly string[] tabNames = { "General", "TilesEditor" };

    private float splitterPosition = 1200f;
    private bool isDraggingSplitter = false;
    private float lastSplitterPosition = 0;

    private LevelDesignerConfig config;

    private int gridSize = 11;
    private float cellSize = 50f;

    private TileItem selectedTile = null;

    private Dictionary<Vector2Int, List<TileItem>>
        gridData = new Dictionary<Vector2Int, List<TileItem>>();

    private int selectedHotbarIndex = -1;
    private int selectedGoalsBarIndex = -1;
    private int selectedGeneratorBarIndex = -1;

    private TileItem coloredRedTile;
    private TileItem coloredPurpleTile;
    private TileItem coloredBlueTile;
    private TileItem coloredYellowTile;
    private TileItem coloredGreenTile;

    private List<TileItem> topBarTiles = new List<TileItem>();
    private List<TileItem> topBarGoals = new List<TileItem>();
    private List<TileItem> barGenerator = new List<TileItem>();

    private Rect windowRect = new Rect(0, 0, 300, 200);
    private bool isWindowOpen = false;
    private bool isWindowOpenGenerator = false;
    private Vector2Int selectedCell = Vector2Int.zero;
    private bool isMousePressed = false;
    private Vector2Int lastCellPos = new Vector2Int(-1, -1);
    private HashSet<int> openedHotbarIndices = new HashSet<int>();
    private HashSet<int> openedGeneratorbarIndices = new HashSet<int>();
    private HashSet<int> openedGoalsBarIndices = new HashSet<int>(); // Для GoalsTopBar
    private HashSet<int> openedGeneratorIndices = new HashSet<int>(); // Для GoalsTopBar

    [MenuItem("Level Designer/Open Level Designer")]
    public static void ShowWindow()
    {
        LevelDesignerWindow window = GetWindow<LevelDesignerWindow>("Level Designer");
        window.ShowModalUtility();
    }

    private void OnEnable()
    {
        config = Resources.Load<LevelDesignerConfig>("Configs/LevelDesignerConfig/LevelDesignerConfig");
        levels = Resources.Load<TextAsset>("Gameplay/Levels");

        coloredRedTile = config.Tiles.FirstOrDefault(tile => tile.Type == TileTypeId.coloredRed);
        coloredPurpleTile = config.Tiles.FirstOrDefault(tile => tile.Type == TileTypeId.coloredPurple);
        coloredBlueTile = config.Tiles.FirstOrDefault(tile => tile.Type == TileTypeId.coloredBlue);
        coloredYellowTile = config.Tiles.FirstOrDefault(tile => tile.Type == TileTypeId.coloredYellow);
        coloredGreenTile = config.Tiles.FirstOrDefault(tile => tile.Type == TileTypeId.coloredGreen);
    }

    private float buttonWidth = 150f;
    private float buttonHeight = 40f;

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical(GUILayout.Width(splitterPosition));
        DrawGrid();
        EditorGUILayout.EndVertical();

        ResizeSplitter();

        EditorGUILayout.BeginVertical();

        GUILayout.Space(10f);
        DrawButtons();

        DrawTabs();
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        HandleKeyboardInput();

        DrawWindow();
        DrawWindowGenerator();
    }
}
