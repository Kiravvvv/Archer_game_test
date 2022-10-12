using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/*
[CustomEditor(typeof(Camera_control))]
public class GUI_Camera_control : Editor
{
    public override void OnInspectorGUI()
    {

        Camera_control C_c = (Camera_control)target;

        
        C_c.Target = EditorGUILayout.ObjectField(new GUIContent("Target", "Цель камеры"), C_c.Target, typeof(Transform), true) as Transform;
        C_c.Behind_target_bool = EditorGUILayout.Toggle(new GUIContent("Behind_target", "Будет находится сзади цели"), C_c.Behind_target_bool);

        if (C_c.Behind_target_bool)
        {
            EditorGUILayout.BeginVertical("box");

            C_c.Height = EditorGUILayout.FloatField(new GUIContent("Height", "Высота камеры"), C_c.Height);
            C_c.Zoom = EditorGUILayout.FloatField(new GUIContent("Zoom", "Приближение камеры"), C_c.Zoom);

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }
        else
        {
            EditorGUILayout.BeginVertical("box");

            C_c.Offset = EditorGUILayout.Vector3Field(new GUIContent("Offset", "Смещение камеры"), C_c.Offset);

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        C_c.Speed = EditorGUILayout.FloatField(new GUIContent("Speed", "Скорость приближения к цели"), C_c.Speed);

        C_c.Mouse_control_bool = EditorGUILayout.Toggle(new GUIContent("Behind_target", "Может ли игрок мышкой вращать камеру"), C_c.Mouse_control_bool);

        if (C_c.Mouse_control_bool)
        {
            EditorGUILayout.BeginVertical("box");

            C_c.Speed_rotation = EditorGUILayout.FloatField(new GUIContent("Speed_rotation", "Скорость поворота"), C_c.Speed_rotation);
            C_c.Axis_X = EditorGUILayout.ObjectField(new GUIContent("Axis_X", "Дочерний объект для поворота по вертикале"), C_c.Axis_X, typeof(Transform), true) as Transform;

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }


        if (GUI.changed)//Проверяет что бы предупредить о сохранение сцены после редактирование в инспекторе 
        {
            EditorUtility.SetDirty(C_c.gameObject);
            EditorSceneManager.MarkSceneDirty(C_c.gameObject.scene);
        }


    }
}
*/