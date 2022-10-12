using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator_attack : Singleton<Indicator_attack>
{

    [Tooltip("Этапы индикатора")]
    [SerializeField]
    Image[] Indicators_array = new Image[0];

    private void Start()
    {
        All_off();
    }

    /// <summary>
    /// Изменить показатель индикатора
    /// </summary>
    /// <param name="_id">Id индикатора</param>
    /// <param name="_value">значение</param>
    public void Change_indicator(int _id, float _value)
    {
        Indicators_array[_id].fillAmount = _value;
    }


    /// <summary>
    /// Обнулить все индикаторы
    /// </summary>
    public void All_off()
    {
        foreach (Image _image in Indicators_array)
        {
            _image.fillAmount = 0;
        }
    }


    /// <summary>
    /// Узнать размер индикатора (на сколько частей поделён)
    /// </summary>
    public int Find_out_Size_indicator
    {
        get
        {
            return Indicators_array.Length;
        }
    }

}
