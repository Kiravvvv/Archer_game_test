//Огнестрел
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firearm : MonoBehaviour
{

    [Tooltip("Точка спавна пули")]
    [SerializeField]
    Transform Fire_point = null;

    [Tooltip("Префаб пули")]
    [SerializeField]
    GameObject Bullet_prefab = null;

    /// <summary>
    /// Стрельнуть
    /// </summary>
    public void Fire()
    {
        if(gameObject.activeSelf)
        Instantiate(Bullet_prefab, Fire_point.position, Fire_point.rotation);
    }

    public void Fire(Vector3 _target)
    {
        if (gameObject.activeSelf)
        {

            Fire_point.transform.LookAt(_target, Fire_point.up);


            Instantiate(Bullet_prefab, Fire_point.position, Fire_point.rotation);
        }
           
    }

}
