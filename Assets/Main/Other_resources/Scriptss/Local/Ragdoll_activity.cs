//Скрипт для включения и отключения Тряпичной куклы(Ragdoll)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ragdoll_activity : MonoBehaviour
{
    #region Переменные
    /// <summary>
    /// Событие когда валяется мешком картошки
    /// </summary>
    [HideInInspector]
    public UnityEvent<bool> Ragdoll_event = new UnityEvent<bool>();

    [Space(20)]
    [Header("Основное")]

    [Tooltip("Аниматор персонажа")]
    [SerializeField]
    Animator Anim = null;

    [Tooltip("Коллайдеры основные (не куклы)")]
    [SerializeField]
    Collider[] Collider_base_array = new Collider[0];

    [Tooltip("Физика всех частей основные (не куклы)")]
    [SerializeField]
    Rigidbody[] Rigidbody_base_array = new Rigidbody[0];



    [Space(20)]
    [Header("Кукла")]

    [Tooltip("Главная кость (от которой всё начинается)")]
    [SerializeField]
    Transform Main_bone = null;

    [Tooltip("Коллайдеры куклы")]
    [SerializeField]
    Collider[] Collider_array = new Collider[0];

    [Tooltip("Физика всех частей куклы")]
    [SerializeField]
    Rigidbody[] Rigidbody_array = new Rigidbody[0];

    [Tooltip("Активирует тряпичную куклу при старте сцены")]
    [SerializeField]
    bool Start_ragdoll = false;

    [Tooltip("Время за которое кости примут исходное положения перед включением анимации вставания")]
    [SerializeField]
    float Time_reset_bones_get_up = 5f;

    [Tooltip("Имя анимации когда персонаж встаёт лежа на спине")]
    [SerializeField]
    string Name_clip_Get_up_back = "Get_up_back";

    [Tooltip("Имя анимации когда персонаж встаёт лежа на животе")]
    [SerializeField]
    string Name_clip_Get_up_belly = "Get_up_belly";

    [Tooltip("Будет автоматически сам вставать")]
    [SerializeField]
    bool Auto_get_up_bool = false;

    [Tooltip("Через какое время автоматически встаёт сам")]
    [SerializeField]
    float Time_Auto_get_up = 2f;

    Transform[] Bones_array = new Transform[0];//Все кости

    Bone_transform[] Get_up_back_Bones_array = new Bone_transform[0];//Запоминание костей при подъёме лежа на спине (начало анимации)
    Bone_transform[] Get_up_belly_Bones_array = new Bone_transform[0];//Запоминание костей при подъёме лежа на спине (начало анимации)
    Bone_transform[] Ragdoll_Bones_array = new Bone_transform[0];//Запоминание костей как лежат у тряпичной куклы (до включения анимации)

    bool Get_up_bool = false;//Занят вставанием

    bool Active_bool = false;//Активность состояния включения регдолла

    bool Off_bool = false;//ОТключить работу




    [Space(20)]
    [Header("HurtBox")]

    [Tooltip("Основной скрипт здоровья для назначения")]
    [SerializeField]
    Health Health_script = null;

    bool HurtBox_mode_bool = false;//Теперь части куклы считаются HurtBox'сами

    #endregion

    #region MonoBehaviour
    private void Start()
    {
        GetComponent<Health>().Dead_event.AddListener(Off);

        Preparation_bones();

        Activity_ragdoll(false);

        if (Start_ragdoll)
        {
            Active_change(true);
        }

        Save_transform_bones_clips();

    }

    private void Update()
    {
        if (Auto_get_up_bool)
        {
            if (Active_bool && !Get_up_bool)
            {
                if (Rigidbody_array[0].IsSleeping())
                {
                    StartCoroutine(Coroutine_Auto_get_up());
                }
            }
        }

    }
    #endregion

    #region Методы
    void Off()
    {
        Off_bool = true;
    }

    /// <summary>
    /// Подготовка для работы с параметрами костей
    /// </summary>
    void Preparation_bones()
    {
        Bones_array = Main_bone.GetComponentsInChildren<Transform>();
        Get_up_back_Bones_array = new Bone_transform[Bones_array.Length];
        Get_up_belly_Bones_array = new Bone_transform[Bones_array.Length];
        Ragdoll_Bones_array = new Bone_transform[Bones_array.Length];

        for (int bone_index = 0; bone_index < Bones_array.Length; bone_index++)
        {
            Get_up_back_Bones_array[bone_index] = new Bone_transform();
            Get_up_belly_Bones_array[bone_index] = new Bone_transform();
            Ragdoll_Bones_array[bone_index] = new Bone_transform();
        }
    }

    /// <summary>
    /// Корутина на автоматическое вставание
    /// </summary>
    /// <returns></returns>
    IEnumerator Coroutine_Auto_get_up()
    {
        Get_up_bool = true;
        yield return new WaitForSeconds(Time_Auto_get_up);
        if(!Off_bool)
        Up_ragdoll();
    }

    /// <summary>
    /// Изменить активность куклы
    /// </summary>
    /// <param name="_active">Активность</param>
    public void Active_change(bool _active)
    {
        
        if (!Active_bool)
        {
            Active_bool = _active;
            Activity_ragdoll(true);
            Anim.enabled = false;
            StopAllCoroutines();
            Ragdoll_event.Invoke(false);
        }
        else if (Active_bool && !Get_up_bool)
        {
            Up_ragdoll();
        }

    }


    /// <summary>
    /// Изменить активность основной физики и коллайдеров
    /// </summary>
    /// <param name="_active">Активность</param>
    public void Activity_base_collider_and_rigibody(bool _active)
    {
        foreach (Collider collider in Collider_base_array)
        {
            collider.enabled = _active;
        }

        foreach (Rigidbody rigidbody in Rigidbody_base_array)
        {
            rigidbody.useGravity = _active;

            rigidbody.isKinematic = !_active;
        }
    }

        /// <summary>
        /// Изменить активность частей куклы
        /// </summary>
        /// <param name="_active">Активность</param>
        void Activity_ragdoll(bool _active)
    {
        foreach (Collider collider in Collider_base_array)
        {
            collider.enabled = !_active;
        }

        foreach (Rigidbody rigidbody in Rigidbody_base_array)
        {
            rigidbody.useGravity = !_active;

            rigidbody.isKinematic = _active;
        }

        foreach (Collider collider in Collider_array)
        {
            if (HurtBox_mode_bool)
            {
                if (collider.enabled == false)
                    collider.enabled = true;

                collider.isTrigger = !_active;
            }
            else
            {
                collider.enabled = _active;
            }
        }

        foreach (Rigidbody rigidbody in Rigidbody_array)
        {
            rigidbody.useGravity = _active;

            rigidbody.isKinematic = !_active;
        }
    }


    /// <summary>
    /// Настройка куклы когда она должна встать
    /// </summary>
    void Up_ragdoll()
    {
        Get_up_bool = true;
        //Проверяем лежит ли он на животе (если нет, значит он на спине)
        bool back_result = true;
        if (Physics.Raycast(Bones_array[0].transform.position, Bones_array[0].transform.forward, out RaycastHit hitInf3o, 10))
        {
            if (hitInf3o.point != null)
                back_result = false;
        }


        //Меняем позицию в целом и центруем
        Vector3 save_position_hip = Rigidbody_array[0].transform.position;
        Quaternion save_rotation_hip = Rigidbody_array[0].transform.rotation;

        transform.position = save_position_hip;

        float direction_constant = 1;
        if (back_result)
        {
            direction_constant = -1;
        }


        Vector3 pos_target_look = Rigidbody_array[0].transform.position + Rigidbody_array[0].transform.up * (10f * direction_constant);
        pos_target_look.y = transform.position.y;

        transform.LookAt(pos_target_look);
        
        //Повернуть в направление лежачей куклы
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo))
        {
            transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
        }

        //Повторно центруемм, но уже тряпичную куклу
        Rigidbody_array[0].transform.position = save_position_hip;
        Rigidbody_array[0].transform.rotation = save_rotation_hip;

        //Сохраняем координаты костей
        Save_transform_bones(Ragdoll_Bones_array);


        //Запускаем выставление костей на исходные позиции
        Activity_ragdoll(false);

        Anim.SetBool("Get_up_back", back_result);
        StartCoroutine(Coroutine_reset_bones(back_result));

    }

    /// <summary>
    /// Полностью поднялся
    /// </summary>
    void Get_Up_animation_final()
    {
        Ragdoll_event.Invoke(true);
    }

    /// <summary>
    /// Включает выставление костей на нужные позиции
    /// </summary>
    /// <returns></returns>
    IEnumerator Coroutine_reset_bones(bool _back)
    {
        float active_time = 0;//Сколько времени прошло

        float final_value = 0;//Когда будет 1, то должно всё закончится

        while (final_value < 1)
        {
            yield return new WaitForSeconds(Time.deltaTime);

            active_time += Time.deltaTime;

            final_value = active_time / Time_reset_bones_get_up;

            for(int bone_index = 0; bone_index < Bones_array.Length; bone_index++)
            {
                if (_back)
                {
                    Bones_array[bone_index].transform.localPosition = Vector3.Lerp(Ragdoll_Bones_array[bone_index].Position, Get_up_back_Bones_array[bone_index].Position, final_value);
                    Bones_array[bone_index].transform.localRotation = Quaternion.Lerp(Ragdoll_Bones_array[bone_index].Rotation, Get_up_back_Bones_array[bone_index].Rotation, final_value);
                }
                else
                {
                    Bones_array[bone_index].transform.localPosition = Vector3.Lerp(Ragdoll_Bones_array[bone_index].Position, Get_up_belly_Bones_array[bone_index].Position, final_value);
                    Bones_array[bone_index].transform.localRotation = Quaternion.Lerp(Ragdoll_Bones_array[bone_index].Rotation, Get_up_belly_Bones_array[bone_index].Rotation, final_value);
                }

            }

        }

        Anim.enabled = true;

        if (_back)
            Anim.Play(Name_clip_Get_up_back,0,0);
        else
            Anim.Play(Name_clip_Get_up_belly, 0, 0);

        Anim.SetTrigger("Get_up");
        Get_up_bool = false;
        Active_bool = false;
    }

    /// <summary>
    /// Сохранить позицию и поворот костей в списке
    /// </summary>
    /// <param name="_bones_array">список для записи</param>
    void Save_transform_bones(Bone_transform[] _bones_array)
    {
        for(int bone_index = 0; bone_index < Bones_array.Length; bone_index++)
        {
            _bones_array[bone_index].Position = Bones_array[bone_index].localPosition;
            _bones_array[bone_index].Rotation = Bones_array[bone_index].localRotation;
        }
    }


    void Save_transform_bones_clips()
    {
        //Vector3 p = transform.position;
        //Quaternion q = transform.rotation;

        foreach(AnimationClip clip in Anim.runtimeAnimatorController.animationClips)
        {
            if (clip.name == Name_clip_Get_up_back)
            {
                clip.SampleAnimation(gameObject, 0);
                Save_transform_bones(Get_up_back_Bones_array);
                break;
            }
        }

        foreach (AnimationClip clip in Anim.runtimeAnimatorController.animationClips)
        {
            if (clip.name == Name_clip_Get_up_belly)
            {
                clip.SampleAnimation(gameObject, 0);
                Save_transform_bones(Get_up_belly_Bones_array);
                break;
            }
        }

        //transform.position = p;
        //transform.rotation = q;

    }


    /// <summary>
    /// Толкнуть куклу в направление
    /// </summary>
    /// <param name="_direction"></param>
    /// <param name="_force"></param>
    public void Add_impulse_ragdoll(Vector3 _direction, float _force, ForceMode _force_mode)
    {
        for (int x = 0; x < Rigidbody_array.Length; x++)
        {
                Rigidbody_array[x].AddForce((_direction) * _force, _force_mode);

        }
    }

    #endregion

    #region Дополнительно


    /// <summary>
    /// Автоматически устанавливает скрипт передающий получаемый урон от частей куклы в скрипт здоровья
    /// </summary>
    public void Auto_add_HurtBox_component()
    {
        if (Health_script)
        {
            for (int x = 0; x < Collider_array.Length; x++)
            {
                if (Collider_array[x].GetComponent<HurtBox>())
                {
                    Collider_array[x].GetComponent<HurtBox>().Add_Main_health(Health_script);
                }
                else
                {
                    Collider_array[x].gameObject.AddComponent<HurtBox>().Add_Main_health(Health_script);
                }
            }
            HurtBox_mode_bool = true;
            Activity_ragdoll(false);
        }
        else
        {
            print("Не указан основной Health скрипт");
        }

    }


    /// <summary>
    /// Автоматически находит компоненты куклы
    /// </summary>
    public void Auto_Find_components()
    {
        if (!Anim)
        {
            if (GetComponent<Animator>())
                Anim = GetComponent<Animator>();
        }

        if (Collider_base_array.Length == 0)
        {
            if (GetComponent<Collider>())
            {
                Collider_base_array = new Collider[1];
                Collider_base_array[0] = GetComponent<Collider>();
            }     
        }

        if (Rigidbody_base_array.Length == 0)
        {
            if (GetComponent<Rigidbody>())
            {
                Rigidbody_base_array = new Rigidbody[1];
                Rigidbody_base_array[0] = GetComponent<Rigidbody>();
            }   
        }




        List<Collider> collider_list = new List<Collider>();
        List<Rigidbody> rigidbody_list = new List<Rigidbody>();

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child != transform && child.GetComponent<Collider>())
            {
                collider_list.Add(child.GetComponent<Collider>());
            }

            if (child != transform && child.GetComponent<Rigidbody>())
            {
                rigidbody_list.Add(child.GetComponent<Rigidbody>());
            }

        }

        Collider_array = Game_calculator.Convert_from_List_to_Array(collider_list);
        Rigidbody_array = Game_calculator.Convert_from_List_to_Array(rigidbody_list);
    }

    class Bone_transform
    {
        public Vector3 Position = Vector3.zero;

        public Quaternion Rotation = Quaternion.identity;
    }
    #endregion

}
