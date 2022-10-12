//Создание кривой безье
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game_Bezier_curve
{

    /// <summary>
    /// Узнать точку на кривой из 3 точек
    /// </summary>
    /// <param name="_point_start">Стартовая точка</param>
    /// <param name="_point_1">Средняя точка</param>
    /// <param name="_point_end">Последняя точка</param>
    /// <param name="_step">Шаг (промежуток кривой)</param>
    /// <returns></returns>
    public static Vector3 Get_point_Bezier(Vector3 _point_start, Vector3 _point_1, Vector3 _point_end, float _step)
    {
        Vector3 part_1_1 = Vector3.Lerp(_point_start, _point_1, _step);
        Vector3 part_1_2 = Vector3.Lerp(_point_1, _point_end, _step);

        Vector3 part_2_1 = Vector3.Lerp(part_1_1, part_1_2, _step);

        return part_2_1;
    }

    /// <summary>
    /// Узнать точку на кривой из 4 точек
    /// </summary>
    /// <param name="_point_start">Стартовая точка</param>
    /// <param name="_point_1">1 средняя точка</param>
    /// <param name="_point_2">2 средняя точка</param>
    /// <param name="_point_end">Последняя точка</param>
    /// <param name="_step">Шаг (промежуток кривой)</param>
    /// <returns></returns>
    public static Vector3 Get_point_Bezier(Vector3 _point_start, Vector3 _point_1, Vector3 _point_2, Vector3 _point_end, float _step)
    {
        Vector3 part_1_1 = Vector3.Lerp(_point_start, _point_1, _step);
        Vector3 part_1_2 = Vector3.Lerp(_point_1, _point_2, _step);
        Vector3 part_1_3 = Vector3.Lerp(_point_2, _point_end, _step);

        Vector3 part_2_1 = Vector3.Lerp(part_1_1, part_1_2, _step);
        Vector3 part_2_2 = Vector3.Lerp(part_1_2, part_1_3, _step);

        Vector3 part_3_1 = Vector3.Lerp(part_2_1, part_2_2, _step);

        return part_3_1;
    }

    /// <summary>
    /// Узнать точку на кривой из массива точек
    /// </summary>
    /// <param name="_point_array">Список точек</param>
    /// <param name="_step">Шаг (промежуток кривой)</param>
    /// <returns></returns>
    public static Vector3 Get_point_Bezier(Vector3[] _point_array, float _step)
    {
        Vector3 part_1_1 = Vector3.Lerp(_point_array[0], _point_array[1], _step);
        Vector3 part_1_2 = Vector3.Lerp(_point_array[1], _point_array[2], _step);
        Vector3 part_1_3 = Vector3.Lerp(_point_array[2], _point_array[3], _step);

        Vector3 part_2_1 = Vector3.Lerp(part_1_1, part_1_2, _step);
        Vector3 part_2_2 = Vector3.Lerp(part_1_2, part_1_3, _step);

        Vector3 part_3_1 = Vector3.Lerp(part_2_1, part_2_2, _step);

        return part_3_1;
    }


}
