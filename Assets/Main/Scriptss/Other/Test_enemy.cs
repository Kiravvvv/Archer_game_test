using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_enemy : MonoBehaviour
{

    private void OnEnable()
    {
        Game_administrator.Instance.Add_enemy_list(transform);
    }

    private void OnDisable()
    {
        if(Game_administrator.Instance != null)
        Game_administrator.Instance.Remove_enemy_list(transform);
    }


}
