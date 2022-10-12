//Добавляет кнопку очищения сохранений в администратор скрипте
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Game_administrator))]
public class GUI_Game_administrator : Editor
{
    /*
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Game_administrator Ga = (Game_administrator)target;

        if(GUILayout.Button("Очистить сохранения"))
        {
            Ga.Clear_save();
        }
    }
    */
}
#endif
