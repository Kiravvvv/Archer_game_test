//Скрипт Фиксированного прыжка
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
public class Character_fix_jump : Game_character_abstract
{

    [Tooltip("Высота прыжка")]
    [SerializeField]
    float Jump_height = 4f;

    [Tooltip("Продолжительность прыжка")]
    [SerializeField]
    float Jump_duration = 0.5f;

    [Tooltip("Кривая прыжка")]
    [SerializeField]
    AnimationCurve Jump_curve = new AnimationCurve();



    IEnumerator Jump_coroutine()
    {
        float expired_seconds = 0f;
        float progress = 0;
        Vector3 start_position = transform.position;

        while(progress < 1)
        {
            expired_seconds += Time.deltaTime;
            progress = expired_seconds / Jump_duration;

            //transform.position = Vector3.Lerp(start_position, new Vector3(0, Jump_curve.Evaluate(progress) * Jump_height, 0 ), progress);
            transform.position = new Vector3(start_position.x, start_position.y + Jump_curve.Evaluate(progress) * Jump_height, start_position.z);

            yield return null;
        }

        

    }


}
