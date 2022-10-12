//Обычный объект
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Game_object_abstract : MonoBehaviour
{
    [Space(20)]
    [Header("Настройки объекта")]

    [Tooltip("Имя объекта (если есть)")]
    public string Name = "Имя объекта";

    [Tooltip("Аниматор (если есть)")]
    [SerializeField]
    protected Animator Anim = null;

    [Tooltip("Скрипт для управления звуками")]
    [SerializeField]
    protected Sound_control Sound_control_ = null;

    protected Transform My_transform = null;//Трансформ объекта 

    protected virtual void Start()
    {
        My_transform = transform;
    }

    /// <summary>
    /// Узнать имя объекта
    /// </summary>
    public string Find_out_Name
    {
        get
        {
            return Name;
        }
    }

}
