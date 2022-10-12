//Скрипт для отображение загрузки следующей сцены
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class LevelLoading : MonoBehaviour {

    [Header ("Прогресс бар загрузки(не обязатялен)")]
    [SerializeField]
	Image ProgressIm = null;

    [Header("Клавиша для запуска следующей сцены")]
    [SerializeField]
    KeyCode Key_button = KeyCode.Space;

    [Header("Должен ли пользователь нажимать на кнопку, что бы продолжить")]
    [SerializeField]
    bool Player_button_enter = false;

    [Header("Видеоплеер(не обязатялен)")]
    [SerializeField]
    VideoPlayer Video_player = null;

    [Header("Ролик(не обязатялен)")]
    [SerializeField]
    VideoClip Video_clip = null;

    [Header("Сколько должно пройти времени")]
    [Tooltip("Сколько минимум должно пройти времени прежде, чем будет включена новая сцена(не в зависимости от того, как давно сама сцена загружена)")]
    [SerializeField]
    float Timer_end_scene = 4f;

    AsyncOperation asyncOperation = null;

    //Окончание таймера
    bool End_timer;

    void Start () {
        if(Video_clip != null)
        Video_player.clip = Video_clip;

        StartCoroutine(LoadScene());
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(Timer_end_scene);
        End_timer = true;

    }

    IEnumerator LoadScene()
    {
        yield return null;

        // Начинаем загружать указанную сцену
        if (PlayerPrefs.GetString("Load_scene_name") == "")
            asyncOperation = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("Load_scene_ID"));
        else
            asyncOperation = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("Load_scene_name"));


        // Запретить запуск новой сцены
        asyncOperation.allowSceneActivation = false;
        //Debug.Log("Pro :" + asyncOperation.progress);
        // Когда загрузка еще продолжается, вывести текст и индикатор выполнения
        while (!asyncOperation.isDone)
        {
            //Полоска или текст прогресса загрузки
            // m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";
            float progress = asyncOperation.progress / 0.9f;
            ProgressIm.fillAmount = progress;


            // Проверяем загрузилась ли сцена
            if (asyncOperation.progress >= 0.9f)
            {
                //Текст говорящий об окончание загрузки
                // m_Text.text = "Press the space bar to continue";
                //Клавиша нажатия для запуска загруженной сцены
                if (Input.GetKeyDown(Key_button) && End_timer || !Player_button_enter)
                {
                    //Запустить сцену
                    if (!Video_player)
                        asyncOperation.allowSceneActivation = true;
                    else if (!Video_player.isPlaying)
                        asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
	
}
