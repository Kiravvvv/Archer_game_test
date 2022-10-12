//Финиш
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_trigger : MonoBehaviour
{
    [Tooltip("Системы частиц салюта")]
    [SerializeField]
    ParticleSystem[] PS_array = new ParticleSystem[0];

    List<GameObject> Obj_finish_list = new List<GameObject>();//Лист объектов пересёкших финиш

    private void OnTriggerEnter(Collider other)
    {

            if (other.tag == "Player" && Find_out_finish(other.gameObject))
            {
                Game_administrator.Instance.End_game(true);
                for(int x = 0; x < PS_array.Length; x++)
                {
                    PS_array[x].Play();
                }
            }

    }

    /// <summary>
    /// Узнать пересекал ли объект финиш
    /// </summary>
    /// <param name="_obj">Объект который будем проверять</param>
    /// <returns></returns>
    bool Find_out_finish(GameObject _obj)
    {
        bool result = true;

        for (int x = 0; x < Obj_finish_list.Count; x++)
        {
            if(Obj_finish_list[x] == _obj)
            {
                result = false;
            }
        }

        return result;
    }
}
