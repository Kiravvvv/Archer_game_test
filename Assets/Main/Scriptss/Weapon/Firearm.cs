//���������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firearm : MonoBehaviour
{

    [Tooltip("����� ������ ����")]
    [SerializeField]
    Transform Fire_point = null;

    [Tooltip("������ ����")]
    [SerializeField]
    Bullet Bullet_prefab = null;

    [Tooltip("������ ����� ����")]
    [SerializeField]
    Bullet Super_Bullet_prefab = null;

    [Tooltip("������� ��������")]
    [SerializeField]
    float Spread = 0;

    float Spread_active = 0;//��� ������ � ���������

    int Damage = 0;

    Quaternion Default_rotation_Fire_point = Quaternion.identity;//����������� �������� ����� ������ ���� ��� ������

    int Attack_mode_id = 0;

    Transform Target = null;//���� �����

    private void Start()
    {
        Spread_active = Spread;
        Default_rotation_Fire_point = Fire_point.localRotation;
    }

    /// <summary>
    /// ����������
    /// </summary>
    public void Fire()
    {
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
            }

            Attack_mode_id = 0;
        }
        
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
        Vector3 position_attack = (Target.position + (Vector3.up * (Random.Range(10, 15)))) + _position_addination;

        Bullet bulet = null;

        bulet = Instantiate(Bullet_prefab, position_attack, Quaternion.Euler(90, 0, 0));

        if (Damage > 0)
            bulet.Specify_settings(Damage);
    }


    /// <summary>
    /// ���������� � ��������� ����� ��� �������
    /// </summary>
    /// <param name="_damage"></param>
    public void Fire(int _damage)
    {
        Damage = _damage;
        Fire();
    }

    /// <summary>
    /// �������� �� �����������
    /// </summary>
    /// <param name="_direction">�����������</param>
    public void Fire(Vector3 _direction)
    {
        Fire_point.transform.LookAt(_direction, Fire_point.up);
        Fire_point.transform.eulerAngles += new Vector3(Random.Range(-Spread_active, Spread_active), Random.Range(-Spread_active, Spread_active), Fire_point.transform.localRotation.z);

        Fire();
    }

    /// <summary>
    /// �������� �� ����������� � ��������� �����
    /// </summary>
    /// <param name="_direction">�����������</param>
    /// <param name="_damage">����</param>
    public void Fire(Vector3 _direction, int _damage)
    {
        Damage = _damage;
        Fire(_direction);
    }

    /// <summary>
    /// ������� � ����� ���������
    /// </summary>
    /// <param name="_force_spead">���� �������� (�� 0 �� 1)</param>
    public void Fire(float _force_spead)
    {
        Spread_active = Spread * _force_spead;
        Fire_point.transform.localRotation = Default_rotation_Fire_point;
        Fire_point.transform.eulerAngles += new Vector3(Random.Range(-Spread_active, Spread_active), Random.Range(-Spread_active, Spread_active), Fire_point.transform.localRotation.z);
        Fire();
    }

    /// <summary>
    /// ������� � ����� ��������� � ��������� �����
    /// </summary>
    /// <param name="_force_spead">���� �������� (�� 0 �� 1)</param>
    /// <param name="_damage">����</param>
    public void Fire(float _force_spead, int _damage)
    {
        Damage = _damage;
        Fire(_force_spead);
    }

    /// <summary>
    /// ������� �� ����������� � � ����� ��������
    /// </summary>
    /// <param name="_direction">�����������</param>
    /// <param name="_force_spead">���� �������� (�� 0 �� 1)</param>
    public void Fire(Vector3 _direction, float _force_spead)
    {
        Spread_active = Spread * _force_spead;
        Fire(_direction);
    }

    /// <summary>
    /// ������� �� �����������, � ����� �������� � ���
    /// </summary>
    /// <param name="_direction">�����������</param>
    /// <param name="_force_spead">���� �������� (�� 0 �� 1)</param>
    /// <param name="_damage">����</param>
    public void Fire(Vector3 _direction, float _force_spead, int _damage)
    {
        Damage = _damage;
        Fire(_direction, _force_spead);
    }


    /// <summary>
    /// ������� ����� �����
    /// </summary>
    /// <param name="_id_mode">����� �����</param>
    public void Mode_attack(int _id_mode)
    {
        Attack_mode_id = _id_mode;
    }

    /// <summary>
    /// ����� ����
    /// </summary>
    /// <param name="_target">����</param>
    public void New_target(Transform _target)
    {
        Target = _target;
    }

}
