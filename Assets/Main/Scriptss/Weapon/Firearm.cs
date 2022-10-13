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

    [Tooltip("Разброс выстрела")]
    [SerializeField]
    float Spread = 0;

    float Spread_active = 0;//Для работы с разбросом

    Quaternion Default_rotation_Fire_point = Quaternion.identity;//Направление поворота точки спавна пули при старте

    private void Start()
    {
        Spread_active = Spread;
        Default_rotation_Fire_point = Fire_point.localRotation;
    }

    /// <summary>
    /// Стрельнуть
    /// </summary>
    public void Fire()
    {
        if (gameObject.activeSelf)
        {
            Instantiate(Bullet_prefab, Fire_point.position, Fire_point.rotation);
        }
        
    }

    /// <summary>
    /// Стрельба по направлению
    /// </summary>
    /// <param name="_direction"></param>
    public void Fire(Vector3 _direction)
    {
        Fire_point.transform.LookAt(_direction, Fire_point.up);
        Fire_point.transform.eulerAngles += new Vector3(Random.Range(-Spread_active, Spread_active), Random.Range(-Spread_active, Spread_active), Fire_point.transform.localRotation.z);

        Fire();
    }

    /// <summary>
    /// Выстрел с силой разбросом
    /// </summary>
    /// <param name="_force_spead">Сила разброса (от 0 до 1)</param>
    public void Fire(float _force_spead)
    {
        Spread_active = Spread * _force_spead;
        Fire_point.transform.localRotation = Default_rotation_Fire_point;
        Fire_point.transform.eulerAngles += new Vector3(Random.Range(-Spread_active, Spread_active), Random.Range(-Spread_active, Spread_active), Fire_point.transform.localRotation.z);
        Fire();
    }

    /// <summary>
    /// Выстрел по направлению с силой разбросом
    /// </summary>
    /// <param name="_direction">Направление</param>
    /// <param name="_force_spead">Сила разброса (от 0 до 1)</param>
    public void Fire(Vector3 _direction, float _force_spead)
    {
        Spread_active = Spread * _force_spead;
        Fire(_direction);
    }

}
