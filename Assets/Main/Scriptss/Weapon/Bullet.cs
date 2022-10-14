//Пуля
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Tooltip("Урон")]
    [SerializeField]
    int Damage = 10;//Урон от пули

    [Tooltip("Скорость снаряда")]
    [SerializeField]
    float Speed_bullet = 150f;

    [Tooltip("След от снаряда")]
    [SerializeField]
    TrailRenderer Trail = null;

    [Tooltip("Физика")]
    [SerializeField]
    Rigidbody Body = null;

    [Tooltip("Время до самоуничтожения")]
    [SerializeField]
    float Time_destroy = 10f;

    [Tooltip("Дальность проверки перед собой лучом")]
    [SerializeField]
    float Distance_raycast_detected = 0.5f;

    [Tooltip("Слои с которыми будет взаимодействовать")]
    [SerializeField]
    LayerMask Layer_detected = 0;

    bool Active_bool = true;





    [Space(20)]
    [Header("Режим стрелы")]

    [Tooltip("Включить режим стрелы (будет застревать и не уничтожатся при контакте)")]
    [SerializeField]
    bool Arrow_mode_bool = false;

    [Tooltip("Время до самоуничтожения после застревания стрелы")]
    [SerializeField]
    float Time_destroy_arrow = 20f;

    [Tooltip("Выключает таймер самоуничтожения для застрявшей стрелы")]
    [SerializeField]
    bool No_time_destroy = false;


    private void Start()
    {
        Body.AddForce(transform.forward * Speed_bullet);
    }

    private void Update()
    {
        if (Active_bool)
        {
            Raycast_preparation();
        }
    }

    /// <summary>
    /// Пускать рейкаст вперёд, для проверки
    /// </summary>
    void Raycast_preparation()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Distance_raycast_detected, Layer_detected))
        {

            if (hit.transform.GetComponent<I_damage>() != null)
            {
                hit.transform.GetComponent<I_damage>().Damage(Damage);
            }

            if (Arrow_mode_bool)
            {
                Off_rigidbody_arrow();

                transform.position = hit.point;

                //transform.rotation = Quaternion.LookRotation(-hit.normal);//Поворот по нормале сопрокосновения

                transform.SetParent(hit.transform, true);

                
            }
            else
            {
                Destroy(gameObject);
            }


            Active_bool = false;
        }
    }

    /// <summary>
    /// Указать параметры пули
    /// </summary>
    /// <param name="_damage">Указать урон</param>
    /// <param name="_bullet_flight_speed">Указать скорость полёта</param>
    public void Specify_settings(int _damage, float _bullet_flight_speed)
    {
        StartCoroutine(Destroy_coroutine());
        Speed_bullet = _bullet_flight_speed;
        Damage = _damage;
        Body.velocity = Vector3.zero;
        Body.AddForce(transform.forward * _bullet_flight_speed);
    }

    public void Specify_settings(int _damage)
    {
        Specify_settings(_damage, Speed_bullet);
    }

    public void Specify_settings(float _bullet_flight_speed)
    {
        Specify_settings(Damage, _bullet_flight_speed);
    }

    /// <summary>
    /// Остановить стрелу
    /// </summary>
    void Off_rigidbody_arrow()
    {
        Body.isKinematic = true;
        Body.velocity = Vector3.zero;

        if (Trail)
        {
            Trail.time = 0.1f;
            Trail.emitting = false;
        }

            StopAllCoroutines();

        if (!No_time_destroy)
        {
            Time_destroy = Time_destroy_arrow;
            StartCoroutine(Destroy_coroutine());
        }


    }

    /// <summary>
    /// Уничтожить пули спустя время
    /// </summary>
    /// <returns></returns>
    IEnumerator Destroy_coroutine()
    {
        yield return new WaitForSeconds(Time_destroy);
        Destroy(gameObject);
    }
}
