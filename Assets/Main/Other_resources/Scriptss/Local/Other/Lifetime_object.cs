//Через время объект с этим скриптом самоуничтожится
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime_object : MonoBehaviour
{

    [Tooltip("Время до самоуничтожения")]
    [SerializeField]
    float Time_destroy = 10f;

    [Tooltip("Уничтожает объект, а не деспавнит")]
    [SerializeField]
    bool Destroy_bool = true;

    private void OnEnable()
    {
        StartCoroutine(Destroy_coroutine());
    }


    IEnumerator Destroy_coroutine()
    {
        yield return new WaitForSeconds(Time_destroy);

        if (Destroy_bool)
            Destroy(gameObject);
        else
        Destroy(gameObject);
    }
}
