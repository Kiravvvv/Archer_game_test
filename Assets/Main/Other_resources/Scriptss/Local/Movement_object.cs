//Передвигает объект между 2-мя точками
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_object : MonoBehaviour
{
    [Tooltip("Стартовая точка движения")]
    [SerializeField]
    Transform Start_point = null;

    [Tooltip("Конечная точка движения")]
    [SerializeField]
    Transform End_point = null;

    [Tooltip("Скорость")]
    [SerializeField]
    float Speed = 0.01f;



    [Header("Внутриние параметры (не трогать, если не нужно)")]
    [Space(20)]
    [Tooltip("Меш объекта")]
    [SerializeField]
    Mesh Object_mesh = null;

    [Tooltip("Расположение на линии")]
    [Range(0f, 1f)]
    [SerializeField]
    float Step = 0;

    bool Movement_end = true;


    private void FixedUpdate()
    {
        if(Step < 1 && Movement_end)
        {
            Step += Speed;
        }
        else if(Movement_end)
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

        transform.localPosition = Vector3.Lerp(Start_point.position, End_point.position, Step);

    }



    private void OnDrawGizmos()
    {
        if (Start_point && End_point && Object_mesh)
        {
            int sigment_number = 20;

            Vector3 preveuse_point = Start_point.position;

            for (int x = 0; x < sigment_number; x++)
            {
                float perimeter = (float)x / sigment_number;
                Vector3 point = Vector3.Lerp(Start_point.position, End_point.position, perimeter);
                Gizmos.DrawLine(preveuse_point, point);
                preveuse_point = point;
            }

            Vector3 pos = Vector3.Lerp(Start_point.position, End_point.position, Step);

            Gizmos.DrawMesh(Object_mesh, pos, transform.rotation, transform.localScale - new Vector3(-0.001f, -0.001f, -0.001f));

            Gizmos.DrawMesh(Object_mesh, Start_point.position, transform.rotation, transform.localScale * 0.8f);
            Gizmos.DrawMesh(Object_mesh, End_point.position, transform.rotation, transform.localScale * 0.8f);
        }
    }

}
