using UnityEditor;
using UnityEngine;

public partial class LevelDesignerWindow : EditorWindow
{
    private void DrawTabs()
    {
        selectedTab = GUILayout.Toolbar(selectedTab, tabNames);

        EditorGUILayout.Space();
        switch (selectedTab)
        {
            case 0:
                DrawGeneralTab();
                break;
            case 1:
                DrawTilesEditorTab();
                break;
        }
    }

    private void DrawButtons()
    {
        GUILayout.BeginHorizontal(); // Смена на горизонтальный макет

        // Первая кнопка
        if (GUILayout.Button("Board Clear", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
        {
            OnBoardClearButtonClicked();
        }

        // Вторая кнопка
        if (GUILayout.Button("Fill Board Tiles", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
        {
            OnFillBoardTilesButtonClicked();
        }

        // Новая кнопка "Save Level"
        if (GUILayout.Button("Save Level", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
        {
            OnSaveLevelButtonClicked();
        }

        GUILayout.EndHorizontal(); // Закрываем горизонтальный макет
    }
}