//?????????
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firearm : MonoBehaviour
{

    [Tooltip("????? ?????? ????")]
    [SerializeField]
    Transform Fire_point = null;

    [Tooltip("?????? ????")]
    [SerializeField]
    Bullet Bullet_prefab = null;

    [Tooltip("?????? ????? ????")]
    [SerializeField]
    Bullet Super_Bullet_prefab = null;

    [Tooltip("??????? ? ??????????? ?? ??????? UI ???????? ??????? (???????? ???? ???????? ?? ?????)")]
    [SerializeField]
    bool Aim_image_spread_bool = true;

    [Tooltip("??????? ???????? (???????????????? ????????? ??????????? ???????? ????? ????????? ???????? Fire_point)")]
    [SerializeField]
    float Spread = 0;

    float Spread_active = 0;//??? ?????? ? ?????????

    int Damage = 0;

    Quaternion Default_rotation_Fire_point = Quaternion.identity;//??????????? ???????? ????? ?????? ???? ??? ??????

    int Attack_mode_id = 0;

    Transform Target = null;//???? ?????

    Camera Cam = null;

    Vector3 Finale_point = Vector3.zero;//????? ???? ???????? ???????

    private void Start()
    {
        Cam = Game_administrator.Instance.Find_out_Player_script.Find_out_Camera;
        Spread_active = Spread;
        Default_rotation_Fire_point = Fire_point.localRotation;
    }

    void Fire_normal()
    {

        if (Damage > 0)
            Instantiate(Bullet_prefab, Fire_point.position, Fire_point.rotation).Specify_settings(Damage);
        else
            Instantiate(Bullet_prefab, Fire_point.position, Fire_point.rotation);
    }

    void Fire_super_1()
    {
        StartCoroutine(Coroutine_super_attack());
    }

    IEnumerator Coroutine_super_attack()
    {

        for (int x = 0; x < 15; x++)
        {
            Fire_normal();

            yield return new WaitForSeconds(0.01f);
        }

    }

    void Fire_super_2()
    {
        Vector3 save_rotation = Fire_point.eulerAngles;

        for (int x = 0; x < 6; x++)
        {
            Fire_point.rotation = Quaternion.Euler(save_rotation + new Vector3(0,2 * x,0));
            Fire_normal();
        }

        for (int x = 1; x < 6; x++)
        {
            Fire_point.rotation = Quaternion.Euler(save_rotation + new Vector3(0, -1 * x, 0));
            Fire_normal();
        }
    }

    void Fire_super_3()
    {
        if (Damage > 0)
            Instantiate(Super_Bullet_prefab, Fire_point.position, Fire_point.rotation).Specify_settings(Damage);
        else
            Instantiate(Super_Bullet_prefab, Fire_point.position, Fire_point.rotation);
    }

    void Fire_super_4()
    {
        float density = 0.3f;

        Spawn_arrow_super_attack_4(new Vector3(0, 0, 0));

        for (int x = 1; x < 5; x++)
        {
            for(int z = 1; z < 5; z++)
            {
                Spawn_arrow_super_attack_4(new Vector3(density * x, 0, 0));
                Spawn_arrow_super_attack_4(new Vector3(-density * x, 0, 0));
                Spawn_arrow_super_attack_4(new Vector3(0, 0, density * z));
                Spawn_arrow_super_attack_4(new Vector3(0, 0, -density * z));
            }
        }

        for (int x = 1; x < 5; x++)
        {
            for (int z = 1; z < 5; z++)
            {
                Spawn_arrow_super_attack_4(new Vector3(density * x, 0, density * z));
                Spawn_arrow_super_attack_4(new Vector3(-density * x, 0, density * z));
                Spawn_arrow_super_attack_4(new Vector3(density * x, 0, -density * z));
                Spawn_arrow_super_attack_4(new Vector3(-density * x, 0, -density * z));
            }
        }

    }

    void Spawn_arrow_super_attack_4(Vector3 _position_addination)
    {
        Vector3 position_attack = (Finale_point + (Vector3.up * (Random.Range(10, 15)))) + _position_addination;

        Bullet bulet = null;

        bulet = Instantiate(Bullet_prefab, position_attack, Quaternion.Euler(90, 0, 0));

        if (Damage > 0)
            bulet.Specify_settings(Damage);
    }


    /// <summary>
    /// ??????????
    /// </summary>
    public void Fire()
    {
        if (Aim_image_spread_bool)
        {

            Vector3 point_screen_point = Game_HC_UI.Instance.Aim_spread_random_point;

            Ray ray = Cam.ScreenPointToRay(point_screen_point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Finale_point = hit.point;
                print(hit.transform.name);
            }
            else
            {
                Finale_point = ray.direction * 4000f;
            }
            Fire_point.transform.LookAt(Finale_point, Fire_point.up);
        }


        if (gameObject.activeSelf)
        {
            switch (Attack_mode_id)
            {
                case 0:
                    Fire_normal();
                    break;

                case 1:
                    Fire_super_1();
                    break;

                case 2:
                    Fire_super_2();
                    break;

                case 3:
                    Fire_super_3();
                    break;

                case 4:
                    Fire_super_4();
                    break;
                default:
                    Fire_normal();
                    break;
            }

            Attack_mode_id = 0;
        }

    }

    /// <summary>
    /// ?????????? ? ????????? ????? ??? ???????
    /// </summary>
    /// <param name="_damage"></param>
    public void Fire(int _damage)
    {
        Damage = _damage;
        Fire();
    }

    /// <summary>
    /// ???????? ?? ???????????
    /// </summary>
    /// <param name="_point_end">???????? ????? (???? ???????)</param>
    public void Fire(Vector3 _point_end)
    {
        Fire_point.transform.LookAt(_point_end, Fire_point.up);
        Fire_point.transform.eulerAngles += new Vector3(Random.Range(-Spread_active, Spread_active), Random.Range(-Spread_active, Spread_active), Fire_point.transform.localRotation.z);

        Fire();
    }

    /// <summary>
    /// ???????? ?? ??????????? ? ????????? ?????
    /// </summary>
    /// <param name="_point_end">???????? ????? (???? ???????)</param>
    /// <param name="_damage">????</param>
    public void Fire(Vector3 _point_end, int _damage)
    {
        Damage = _damage;
        Fire(_point_end);
    }

    /// <summary>
    /// ??????? ? ????? ?????????
    /// </summary>
    /// <param name="_force_spead">???? ???????? (?? 0 ?? 1)</param>
    public void Fire(float _force_spead)
    {
        Spread_active = Spread * _force_spead;
        Fire_point.transform.localRotation = Default_rotation_Fire_point;
        Fire_point.transform.eulerAngles += new Vector3(Random.Range(-Spread_active, Spread_active), Random.Range(-Spread_active, Spread_active), Fire_point.transform.localRotation.z);
        Fire();
    }

    /// <summary>
    /// ??????? ? ????? ????????? ? ????????? ?????
    /// </summary>
    /// <param name="_force_spead">???? ???????? (?? 0 ?? 1)</param>
    /// <param name="_damage">????</param>
    public void Fire(float _force_spead, int _damage)
    {
        Damage = _damage;
        Fire(_force_spead);
    }

    /// <summary>
    /// ??????? ?? ??????????? ? ? ????? ????????
    /// </summary>
    /// <param name="_point_end">???????? ????? (???? ???????)</param>
    /// <param name="_force_spead">???? ???????? (?? 0 ?? 1)</param>
    public void Fire(Vector3 _point_end, float _force_spead)
    {
        Spread_active = Spread * _force_spead;
        Fire(_point_end);
    }

    /// <summary>
    /// ??????? ?? ???????????, ? ????? ???????? ? ???
    /// </summary>
    /// <param name="_point_end">???????? ????? (???? ???????)</param>
    /// <param name="_force_spead">???? ???????? (?? 0 ?? 1)</param>
    /// <param name="_damage">????</param>
    public void Fire(Vector3 _point_end, float _force_spead, int _damage)
    {
        Damage = _damage;
        Fire(_point_end, _force_spead);
    }


    /// <summary>
    /// ??????? ????? ?????
    /// </summary>
    /// <param name="_id_mode">????? ?????</param>
    public void Mode_attack(int _id_mode)
    {
        Attack_mode_id = _id_mode;
    }

    /// <summary>
    /// ????? ????
    /// </summary>
    /// <param name="_target">????</param>
    public void New_target(Transform _target)
    {
        Target = _target;
    }

}
