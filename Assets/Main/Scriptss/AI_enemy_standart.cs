using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_enemy_standart : Game_character_abstract
{
    [Space(20)]
    [Header("Настройки ИИ")]
    [Tooltip("ИИ агент")]
    [SerializeField]
    NavMeshAgent NavMeshAgent_ = null;

    [Tooltip("Дистанция атаки")]
    [SerializeField]
    float Distance_attack = 1f;

    Transform Target = null;//Цель

    Coroutine Coroutine_target = null;

    protected override void Start()
    {
        base.Start();
        NavMeshAgent_.speed = Speed_movement;
        Health_script.Killer_attack_delegate += New_target_killer;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && !Game_calculator.Checking_obstacles_between_two_objects(Head, other.transform, 8))
        {
            New_target(other.transform);
        }
    }


    /// <summary>
    /// Назначить целью атакующего персонажа
    /// </summary>
    /// <param name="_killer">Сам атакующий</param>
    void New_target_killer( Game_character_abstract _killer)
    {
        if(_killer != null)
        New_target(_killer.transform);
    }


    /// <summary>
    /// Назначить новую цель
    /// </summary>
    /// <param name="_target">Цель</param>
    void New_target(Transform _target)
    {
        if (_target.tag == "Player" && Health_script.Find_out_Alive)
        {
            Target = _target;

            if (Coroutine_target != null)
                StopCoroutine(Update_target_coroutine());

            Coroutine_target = StartCoroutine(Update_target_coroutine());
        }
    }


    IEnumerator Update_target_coroutine()//Обновление реакции ИИ
    {

        while (Target)
        {
            if (Vector3.Distance(Target.position, My_transform.position) > Distance_attack)
            {
                Move();
            }
            //else if (Check_visual() && Check_look_rotation())
            else if (Check_look_rotation())
            {
                Stop_move_and_attack();
            }
            else
            {
                Move();
            }

            yield return new WaitForSeconds(0.4f);
        }

    }

    /// <summary>
    /// Пусть движется к цели
    /// </summary>
    void Move()
    {
        NavMeshAgent_.isStopped = false;
        NavMeshAgent_.SetDestination(Target.position);
        Anim.SetBool("Attack", false);
        Anim.SetBool("Move", true);
    }


    /// <summary>
    /// Остановить движение
    /// </summary>
    void Stop_move_and_attack()
    {
        NavMeshAgent_.isStopped = true;
        Additional_rotation_look();
        Additional_move_back();
        Anim.SetBool("Attack", true);
        Anim.SetBool("Move", false);
    }


    /// <summary>
    /// Проверить визуально на наличие препятсвий от целей
    /// </summary>
    /// <returns></returns>
    bool Check_visual()
    {
        bool result = false;

        Ray ray = new Ray(Head.position, My_transform.forward);

        RaycastHit hit;

        if (Physics.Linecast(Head.position, Target.position, out hit))
        {
            if (hit.transform.tag != "Player")
            {
                result = false;
            }
            else
            {
                result = true;
            }
        }

        return result;
    }


    /// <summary>
    /// Проверить, повёрнут ли в сторону цели
    /// </summary>
    /// <returns></returns>
    bool Check_look_rotation()
    {
        bool result = false;

        Vector3 direction = Target.position - My_transform.position;
        Quaternion qua = Quaternion.LookRotation(direction);

        if (Quaternion.Angle(My_transform.rotation, qua) <= 10f)
        {
            result = true;
        }

        return result;
    }



    /// <summary>
    /// Дополнительный доворот в сторону цели
    /// </summary>
    void Additional_rotation_look()
    {
            var direction = (Target.position - transform.position).normalized;
            direction.y = 0f;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), 0.8f);
    }



    /// <summary>
    /// Дополнительное движение назад, что бы не стоять вплотную к цели
    /// </summary>
    void Additional_move_back()
    {
        if ((Vector3.Distance(Target.position, My_transform.position)) < Distance_attack * 0.6f)
        {
            transform.position -= transform.forward * (Speed_movement * Time.deltaTime);
        }
    }

    /// <summary>
    /// Погиб
    /// </summary>
    protected override void Dead()
    {
        base.Dead();
        NavMeshAgent_.isStopped = true;
        NavMeshAgent_.enabled = false;
    }

}
