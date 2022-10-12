//Триггер нанесения урона
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{

    [Tooltip("Здоровье владельца (что бы не нанести урон самому себе)")]
    [SerializeField]
    Health My_health = null;

    [Tooltip("Нанесения урона")]
    [SerializeField]
    int Damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        Health h = null;

        if (other.GetComponent<Health>())
            h = other.GetComponent<Health>();

        if (h != null && h != My_health)
        {
            h.Damage_add(Damage, null);
        }
    }
}
