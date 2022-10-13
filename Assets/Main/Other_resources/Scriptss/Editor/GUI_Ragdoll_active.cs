//��������� ������ �������� ��������������� ������ ����������� � ������ Ragdoll ������
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ragdoll_activity))]
public class GUI_Ragdoll_active : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Ragdoll_activity Ra = (Ragdoll_activity)target;

        if (GUILayout.Button("����� ���������� Ragdoll"))
        {
            Ra.Auto_Find_components();
        }

        if (GUILayout.Button("�������� ������ HurtBox ��� ������ �����"))
        {
            Ra.Auto_add_HurtBox_component();
        }
    }
}
#endif

