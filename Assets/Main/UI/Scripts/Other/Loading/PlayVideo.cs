//Скрипт запуска видео и отслеживание его окончания для загрузки сцены
using System.Collections;
using System.Collections.Generic;
//Скрипт для показа ролика и загрузки нужной сцены.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;



public class PlayVideo : MonoBehaviour
{

    enum Type_level_loading
    {
        Instant,
        Progressbar
    }

    [Header("Тип загрузки")]
    [SerializeField]
    Type_level_loading Type = Type_level_loading.Instant;

    [Header("Номер сцены которую нужно загрузить")]
    [SerializeField]
    int Scene_number = 0;

    [Header("Имя сцены которую нужно загрузить (если пусто, то загрузка через номер)")]
    [SerializeField]
    string Scene_name = null;

    [Header("Видеоплеер")]
    [SerializeField]
    VideoPlayer Video_player = null;

    [Header("Ролик")]
    [SerializeField]
    VideoClip Video_clip = null;

    bool Active = false;

    private void Start()
    {
        Video_player.clip = Video_clip;
        StartCoroutine(Caru());
    }

    private void Update()
    {
        if (!Video_player.isPlaying && Active || Input.GetKeyDown(KeyCode.Escape))
        {
            //Мгновенный вариант загрузки
            if (Type == Type_level_loading.Instant)
            {
                if (Scene_name == "")
                {
                    SceneManager.LoadScene(Scene_number);
                }
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

    IEnumerator Caru()
    {
        Video_player.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);

        while (!Video_player.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        Video_player.Play();
        Active = true;

    }
}
 