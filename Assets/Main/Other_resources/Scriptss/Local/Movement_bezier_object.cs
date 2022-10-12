//Передвигает объект по кривой безъе из 4-х точек
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_bezier_object : MonoBehaviour
{
    [Header("Основные параметры")]
    [Tooltip("Стартовая точка движения")]
    [SerializeField]
    Transform Start_point = null;

    [Tooltip("Средняя точка")]
    [SerializeField]
    Transform Middle_Point = null;

    [Tooltip("Средняя точка 2")]
    [SerializeField]
    Transform Middle_Point_2 = null;

    [Tooltip("Конечная точка движения")]
    [SerializeField]
    Transform End_point = null;

    [Tooltip("Объект")]
    [SerializeField]
    Transform Object = null;

    [Tooltip("Скорость")]
    [SerializeField]
    float Speed = 0.01f;





    [Header("Внутриние параметры (не трогать, если не нужно)")]
    [Space(20)]
    [Range(0f, 1f)]
    [Tooltip("Расположение на кривой")]
    [SerializeField]
    float Step = 0;

    [Tooltip("Меш объекта")]
    [SerializeField]
    Mesh Object_mesh = null;

    [Tooltip("Случайная позиция на кривой")]
    [SerializeField]
    bool Random_position_bool = false;

    bool Movement_end = true;

    private void Start()
    {
        if (Random_position_bool)
            Step = Random.Range(0f, 1f);
    }

    private void FixedUpdate()
    {
        if (Step < 1 && Movement_end)
        {
            Step += Speed;
        }
        else if (Movement_end)
        {
            Movement_end = false;
        }


        if (Step > 0 && !Movement_end)
        {
            Step -= Speed;
        }
        else if (!Movement_end)
        {
            Movement_end = true;
        }

        Object.position = Game_Bezier_curve.Get_point_Bezier(Start_point.position, Middle_Point.position, Middle_Point_2.position, End_point.position, Step);
    }

    private void OnDrawGizmos()
    {
        if (Start_point && Middle_Point && Middle_Point_2 && End_point && Object_mesh)
        {
            int sigment_number = 20;

            Vector3 preveuse_point = Start_point.position;

            for (int x = 0; x < sigment_number; x++)
            {
                float perimeter = (float)x / sigment_number;
                Vector3 point = Game_Bezier_curve.Get_point_Bezier(Start_point.position, Middle_Point.position, Middle_Point_2.position, End_point.position, perimeter);
                Gizmos.DrawLine(preveuse_point, point);
                preveuse_point = point;
            }

            Vector3 pos = Game_Bezier_curve.Get_point_Bezier(Start_point.position, Middle_Point.position, Middle_Point_2.position, End_point.position, Step);

            Gizmos.DrawMesh(Object_mesh, pos, Object.rotation, Object.localScale - new Vector3(-0.001f, -0.001f, -0.001f));

            Gizmos.DrawMesh(Object_mesh, Start_point.position, transform.rotation, transform.localScale * 0.8f);
            Gizmos.DrawMesh(Object_mesh, End_point.position, transform.rotation, transform.localScale * 0.8f);
        }
    }

}
