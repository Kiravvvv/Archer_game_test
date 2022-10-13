//Пуля
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("Физика")]
    [SerializeField]
    Rigidbody Body = null;

    [Tooltip("Время до самоуничтожения")]
    [SerializeField]
    float Time_destroy = 10f;

    [Tooltip("След от пули")]
    [SerializeField]
    GameObject Shot_mark = null;

    [Tooltip("Урон")]
    [SerializeField]
    int Damage = 10;//Урон от пули

    [Tooltip("Скорость пули")]
    [SerializeField]
    float Speed_bullet = 150f;

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
        Damage = _damage;
        Body.velocity = Vector3.zero;
        Body.AddForce(transform.forward * _bullet_flight_speed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Health>())
        {
            other.gameObject.GetComponent<Health>().Damage_add(Damage, null);
        }
        else
        {
            if (Shot_mark)
            {
                ContactPoint contact = other.contacts[0];
                Quaternion hit_rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);

                GameObject obj = Instantiate(Shot_mark, contact.point + (contact.normal * 0.005f), hit_rotation);

                float r = UnityEngine.Random.Range(0, 360);
                obj.transform.Rotate(0, 0, r);
            }

        }
       Destroy(gameObject);
    }

    IEnumerator Destroy_coroutine()//Уничтожить пули спустя время
    {
        yield return new WaitForSeconds(Time_destroy);
        Destroy(gameObject);
    }
}
