//Здоровье 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, I_damage
{
    /// <summary>
    /// Делегат для событий относящихся к тому кто атакует 
    /// </summary>
    /// <param name="_killer">Атакующий</param>
    public delegate void Killer_attack(Game_character_abstract _killer);
    /// <summary>
    /// Переменная Делегата для событий относящихся к тому кто атакует 
    /// </summary>
    public Killer_attack Killer_attack_delegate;

    /// <summary>
    /// Событие когда здоровье закончилось
    /// </summary>
    [HideInInspector]
    public UnityEvent Dead_event = new UnityEvent();

    /// <summary>
    /// Событие когда наносится вред
    /// </summary>
    [HideInInspector]
    public UnityEvent Harm_event = new UnityEvent();

    [Space(20)]
    [Header("Основное")]

    [Tooltip("Количество жизней")]
    [SerializeField]
    protected int Health_active = 10;

    protected int Health_default = 0;//Параметр для манипуляции с жизнями

    [Tooltip("Не умирает")]
    [SerializeField]
    private bool No_death_bool = false;

    [Tooltip("Уничтожается сразу когда заканчивается здоровье")]
    [SerializeField]
    private bool Death_destroy_bool = false;

    private Game_character_abstract Killer = null;//Кто убил




    [Space(20)]
    [Header("Дополнительно")]

    [Tooltip("Скрипт тряпичной куклы (не обязательно)")]
    [SerializeField]
    Ragdoll_activity Ragdoll_script = null;

    [Tooltip("Скрипт мигания при получение урона (не обязательно)")]
    [SerializeField]
    Blinking_effect Blinking_effect_script = null;



    protected bool Alive_bool = true;//Является ли живым

    protected Transform My_transform = null;//Трансформ объекта 

    private void Start()
    {
        My_transform = transform;

        Health_default = Health_active;
    }

    /// <summary>
    /// Изменение здоровья
    /// </summary>
    /// <param name="_change">На какое значение изменить</param>
    protected virtual void Change_health(int _change)
    {

        Health_active += _change;

        if (Health_active <= 0)
        {
            Health_active = 0;

            if (!No_death_bool)
            {

                Death();
            }
        }
        else if (Health_active > Health_default)
        {
            Health_active = Health_default;
        }

    }

    /// <summary>
    /// Смерть/разрушение объекта
    /// </summary>
    public virtual void Death()
    {
        Alive_bool = false;

        Dead_event.Invoke();

        if (Ragdoll_script)
        {
            Ragdoll_script.Active_change(true);
        }
        else if (Death_destroy_bool)
        {
            Destroy(gameObject);
        }
           
    }

    /// <summary>
    /// Получение урона с указанием кто атаковал
    /// </summary>
    /// <param name="_damage">Значение урона</param>
    /// <param name="_killer">Кто атаковал</param>
    public virtual void Damage_add(int _damage, Game_character_abstract _killer)
    {
        Killer = _killer;

        if (Alive_bool)
        {
            Change_health(-_damage);

            Killer_attack_delegate?.Invoke(_killer);

            Harm_event.Invoke();

            if (Blinking_effect_script)
            Blinking_effect_script.Activation();

        }
    }

    public void Damage()
    {
        Damage_add(1, null);

    }

    public void Damage(int _damage)
    {
        Damage_add(_damage, null);
    }

    public void Damage(int _damage, Game_character_abstract _killer)
    {
        Damage_add(_damage, _killer);
    }

    /// <summary>
    /// Узнать жив ли персонаж
    /// </summary>
    public bool Find_out_Alive
    {
        get
        {
            return Alive_bool;
        }
    }
}
