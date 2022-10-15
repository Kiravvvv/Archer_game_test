using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_HC_UI : Singleton<Game_HC_UI>
{
    [Tooltip("��������� ������")]
    [SerializeField]
    GameObject Start_panel = null;

    [Tooltip("������� ������ (�� ����� �������� ����)")]
    [SerializeField]
    GameObject Game_panel = null;

    [Tooltip("������ ����� ���� (������)")]
    [SerializeField]
    GameObject End_panel_win = null;

    [Tooltip("������ ����� ���� (���������)")]
    [SerializeField]
    GameObject End_panel_lose = null;



    [Space(20)]
    [Header("������")]

    [Tooltip("���������� ������")]
    [SerializeField]
    Image Health_image = null;

    [Tooltip("������ ������")]
    [SerializeField]
    Image Aim_image = null;

    Vector2 Save_size_Aim = Vector2.one;

    private void Start()
    {
        Save_size_Aim = Aim_image.rectTransform.sizeDelta;
        Game_administrator.Player_Health_info_event.AddListener(Change_health);
        All_off_panels();
        Start_panel.SetActive(true);
    }

    /// <summary>
    /// ��������� ��� ������
    /// </summary>
    void All_off_panels()
    {
        Start_panel.SetActive(false);
        Game_panel.SetActive(false);
        End_panel_win.SetActive(false);
        End_panel_lose.SetActive(false);
    }


    public void Start_game()
    {
        Game_administrator.Instance.Start_game();
        All_off_panels();
        Game_panel.SetActive(true);
    }


    /// <summary>
    /// �������� ���������� ������
    /// </summary>
    /// <param name="_value">�������� �� 0 �� 1</param>
    public void Change_health(float _value)
    {
        Health_image.fillAmount = _value;
    }


    /// <summary>
    /// �������� ������ �������
    /// </summary>
    /// <param name="_value">�������� �������</param>
    public void Change_size_aim(float _value)
    {
        Aim_image.rectTransform.sizeDelta = Save_size_Aim * _value;
    }


    public Vector3 Aim_spread_random_point
    {
        get
        {

            Vector3 point = Aim_image.rectTransform.position;//new Vector3(Random.Range(-Aim_image.rectTransform.sizeDelta.x, Aim_image.rectTransform.sizeDelta.x), Random.Range(-Aim_image.rectTransform.sizeDelta.y, Aim_image.rectTransform.sizeDelta.y), Aim_image.rectTransform.position.z);

            point.x += Random.Range(-Aim_image.rectTransform.sizeDelta.x/2, Aim_image.rectTransform.sizeDelta.x/2);
            point.y += Random.Range(-Aim_image.rectTransform.sizeDelta.y/2, Aim_image.rectTransform.sizeDelta.y/2);

            /*
            Vector3 finale_point = Vector3.zero;

            Ray ray = Cam.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                finale_point = hit.point;
                print(hit.transform.name);
            }
            else if (Physics.Raycast(ray, out hit, 100))
            {
                finale_point = hit.point;
            }
            */

            return point;
        }

    }


}
