//Постороний скрипт для более полного экспорта проекта (дополнительно экспортировать настройки, теги и слои)
//Инструмент будет находиться Windows - Custom Export Package
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class Export_script : EditorWindow
{
    bool[] valuesCheck;
    string[] values;
    string fileName;
    bool check, all;

    [MenuItem("Window/Custom Export Package...")]
    static void ShowWindow()
    {
        Export_script editor = GetWindow<Export_script>();
        editor.Init();
    }

    void Init()
    {
        all = true;
        check = true;
        values = Directory.GetFiles("ProjectSettings", "*.asset");
        valuesCheck = new bool[values.Length];

        for (int i = 0; i < values.Length; i++)
        {
            valuesCheck[i] = check;
        }

        fileName = PlayerSettings.productName;
    }

    void OnGUI()
    {
        EditorGUILayout.Separator();
        fileName = EditorGUILayout.TextField("Имя файла:", fileName);
        EditorGUILayout.Separator();
        all = EditorGUILayout.ToggleLeft("Добавить все ассеты проекта в сборку?", all, EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(all ? "Будут включены все ассеты проекта и те настройки, которые выбраны ниже." : "Будут добавлены только настройки выбранные ниже...", MessageType.Info);
        EditorGUILayout.Separator();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Project Settings", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        for (int i = 0; i < values.Length; i++)
        {
            valuesCheck[i] = EditorGUILayout.ToggleLeft(Path.GetFileNameWithoutExtension(values[i]), valuesCheck[i]);
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
        EditorGUILayout.Separator();

        if (GUILayout.Button(check ? "Снять все отметки" : "Выбрать всё", GUILayout.Height(30)))
        {
            check = !check;

            for (int i = 0; i < values.Length; i++)
            {
                valuesCheck[i] = check;
            }
        }

        EditorGUILayout.Separator();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Обновить / Сбросить", GUILayout.Height(30)))
        {
            Init();
        }
        if (GUILayout.Button("Экспортировать", GUILayout.Height(30)))
        {
            Export();
        }

        EditorGUILayout.EndHorizontal();
        EditorGUI.DropShadowLabel(new Rect(0, 0, position.width, position.height - 5), "© 2019 NULLcode Studio");
    }

    void Export()
    {
        List<string> projectContent = new List<string>();
        if (all) projectContent.Add("Assets");

        for (int i = 0; i < values.Length; i++)
        {
            if (valuesCheck[i])
            {
                projectContent.Add(values[i]);
            }
        }

        if (projectContent.Count == 0)
        {
            Debug.LogWarning("Custom Export Package --> Не выбран ни одни ассет, экспорт отменен.");
            return;
        }

        AssetDatabase.ExportPackage(projectContent.ToArray(), fileName + ".unitypackage", ExportPackageOptions.Interactive | ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
    }
}
#endif
