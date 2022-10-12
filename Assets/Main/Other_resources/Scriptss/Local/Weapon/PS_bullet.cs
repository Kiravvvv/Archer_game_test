//Скрипт для пуль в системе частиц
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_bullet : MonoBehaviour
{

    [Tooltip("След от пули")]
    [SerializeField]
    GameObject Shot_mark = null;

    [Tooltip("Урон от пули")]
    public int Damage = 1;

    List<ParticleCollisionEvent> Collider_event_list = new List<ParticleCollisionEvent>();

    ParticleSystem PS = null;//Эта система частиц

    // Start is called before the first frame update
    void Start()
    {
        PS = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int events = PS.GetCollisionEvents(other, Collider_event_list);

        for (int i = 0; i < events; i++)
        {
            if(Shot_mark)
            Instantiate(Shot_mark, Collider_event_list[i].intersection, Quaternion.LookRotation(Collider_event_list[i].normal));

            if (other.GetComponent<Health>())
                other.GetComponent<Health>().Damage_add(Damage, null);
        }
    }
}
