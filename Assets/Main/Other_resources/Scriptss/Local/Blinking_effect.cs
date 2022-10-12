//Эффект мигания материала
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking_effect : MonoBehaviour
{
    [Tooltip("Для смены цвета тела")]
    [SerializeField]
    private Renderer[] Material_body_array = new Renderer[0];

    [Tooltip("Цвет нанесения урона")]
    [SerializeField]
    private Color Color_damage = Color.red;

    [Tooltip("Количество миганий (повторений)")]
    [SerializeField]
    private int Number_repetitions = 2;

    [Tooltip("Скорость мигания(чем меньше, тем быстрее)")]
    [SerializeField]
    private float Speed_effect_damage = 0.01f;

    [Range(0f, 1f)]
    [Tooltip("Плавность")]
    [SerializeField]
    private float Smoothness = 0.25f;

    private Color[] Default_color_body_array = new Color[0];

    private Coroutine Damage_effect_coroutine = null;//Коррутина эффекта получения урона


    private void Start()
    {

        if (Material_body_array.Length > 0)
        {
            Default_color_body_array = new Color[Material_body_array.Length];

            for (int x = 0; x < Material_body_array.Length; x++)
            {
                Default_color_body_array[x] = Material_body_array[x].material.color;
            }
        }

    }


    IEnumerator Damage_effect_material_coroutine()
    {
        float step = 0;

        for (int x = 0; x < Number_repetitions; x++)
        {
            while (step < 1)
            {
                step += Smoothness;
                yield return new WaitForSeconds(Speed_effect_damage);
                for (int c = 0; c < Material_body_array.Length; c++)
                {
                    Material_body_array[c].material.color = Color.Lerp(Default_color_body_array[c], Color_damage, step);
                }
            }

            while (step > 0)
            {
                step -= Smoothness;
                yield return new WaitForSeconds(Speed_effect_damage);
                for (int c = 0; c < Material_body_array.Length; c++)
                {
                    Material_body_array[c].material.color = Color.Lerp(Default_color_body_array[c], Color_damage, step);
                }
            }
        }
    }

    /// <summary>
    /// Активировать
    /// </summary>
    public void Activation()
    {

        if (Damage_effect_coroutine != null)
        {
            StopCoroutine(Damage_effect_coroutine);
        }

        if (Material_body_array.Length > 0)
        {
            Damage_effect_coroutine = StartCoroutine(Damage_effect_material_coroutine());
        }
    }

}
