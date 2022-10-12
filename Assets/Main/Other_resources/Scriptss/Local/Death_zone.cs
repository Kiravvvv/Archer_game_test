//Зона в которой всё уничтожается или наносится много урона
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_zone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().Death();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
