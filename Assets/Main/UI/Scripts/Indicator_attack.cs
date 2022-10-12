using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator_attack : Singleton<Indicator_attack>
{

    [Tooltip("����� ����������")]
    [SerializeField]
    Image[] Indicators_array = new Image[0];

    private void Start()
    {
        All_off();
    }

    /// <summary>
    /// �������� ���������� ����������
    /// </summary>
    /// <param name="_id">Id ����������</param>
    /// <param name="_value">��������</param>
    public void Change_indicator(int _id, float _value)
    {
        Indicators_array[_id].fillAmount = _value;
    }


    /// <summary>
    /// �������� ��� ����������
    /// </summary>
    public void All_off()
    {
        foreach (Image _image in Indicators_array)
        {
            _image.fillAmount = 0;
        }
    }


    /// <summary>
    /// ������ ������ ���������� (�� ������� ������ ������)
    /// </summary>
    public int Find_out_Size_indicator
    {
        get
        {
            return Indicators_array.Length;
        }
    }

}
