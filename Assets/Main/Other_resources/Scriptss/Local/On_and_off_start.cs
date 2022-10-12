//Запустить или выключить объекты при старте игры
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_and_off_start : MonoBehaviour
{

    [Tooltip("Объекты которые нужно включить")]
    [SerializeField]
    GameObject[] On_objects = new GameObject[0];

    [Tooltip("Объекты которые нужно выключить")]
    [SerializeField]
    GameObject[] Off_objects = new GameObject[0];

    private void Start()
    {
        Active_objects(On_objects, true);
        Active_objects(Off_objects, false);
    }

    /// <summary>
    /// Включить или выключить список объектов
    /// </summary>
    /// <param name="_objects_array">Список объектов</param>
    /// <param name="_active">Активность</param>
    void Active_objects(GameObject[] _objects_array, bool _active)
    {
        for(int x = 0; x < _objects_array.Length; x++)
        {
            _objects_array[x].SetActive(_active);
        }
    }

}
