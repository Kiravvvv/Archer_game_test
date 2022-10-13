using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    [Tooltip("Префаб противника")]
    [SerializeField]
    AI_enemy_standart Prefab_spawn = null;

    [Tooltip("Точки спавна")]
    [SerializeField]
    Transform[] Points_array = new Transform[0];

    [Tooltip("Время спавна")]
    [SerializeField]
    float Time_spawn = 1f;

    private void Start()
    {
        Game_administrator.Start_game_event.AddListener(Start_game);
    }

    void Start_game()
    {
        StartCoroutine(Coroutine_Time_spawn());
    }

    void Spawn_enemy()
    {
        int id_point = Random.Range(0, Points_array.Length);

        Instantiate(Prefab_spawn, Points_array[id_point].position, Quaternion.identity);
    }

    IEnumerator Coroutine_Time_spawn()
    {
        yield return new WaitForSeconds(Time_spawn);
        Spawn_enemy();
        StartCoroutine(Coroutine_Time_spawn());
    }

}
