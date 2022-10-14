using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_script : MonoBehaviour
{

    #region Переменные

    [Tooltip("Скорость поворота")]
    [SerializeField]
    float Speed_rotation = 0.1f;

    [Tooltip("Скорость зарядки атаки")]
    [SerializeField]
    float Speed_up_charger_attack = 0.1f;

    [Tooltip("Максимальный размер стамины")]
    [SerializeField]
    float Stamina = 100f;

    float Stamina_active = 0;//Параметр для работы со стаминой

    [Tooltip("Скорость востановления стамины")]
    [SerializeField]
    float Recovery_stamina = 2f;

    [Tooltip("Затраты стамины за атаку")]
    [SerializeField]
    float Cost_stamina_attack = 30f;

    [Tooltip("Картинка показывающая значение стамины")]
    [SerializeField]
    Image Stamina_value_image = null;

    [Tooltip("Скрипт оружия")]
    [SerializeField]
    Firearm Weapon_script = null;

    [Tooltip("Урон в зависимости от натяжения выстрела")]
    [SerializeField]
    int[] Step_damage_array = new int[0];

    [Tooltip("Аниматор")]
    [SerializeField]
    Animator Anim = null;

    [Tooltip("Скрипт слежения камеры")]
    [SerializeField]
    Camera_tracking Camera_tracking_script = null;

    Transform Target = null;

    bool[] End_charger_bool_array = new bool[0];
    float[] Charger_array = new float[0];

    int Step_attack_fin = 0;//На какой степени зарядки был сделан выстрел

    float Charger_value = 0;//Зарядка

    bool Active_bool = false;

    bool Attack_bool = false;//Сейчас заняты стрельбой

    #endregion

    #region MonoBehaviour Callbacks
    protected void Awake()
    {
        Stamina_active = Stamina;

        Game_administrator.Instance.Add_player_script(this);
        Game_administrator.Start_game_event.AddListener(Start_game);

        int size_indicator = Indicator_attack.Instance.Find_out_Size_indicator;
        End_charger_bool_array = new bool[size_indicator];
        Charger_array = new float[size_indicator];
    }

    private void Start()
    {
        New_enemy_target();
    }


    protected void Update()
    {
        if (Active_bool)
        {
            if (Input.GetKey(KeyCode.Mouse0) && Cost_stamina_attack <= Stamina_active)
            {
                Charger_up_attack();
                Anim.Play("Draw Arrow");
            }
            else
            {
                Off_charger_attack();
            }

            Rotation_look_to_target();

            if(Stamina_active < Stamina && !Attack_bool)
            {
                Stamina_active += Recovery_stamina * Time.deltaTime;
                Stamina_value_image.fillAmount = Stamina_active/Stamina;
            }
        }


    }
    #endregion

    #region Методы
    /// <summary>
    /// Зарядка атаки
    /// </summary>
    void Charger_up_attack()
    {
        Attack_bool = true;

        float param_Charger = 1f / End_charger_bool_array.Length;

        for (int x = 0; x < End_charger_bool_array.Length; x++)
        {
            if (End_charger_bool_array[x] == false)
            {
                Charger_array[x] += Speed_up_charger_attack * Time.deltaTime;
                Indicator_attack.Instance.Change_indicator(x, Charger_array[x]);

                Step_attack_fin = x;

                Charger_value = ((param_Charger * x) + param_Charger * Charger_array[x]);
                Game_HC_UI.Instance.Change_size_aim(1f - Charger_value + 0.1f);

                if (Charger_array[x] >= 1)
                {
                    End_charger_bool_array[x] = true;
                }
                break;
            }
        }

        if (End_charger_bool_array[End_charger_bool_array.Length - 1] == true)
        {
            Off_charger_attack();
        }
    }

    void Start_game()
    {
        Active_bool = true;
    }

    /// <summary>
    /// Обнулить зарядку атаки
    /// </summary>
    void Off_charger_attack()
    {
        if (Charger_array[0] != 0)
        {
            Attack_bool = false;
            Stamina_active -= Cost_stamina_attack;
            Stamina_value_image.fillAmount = Stamina_active / Stamina;

            Fire();
            
            Anim.Play("Aim Recoil");
            for (int x = 0; x < End_charger_bool_array.Length; x++)
            {
                End_charger_bool_array[x] = false;
                Charger_array[x] = 0;
            }

            Game_HC_UI.Instance.Change_size_aim(1);
            Indicator_attack.Instance.All_off();
        }

    }

    void Fire()
    {
        if (Charger_value > 1)
            Charger_value = 1;

        if (Target)
        {
            Weapon_script.New_target(Target);
            Weapon_script.Fire(Target.position, 1f - Charger_value, Step_damage_array[Step_attack_fin]);
        }
        else
            Weapon_script.Fire(1f - Charger_value);
    }






    /// <summary>
    /// Поворот в сторону цели
    /// </summary>
    void Rotation_look_to_target()
    {
        if (Target)
        {
            Quaternion new_rotation = Quaternion.LookRotation(new Vector3(Target.position.x, transform.position.y, Target.position.z) - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new_rotation, Speed_rotation);
        }

    }

    /// <summary>
    /// Найти новую цель
    /// </summary>
    public void New_enemy_target()
    {
        if (Game_administrator.Instance.Find_out_Enemy_list.Count > 0)
        {
            Target = Game_calculator.Find_by_distance_object_XZ(transform, Game_administrator.Instance.Find_out_Enemy_list, true).GetComponent<AI_enemy_standart>().Find_out_Head;
            Camera_tracking_script.New_look_target(Target);
        }

    }


    /// <summary>
    /// Активировать особую атаку
    /// </summary>
    /// <param name="_id_super_attack"></param>
    public void Activation_super_attack(int _id_super_attack)
    {
        Weapon_script.Mode_attack(_id_super_attack);
    }

    #endregion

}
