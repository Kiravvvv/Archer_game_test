//Пуля
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("Физика")]
    [SerializeField]
    Rigidbody Body = null;

    [Tooltip("Коллайдер")]
    [SerializeField]
    Collider Collider_main = null;

    [Tooltip("Время до самоуничтожения")]
    [SerializeField]
    float Time_destroy = 10f;


    [Tooltip("Урон")]
    [SerializeField]
    int Damage = 10;//Урон от пули

    [Tooltip("Скорость пули")]
    [SerializeField]
    float Speed_bullet = 150f;

    [Tooltip("След от снаряда")]
    [SerializeField]
    TrailRenderer Trail = null;

    [Tooltip("Самоуничтожение при столкновение")]
    [SerializeField]
    bool Destroy_detected_bool = false;

    Vector3 Start_scale = Vector3.one;

    private void Start()
    {
        Body.AddForce(transform.forward * Speed_bullet);
        Start_scale = transform.localScale;
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


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<I_damage>() != null)
        {
            other.gameObject.GetComponent<I_damage>().Damage(Damage, null);
        }

        transform.SetParent(other.transform);

        Off_rigidbody();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<I_damage>() != null)
        {
            other.gameObject.GetComponent<I_damage>().Damage(Damage, null);
        }

        transform.SetParent(other.transform);

        Off_rigidbody();
    }

    void Off_rigidbody()
    {
        Body.isKinematic = true;
        Body.velocity = Vector3.zero;

        Collider_main.enabled = false;

        if (Trail)
        {
            Trail.time = 0.1f;
            Trail.emitting = false;
        }

        if (Destroy_detected_bool)
            Destroy(gameObject);
        else
        {
            StopAllCoroutines();
            Time_destroy = Time_destroy * 2;
            StartCoroutine(Destroy_coroutine());
        }

    }

    IEnumerator Destroy_coroutine()//Уничтожить пули спустя время
    {
        yield return new WaitForSeconds(Time_destroy);
        Destroy(gameObject);
    }
}
