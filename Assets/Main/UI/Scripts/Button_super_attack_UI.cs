using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_super_attack_UI : MonoBehaviour
{
    
    public void Activation_super_Attack(int _id_super_attack)
    {
        Game_administrator.Instance.Find_out_Player_script.Activation_super_attack(_id_super_attack);
    }

}
