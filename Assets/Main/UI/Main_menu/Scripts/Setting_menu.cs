//Скрипт настроек и задаёт положение настроек согласно сохранению
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_menu : Singleton<Setting_menu>
{
    [Header("Настройки эффектов")]
    [Tooltip("Ползунок громкости эффектов")]
    [SerializeField]
    private Slider Effect_sound_slider = null;

    public delegate void Effect_sound_delegate(float _value);//Делегат изменения громкости эффектов
    public Effect_sound_delegate Effect_sound_d;//Экземпляр делегата изменения громкости эффектов

    [Header("Настройки музыки")]
    [Tooltip("Ползунок громкости музыки")]
    [SerializeField]
    private Slider Music_slider = null;

    public delegate void Music_delegate(float _value);//Делегат изменения громкости музыки
    public Music_delegate Music_d;//Экземпляр делегата изменения громкости музыки

    [Header("Настройки оповещения")]
    [Tooltip("Галочка оповещения")]
    [SerializeField]
    private Toggle Alert_toggle = null;

    [Header("Настройки оповещения")]
    [Tooltip("Галочка вибрации")]
    [SerializeField]
    private Toggle Vibration_toggle = null;

    [Header("Выбранный язык")]
    [Tooltip("Список выбора языка")]
    [SerializeField]
    private Dropdown Language_option = null;


    public delegate void Language_delegate(int _index);//Делегат изменения языка
    public Language_delegate Language_d;//Экземпляр делегата изменения языка

    public delegate void Input_key_delegate();//Делегат изменения клавиш управления
    public Input_key_delegate Input_key_d;//Экземпляр делегата изменения клавиш управления

    public delegate void Reset_delegate();//Делегат сбрасывающий насйтроки
    public Reset_delegate Reset_d;//Экземпляр делегата сбрасывающий насйтроки

    private void Start()
    {
        Preparation();
    }



    /// <summary>
    /// Подготовка при включение
    /// </summary>
    void Preparation()
    {
            Effect_sound_slider.value = Save_PlayerPrefs.Know_parameter(Type_parameter_value_float.Sound_effect);

            Music_slider.value = Save_PlayerPrefs.Know_parameter(Type_parameter_value_float.Sound_music);

            Language_option.value = Save_PlayerPrefs.Know_parameter(Type_parameter_value_int.Language);

            Alert_toggle.isOn = Save_PlayerPrefs.Know_parameter(Type_parameter_bool.Alert_bool);

            Vibration_toggle.isOn = Save_PlayerPrefs.Know_parameter(Type_parameter_bool.Vibration_bool);


    }



    /// <summary>
    /// Вернуть настройки в изначальное состояние
    /// </summary>
    public void Reset_setting()
    {
        Sound_effect_control(1);
        Sound_music_control(1);
        Alert_control(true);
        Vibration_control(true);

        Preparation();

        Reset_d?.Invoke();
    }


    /// <summary>
    /// Изменение клавиш управления
    /// </summary>
    public void Input_key_control()
    {
        Input_key_d?.Invoke();
    }



    /// <summary>
    /// Изменение языка
    /// </summary>
    /// <param name="_index">Индекс</param>
    public void Language_control(int _index)
    {
        Save_PlayerPrefs.Save_parameter(Type_parameter_value_int.Language, _index);

        Language_d?.Invoke(_index);
    }



    /// <summary>
    /// Изменение звука эффектов
    /// </summary>
    /// <param name="_value">Сила звука</param>
    public void Sound_effect_control(float _value)
    {
        Save_PlayerPrefs.Save_parameter(Type_parameter_value_float.Sound_effect, _value);

        Effect_sound_d?.Invoke(_value);
    }



    /// <summary>
    /// Изменение звука музыки
    /// </summary>
    /// <param name="_value">Сила звука</param>
    public void Sound_music_control(float _value)
    {
        Save_PlayerPrefs.Save_parameter(Type_parameter_value_float.Sound_music, _value);

        if(Music_d != null)
        Music_d?.Invoke(_value);
    }


    /// <summary>
    /// Изменение оповещения
    /// </summary>
    /// <param name="_bool">Включить или отключить</param>
    public void Alert_control(bool _bool)
    {
        Save_PlayerPrefs.Save_parameter(Type_parameter_bool.Alert_bool, _bool);
    }


    /// <summary>
    /// Изменение вибрации
    /// </summary>
    /// <param name="_bool">Включить или отключить</param>
    public void Vibration_control(bool _bool)
    {
        Save_PlayerPrefs.Save_parameter(Type_parameter_bool.Vibration_bool, _bool);
    }
}
