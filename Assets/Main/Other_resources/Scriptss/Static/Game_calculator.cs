//Статический скрипт для игровых вычислений
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game_calculator
{

    /// <summary>
    /// Найти ближайший объект или дальний по отношению к заданному (основному) по координатам XYZ
    /// </summary>
    /// <param name="_main_object">Основной объект от которого будет идти поиск</param>
    /// <param name="_objects_array">Список объектов</param>
    /// <param name="_nearest">Будем искать ближайший ? (или дальний)</param>
    /// <returns></returns>
    public static Transform Find_by_distance_object_XYZ(Transform _main_object, Transform[] _objects_array, bool _nearest)
    {

        return Find_by_distance_object_XYZ(_main_object.position, _objects_array, _nearest);
    }

    public static Transform Find_by_distance_object_XYZ(Transform _main_object, List<Transform> _objects_list, bool _nearest)
    {
        Transform[] array = Convert_from_List_to_Array(_objects_list);

        return Find_by_distance_object_XYZ(_main_object, array, _nearest);
    }

    /// <summary>
    /// Найти ближайший объект или дальний по отношению к точке координат по XYZ
    /// </summary>
    /// <param name="_main_point">Точка от которой будет поиск</param>
    /// <param name="_objects_array">Список объектов</param>
    /// <param name="_nearest">Будем искать ближайший ? (или дальний)</param>
    /// <returns></returns>
    public static Transform Find_by_distance_object_XYZ(Vector3 _main_point, Transform[] _objects_array, bool _nearest)
    {
        Transform result = _objects_array[0];

        float distance_active = Vector3.Distance(_main_point, result.position);

        for (int x = 0; x < _objects_array.Length; x++)
        {

            float dist = Vector3.Distance(_main_point, _objects_array[x].position);

            if (_nearest)
            {
                if (distance_active > dist)
                {
                    distance_active = dist;
                    result = _objects_array[x];
                }
            }
            else
            {
                if (distance_active < dist)
                {
                    distance_active = dist;
                    result = _objects_array[x];
                }
            }

        }

        return result;
    }

    /// <summary>
    /// Найти ближайший объект или дальний по отношению к заданному (основному) по координатам XY
    /// </summary>
    /// <param name="_main_object">Основной объект от которого будет идти поиск</param>
    /// <param name="_objects_array">Список объектов</param>
    /// <param name="_nearest">Будем искать ближайший ? (или дальний)</param>
    /// <returns></returns>
    public static Transform Find_by_distance_object_XY(Transform _main_object, Transform[] _objects_array, bool _nearest)//Найти ближайший объект или дальний по отношению к заданному (основному) по координатам XY
    {

        return Find_by_distance_object_XY(_main_object.position, _objects_array, _nearest);
    }

    public static Transform Find_by_distance_object_XY(Transform _main_object, List<Transform> _objects_list, bool _nearest)//Найти ближайший объект или дальний по отношению к заданному (основному) по координатам XY
    {
        Transform[] array = Convert_from_List_to_Array(_objects_list);

        return Find_by_distance_object_XY(_main_object, array, _nearest);
    }

    /// <summary>
    /// Найти ближайший объект или дальний по отношению к точке координат по XY
    /// </summary>
    /// <param name="_main_point">Точка от которой будет поиск</param>
    /// <param name="_objects_array">Список объектов</param>
    /// <param name="_nearest">Будем искать ближайший ? (или дальний)</param>
    /// <returns></returns>
    public static Transform Find_by_distance_object_XY(Vector3 _main_point, Transform[] _objects_array, bool _nearest)
    {
        Transform result = _objects_array[0];

        float distance_active = Vector3.Distance(new Vector3(_main_point.x, _main_point.y, 0), new Vector3(result.position.x, result.position.y, 0));

        for (int x = 0; x < _objects_array.Length; x++)
        {

            float dist = Vector3.Distance(new Vector3(_main_point.x, _main_point.y, 0), new Vector3(_objects_array[x].position.x, _objects_array[x].position.y, 0));

            if (_nearest)
            {
                if (distance_active > dist)
                {
                    distance_active = dist;
                    result = _objects_array[x];
                }
            }
            else
            {
                if (distance_active < dist)
                {
                    distance_active = dist;
                    result = _objects_array[x];
                }
            }

        }

        return result;
    }

    /// <summary>
    /// Найти ближайший объект или дальний по отношению к заданному (основному) по координатам XZ
    /// </summary>
    /// <param name="_main_object">Основной объект от которого будет идти поиск</param>
    /// <param name="_objects_array">Список объектов</param>
    /// <param name="_nearest">Будем искать ближайший ? (или дальний)</param>
    /// <returns></returns>
    public static Transform Find_by_distance_object_XZ(Transform _main_object, Transform[] _objects_array, bool _nearest)
    {

        return Find_by_distance_object_XZ(_main_object.position, _objects_array, _nearest);
    }

    public static Transform Find_by_distance_object_XZ(Transform _main_object, List<Transform> _objects_list, bool _nearest)
    {
        Transform[] array = Convert_from_List_to_Array(_objects_list);

        return Find_by_distance_object_XZ(_main_object, array, _nearest);
    }


    /// <summary>
    /// Найти ближайший объект или дальний по отношению к точке координат по XZ
    /// </summary>
    /// <param name="_main_point">Точка от которой будет поиск</param>
    /// <param name="_objects_array">Список объектов</param>
    /// <param name="_nearest">Будем искать ближайший ? (или дальний)</param>
    /// <returns></returns>
    public static Transform Find_by_distance_object_XZ(Vector3 _main_point, Transform[] _objects_array, bool _nearest)//Найти ближайший объект или дальний по отношению к заданному (основному) по координатам XZ
    {
        Transform result = _objects_array[0];

        float distance_active = Vector3.Distance(new Vector3(_main_point.x, 0, _main_point.z), new Vector3(result.position.x, 0, result.position.z));

        for (int x = 0; x < _objects_array.Length; x++)
        {

            float dist = Vector3.Distance(new Vector3(_main_point.x, 0, _main_point.z), new Vector3(_objects_array[x].position.x, 0, _objects_array[x].position.z));

            if (_nearest)
            {
                if (distance_active > dist)
                {
                    distance_active = dist;
                    result = _objects_array[x];
                }
            }
            else
            {
                if (distance_active < dist)
                {
                    distance_active = dist;
                    result = _objects_array[x];
                }
            }

        }

        return result;
    }

    /// <summary>
    /// Преобразовать из Листа в Список
    /// </summary>
    /// <typeparam name="T">Получаемый список</typeparam>
    /// <param name="_list">Лист который будет преобразован</param>
    /// <returns></returns>
    public static T[] Convert_from_List_to_Array<T>(List<T> _list)
    {
        T[] result = new T[_list.Count];

        for(int x = 0; x < _list.Count; x++)
        {
            result[x] = _list[x];
        }

        return result;
    }

    /// <summary>
    /// Преобразовать из Списка в Лист
    /// </summary>
    /// <typeparam name="T">Получаемый лист</typeparam>
    /// <param name="_array">Список который будет преобразован</param>
    /// <returns></returns>
    public static List<T> Convert_from_Array_to_List<T>(T[] _array)
    {
        List<T> result = new List<T>();

        for (int x = 0; x < _array.Length; x++)
        {
            result.Add(_array[x]);
        }

        return result;
    }

    /// <summary>
    /// Узнать есть ли между объектами препятствия
    /// </summary>
    /// <param name="_main_object">Основной объект</param>
    /// <param name="_target">Цель</param>
    /// <returns></returns>
    public static bool Checking_obstacles_between_two_objects(Transform _main_object, Transform _target)
    {
        bool result = false;

        if(Physics.Linecast(_main_object.position, _target.position))
        {
            result = true;
        }

        return result;
    }

    /// <summary>
    /// Узнать есть ли между объектами препятствия с учётом слоя(маски) препятсвия
    /// </summary>
    /// <param name="_main_object">Основной объект</param>
    /// <param name="_target">Цель</param>
    /// <param name="_layer">Слой(маска)</param>
    /// <returns></returns>
    public static bool Checking_obstacles_between_two_objects(Transform _main_object, Transform _target, LayerMask _layer)
    {
        bool result = false;

        if (Physics.Linecast(_main_object.position, _target.position, 1 << _layer))
        {
            result = true;
        }

        return result;
    }

    /// <summary>
    /// Перевести целое число в процент от 0 до 100
    /// </summary>
    /// <param name="_value">Число</param>
    /// <returns></returns>
    public static float Integer_convert_to_percentage(float _value)
    {
        return _value / 100;
    }


    /// <summary>
    /// Узнать направление от точки к точке в 3D
    /// </summary>
    /// <param name="_point_start">Стартовая точка</param>
    /// <param name="_point_end">Конечная точка</param>
    /// <param name="_normalize">Нужно ли нормализовать направление</param>
    /// <returns></returns>
    public static Vector3 Find_out_Direction_3D(Vector3 _point_start, Vector3 _point_end, bool _normalize)
    {
        Vector3 result = Vector3.zero;

        result = _point_end - _point_start;

        if (_normalize)
            result.Normalize();

        return result;
    }



    /// <summary>
    /// Узнать направление от точки к точке в 3D
    /// </summary>
    /// <param name="_point_start">Стартовый объект</param>
    /// <param name="_point_end">Конечный объект</param>
    /// <param name="_normalize">Нужно ли нормализовать направление</param>
    /// <returns></returns>
    public static Vector3 Find_out_Direction_3D(Transform _point_start, Transform _point_end, bool _normalize)
    {
        Vector3 result = Find_out_Direction_3D(_point_start.position, _point_end.position, _normalize);

        return result;
    }



    /// <summary>
    /// Узнать направление от точки к точке в 2D
    /// </summary>
    /// <param name="_point_start">Стартовая точка</param>
    /// <param name="_point_end">Конечная точка</param>
    /// <param name="_normalize">Нужно ли нормализовать направление</param>
    /// <returns></returns>
    public static Vector2 Find_out_Direction_2D(Vector2 _point_start, Vector2 _point_end, bool _normalize)
    {
        Vector2 result = Vector2.zero;

        result = _point_end - _point_start;

        if (_normalize)
            result.Normalize();

        return result;
    }


    /// <summary>
    /// Узнать направление от точки к точке в 2D
    /// </summary>
    /// <param name="_point_start">Стартовый объект</param>
    /// <param name="_point_end">Конечный объект</param>
    /// <param name="_normalize">Нужно ли нормализовать направление</param>
    /// <returns></returns>
    public static Vector2 Find_out_Direction_2D(Transform _point_start, Transform _point_end, bool _normalize)
    {
        Vector2 result = Find_out_Direction_2D(_point_start.position, _point_end.position, _normalize);

        return result;
    }


}
