//Скрипт общего управления во время игры
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Game_administrator : Singleton<Game_administrator>
{
    /// <summary>
    /// Событие относящихся к контролю игрока 
    /// </summary>
    /// <param name="_bool">Параметр активации</param>
    public static UnityEvent<bool> Player_control_event = new UnityEvent<bool>();


    /// <summary>
    /// Событие относящихся к замедлению
    /// </summary>
    /// <param name="_active">Параметр активации</param>
    public static UnityEvent<bool> Time_dilation_event = new UnityEvent<bool>();

    /// <summary>
    /// Событие относящихся к началу игры
    /// </summary>
    public static UnityEvent Start_game_event = new UnityEvent();

    /// <summary>
    /// Событие относящихся к концу игры
    /// </summary>
    /// <param name="_active">Параметр активации</param>
    public static UnityEvent<bool> End_game_event = new UnityEvent<bool>();

    /// <summary>
    /// Событие относящихся к показанию жизней игрока
    /// </summary>
    /// <param name="_active">Значение от 0 до 1</param>
    public static UnityEvent<float> Player_Health_info_event = new UnityEvent<float>();

    /// <summary>
    /// Событие относящихся к получению урона игроком
    /// </summary>
    public static UnityEvent Player_add_damage_event = new UnityEvent();


    [Tooltip("Фактор замедления")]
    [SerializeField]
    protected float Time_dilation_factor = 0.01f;

    bool Time_dilation_bool = false;//Режим времени (замедленный ли)

    Player_script Player_sc = null;//Скрипт игрока

    List<Transform> Enemy_list = new List<Transform>();


    /// <summary>
    /// Нормализовать время
    /// </summary>
    void Normal_time()
    {
        if (!Time_dilation_bool)
            Time_dilation_active(false);
    }




    public void Add_enemy_list(Transform _enemy)
    {
        Enemy_list.Add(_enemy);
        Player_sc.New_enemy_target();
    }

    public void Remove_enemy_list(Transform _enemy)
    {
        Enemy_list.Remove(_enemy);

        if(Enemy_list.Count > 0 && Player_sc != null)
        Player_sc.New_enemy_target();
    }

    public List<Transform> Find_out_Enemy_list
    {
        get
        {
            return Enemy_list;
        }
    }


    /// <summary>
    ///  Сменить активность контроля игрока над персонажем
    /// </summary>
    /// <param name="_active">Включение или выключение</param>
    public void Player_control_activity(bool _active)
    {
        Player_control_event.Invoke(_active);
    }


    /// <summary>
    /// Начать игру
    /// </summary>
    public void Start_game()
    {
        Player_control_event.Invoke(true);
        Start_game_event.Invoke();

    }

    /// <summary>
    /// Закончить игру
    /// </summary>
    /// <param name="_win">Победа?</param>
    public void End_game(bool _win)
    {
        Normal_time();

        Player_control_event.Invoke(false);
        End_game_event.Invoke(_win);

    }



    /// <summary>
    /// Включить замедление времени
    /// </summary>
    /// <param name="_active">Включить замедление?</param>
    public void Time_dilation_active(bool _active)
    {
        if (_active != Time_dilation_bool)
        {
            Time_dilation_event.Invoke(_active);
            Time_dilation_bool = _active;
        }
    }

    /// <summary>
    /// Узнать замедлено время сейчас ли
    /// </summary>
    public bool Find_out_time_dilation_bool
    {
        get
        {
            return Time_dilation_bool;
        }
    }

    /// <summary>
    /// Узнать фактор замедления
    /// </summary>
    public float Find_out_Time_dilation_factor
    {
        get
        {
            return Time_dilation_factor;
        }
    }

    /// <summary>
    /// Добавить игрока для обращения
    /// </summary>
    /// <param name="_player_script"></param>
    public void Add_player_script(Player_script _player_script)
    {
        Player_sc = _player_script;
    }


    /// <summary>
    /// Получить ссылку на скрипт игрока
    /// </summary>
    public Player_script Find_out_Player_script
    {
        get
        {
            return Player_sc;
        }
    }

    /// <summary>
    /// Изменить показатели жизней
    /// </summary>
    /// <param name="_value">Значение от 0 до 1</param>
    public void Player_Health_info(float _value)
    {
        Player_Health_info_event.Invoke(_value);
    }

    /// <summary>
    /// Получить урон (анимация мигания)
    /// </summary>
    public void Player_add_damage()
    {
        Player_add_damage_event.Invoke();
    }


}
