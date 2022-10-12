//Сохранение настроек в PlayerPrefs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Тип параметров с цифровым значением float
/// </summary>
public enum Type_parameter_value_float
{
    Sound_music,
    Sound_effect
}

/// <summary>
/// Тип параметров с цифровым значением int
/// </summary>
public enum Type_parameter_value_int
{
    Language
}

/// <summary>
/// Тип параметров с bool значением
/// </summary>
public enum Type_parameter_bool
{
    Alert_bool,
    Vibration_bool
}

public static class Save_PlayerPrefs
{

   static bool Initialization_saving_bool;//Параметр который определяет, было ли первоначальное сохранение (что бы выставить все параметры "по умолчанию" правильно)


    /// <summary>
    /// Инициализировать и выставить нужные параметры "по умолчанию" на нужное значение
    /// </summary>
    static void Initialization_saving()
    {
        string name = nameof(Initialization_saving_bool);
        PlayerPrefs.SetString(name, "Da");

        //Выставляем нужные параметры
        Save_parameter(Type_parameter_bool.Alert_bool, true);
        Save_parameter(Type_parameter_bool.Vibration_bool, true);
        Save_parameter(Type_parameter_value_float.Sound_music, 1);
        Save_parameter(Type_parameter_value_float.Sound_effect, 1);
        //


        PlayerPrefs.Save();
    }

    /// <summary>
    /// Сохранить параметр с цифровым значением float
    /// </summary>
    /// <param name="_parameter">Тип параметра</param>
    /// <param name="_value">Значение</param>
    public static void Save_parameter(Type_parameter_value_float _parameter, float _value)
    {

        string name = _parameter.ToString();

        PlayerPrefs.SetFloat(name, _value);

        PlayerPrefs.Save();
    }


    /// <summary>
    /// Сохранить параметр уровня с float значением
    /// </summary>
    /// <param name="_parameter">Параметр</param>
    /// <param name="_lvl_index">Индекс уровня</param>
    /// <param name="_value">Значение</param>
    public static void Save_parameter(Type_parameter_value_float _parameter, int _lvl_index, float _value)
    {
        string name = _parameter.ToString() + "_Lvl_" + _lvl_index.ToString();

        PlayerPrefs.SetFloat(name, _value);

        PlayerPrefs.Save();
    }


    /// <summary>
    /// Сохранить параметр с цифровым значением int
    /// </summary>
    /// <param name="_parameter">Тип параметра</param>
    /// <param name="_value">Значение</param>
    public static void Save_parameter(Type_parameter_value_int _parameter, int _value)
    {
        string name = _parameter.ToString();

        PlayerPrefs.SetInt(name, _value);

        PlayerPrefs.Save();
    }


    /// <summary>
    /// Сохранить параметр уровня с int значением
    /// </summary>
    /// <param name="_parameter">Параметр</param>
    /// <param name="_lvl_index">Индекс уровня</param>
    /// <param name="_value">Значение</param>
    public static void Save_parameter(Type_parameter_value_int _parameter, int _lvl_index, int _value)
    {
        string name = _parameter.ToString() + "_Lvl_" + _lvl_index.ToString();

        PlayerPrefs.SetInt(name, _value);

        PlayerPrefs.Save();
    }


    /// <summary>
    /// Сохранить параметр c bool значением
    /// </summary>
    /// <param name="_parameter">Тип параметра</param>
    /// <param name="_bool">Значение да или нет</param>
    public static void Save_parameter(Type_parameter_bool _parameter, bool _bool)
    {
        int value = 0;

        string name = _parameter.ToString();

        value = _bool ? 1 : 0;

        PlayerPrefs.SetInt(name, value);

        PlayerPrefs.Save();
    }


    /// <summary>
    /// Сохранить параметр уровня с bool значением
    /// </summary>
    /// <param name="_parameter">Параметр</param>
    /// <param name="_lvl_index">Индекс уровня</param>
    /// <param name="_value">Значение да или нет</param>
    public static void Save_parameter(Type_parameter_bool _parameter, int _lvl_index, bool _bool)
    {
        int value = 0;

        string name = _parameter.ToString() + "_Lvl_" + _lvl_index.ToString();

        value = _bool ? 1 : 0;

        PlayerPrefs.SetInt(name, value);

        PlayerPrefs.Save();
    }











    /// <summary>
    /// Узнать значение параметра float
    /// </summary>
    /// <param name="_parameter">Тип параметра</param>
    /// <returns></returns>
    public static float Know_parameter(Type_parameter_value_float _parameter)
    {
        if (!PlayerPrefs.HasKey(nameof(Initialization_saving_bool)))
            Initialization_saving();

        string name = _parameter.ToString();

        return PlayerPrefs.GetFloat(name);
    }


    /// <summary>
    /// Узнать параметр уровня с float значением
    /// </summary>
    /// <param name="_parameter">Параметр</param>
    /// <param name="_lvl_index">Индекс уровня</param>
    /// <returns></returns>
    public static float Know_parameter(Type_parameter_value_float _parameter, int _lvl_index)
    {
        if (!PlayerPrefs.HasKey(nameof(Initialization_saving_bool)))
            Initialization_saving();

        string name = _parameter.ToString() + "_Lvl_" + _lvl_index.ToString();

        return PlayerPrefs.GetFloat(name);
    }


    /// <summary>
    /// Узнать значение параметра int
    /// </summary>
    /// <param name="_parameter">Тип параметра</param>
    /// <returns></returns>
    public static int Know_parameter(Type_parameter_value_int _parameter)
    {
        if (!PlayerPrefs.HasKey(nameof(Initialization_saving_bool)))
            Initialization_saving();

        string name = _parameter.ToString();

        return PlayerPrefs.GetInt(name);
    }



    /// <summary>
    /// Узнать параметр уровня с int значением
    /// </summary>
    /// <param name="_parameter">Параметр</param>
    /// <param name="_lvl_index">Индекс уровня</param>
    /// <returns></returns>
    public static int Know_parameter(Type_parameter_value_int _parameter, int _lvl_index)
    {
        if (!PlayerPrefs.HasKey(nameof(Initialization_saving_bool)))
            Initialization_saving();

        string name = _parameter.ToString() + "_Lvl_" + _lvl_index.ToString();

        return PlayerPrefs.GetInt(name);
    }






    /// <summary>
    /// Узнать активность параметра
    /// </summary>
    /// <param name="_parameter">Тип параметра</param>
    /// <returns></returns>
    public static bool Know_parameter(Type_parameter_bool _parameter)
    {
        if (!PlayerPrefs.HasKey(nameof(Initialization_saving_bool)))
            Initialization_saving();

        bool bool_ = false;

        string name = _parameter.ToString();

        bool_ = PlayerPrefs.GetInt(name) == 1 ? true : false;


        return bool_;
    }


    /// <summary>
    /// Узнать параметр уровня с bool значением
    /// </summary>
    /// <param name="_parameter">Параметр</param>
    /// <param name="_lvl_index">Индекс уровня</param>
    /// <returns></returns>
    public static bool Know_parameter(Type_parameter_bool _parameter, int _lvl_index)
    {
        if (!PlayerPrefs.HasKey(nameof(Initialization_saving_bool)))
            Initialization_saving();

        bool bool_ = false;

        string name = _parameter.ToString() + "_Lvl_" + _lvl_index.ToString();

        bool_ = PlayerPrefs.GetInt(name) == 1 ? true : false;

        return bool_;
    }


}
