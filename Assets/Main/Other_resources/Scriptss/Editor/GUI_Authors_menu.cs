//Пользовательские настройки для скрипта меню авторов
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Authors_menu))]
public class GUI_Authors_menu : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Authors_menu Am = (Authors_menu)target;

        if(GUILayout.Button("Добавить в категорию программистов"))
        {
            Am.Generation_author();
        }
    }
}
#endif
