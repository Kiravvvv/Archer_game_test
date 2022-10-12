//Отображение мешей невидимых коллайдеров
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displaying_collider_meshes : MonoBehaviour
{

    [Tooltip("Рендер мешей")]
    [SerializeField]
    private Renderer[] Renderer_mesh_array = new Renderer[0];

    [Tooltip("Цвет меша")]
    [SerializeField]
    private Color Color_mesh = new Color(0.8f, 0f, 0f, 0.5f);

    [Tooltip("Выключить меши при запуске игры")]
    [SerializeField]
    bool Off_mesh_play = true;

    bool Play_bool = false;//Игра запущена


    private void Start()
    {
        Play_bool = true;

        if (Off_mesh_play)
        {
            for (int x = 0; x < Renderer_mesh_array.Length; x++)
            {
                Renderer_mesh_array[x].enabled = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Play_bool)
        {
            
            if (Renderer_mesh_array.Length > 0 && Renderer_mesh_array[0] != null && (Renderer_mesh_array[0].material.color != Color_mesh || Renderer_mesh_array[0].enabled == false))
            {
                for (int x = 0; x < Renderer_mesh_array.Length; x++)
                {
                    if (Renderer_mesh_array[x] != null)
                    {
                        Renderer_mesh_array[x].enabled = true;
                        Renderer_mesh_array[x].material.color = Color_mesh;
                    }
                }
            }


        }
    }

}
