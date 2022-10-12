//Скрипт для загрузки сцен
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_scene : MonoBehaviour {


    enum Type_level_loading{
        Instant,
        Progressbar
    }

    [Header("Тип загрузки")]
    [SerializeField]
    private Type_level_loading Type = Type_level_loading.Instant;

    [Header("Номер сцены которую нужно загрузить")]
	public int Scene_number = 0;

    [Header("Имя сцены которую нужно загрузить")]
    [Tooltip("Если пусто, то загрузка через номер")]
    [SerializeField]
     string Scene_name = null;


    /// <summary>
    /// Загрузить
    /// </summary>
    public void Load(){
        //Мгновенный вариант загрузки
        if (Type == Type_level_loading.Instant)
        {
            if (Scene_name == "")
                SceneManager.LoadScene(Scene_number);
            else
                SceneManager.LoadScene(Scene_name);
        }

        //Вариант загрузки через загрузочное меню
        else if (Type == Type_level_loading.Progressbar)
        {
            if (Scene_name == "")
            {
                PlayerPrefs.SetInt("Load_scene_ID", Scene_number);
                PlayerPrefs.SetString("Load_scene_name", "");
            }
            else
                PlayerPrefs.SetString("Load_scene_name", Scene_name);

            PlayerPrefs.Save();
            
            SceneManager.LoadScene("Loading");
        }
    }
}
