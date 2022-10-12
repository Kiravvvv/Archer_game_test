//Замедление (возможно сделать и в ускорение) объекта
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Slow_motion_change : MonoBehaviour
{

    protected float Time_dilation_factor = 0.1f;

    bool Active_bool = false;

    protected virtual void Start()
    {

        if (Game_administrator.Instance)
        {
            Time_dilation_factor = Game_administrator.Instance.Find_out_Time_dilation_factor;
            Game_administrator.Time_dilation_event.AddListener(Time_dilation_active);

            if (Game_administrator.Instance.Find_out_time_dilation_bool)
            {
                Time_dilation_active(true);
            }

        }
    }

    private void OnDisable()
    {
        if (Game_administrator.Instance)
        {
            Game_administrator.Time_dilation_event.RemoveListener(Time_dilation_active);
        }
    }

    /// <summary>
    /// Включить замедление или вернуть время в норму
    /// </summary>
    /// <param name="_active">Активность</param>
    public void Time_dilation_active(bool _active)
    {
        if (_active && !Active_bool)
        {
            On_dilation();
            Active_bool = true;
        }

        else if (!_active && Active_bool)
        {
            Off_dilation();
            Active_bool = false;
        }
           
    }

    /// <summary>
    /// Абстракт при включение замедления
    /// </summary>
    protected abstract void On_dilation();

    /// <summary>
    /// Абстракт при выключение замедления
    /// </summary>
    protected abstract void Off_dilation();

}
