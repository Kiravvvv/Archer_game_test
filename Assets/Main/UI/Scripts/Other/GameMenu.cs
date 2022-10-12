//Скрипт для игрового меню во время паузы
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : Singleton<GameMenu> {


    [Tooltip("Игровое меню")]
    [SerializeField]
    GameObject Game_menu_canvas = null;

    [Tooltip("Включено ли игровое меню")]
    public bool Game_menu_bool = false;

    [Header("Название сцены которая ведёт в главное меню")]
    [SerializeField]
    string Scene_main_menu = "Main_menu";

    [Header("Заранее записаная загрузка уровня")]
    [SerializeField]
    string Load_scene_name = "Menu"; 

    [Header("Клавиша вкл/выкл меню ")]
    [SerializeField]
    KeyCode Exit_key = KeyCode.Escape;

    [Header("Ссылки на игровые вкладки")]
    [SerializeField]
    List<GameObject> Bookmark = new List<GameObject>();

    [Header("Будет влиять на курсор во время включения и выключения меню")]
    [SerializeField]
    bool Cursor_active = false;

    void Update()
    {
        if (Input.GetKeyDown(Exit_key))
        {
            Enter_button();
        }

    }


    /// <summary>
    /// Включение и отключение игрового меню
    /// </summary>
    void Enter_button()
    {
        if (!Game_menu_bool)
        {
            Game_menu_activity(true);

            if (Cursor_active)
                Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            bool null_bookmark = true;

            for (int x = 0; x < Bookmark.Count; x++)
            {
                if (Bookmark[x].activeSelf)
                {
                    null_bookmark = false;
                    Bookmark[x].SetActive(false);
                }
            }
            if (null_bookmark)
            {
                Game_menu_activity(false);

                if (Cursor_active)
                    Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }


    /// <summary>
    /// Включение/Выключение игрового меню
    /// </summary>
    /// <param name="_active">Включить и отключить менюшку</param>
    void Game_menu_activity(bool _active)
    {
        Game_administrator.Instance.Player_control_activity(!_active);
        Game_menu_bool = _active;
        Game_menu_canvas.SetActive(_active);
    }


    /// <summary>
    /// Продолжить дальше (выключить меню)
    /// </summary>
    public void Continue()
    {
        Game_menu_activity(false);
        Time.timeScale = 1;

        if(Cursor_active)
        Game_Player.Cursor_player(false);
    }


    /// <summary>
    /// Перезагрузить сцену (уровень)
    /// </summary>
    public void Restart_scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }


    /// <summary>
    /// Выход в главное меню (загрузить сцену с главным меню)
    /// </summary>
    public void Load_main_menu(){
        Time.timeScale = 1;
        Start_loading(Scene_main_menu);
        Game_menu_activity(false);
    }


    /// <summary>
    /// Загрузить указанную тут сцену (уровень)
    /// </summary>
    public void Load_scene()
    {
        Start_loading(Load_scene_name);
        Time.timeScale = 1;
        Game_menu_activity(false);
    }


    /// <summary>
    /// Загрузить указанную сцену (уровень)
    /// </summary>
    /// <param name="_id_scene">Номер сцены</param>
    public void Load_scene(int _id_scene)
    {
        Start_loading(_id_scene);
        Time.timeScale = 1;
        Game_menu_activity(false);
    }


    /// <summary>
    /// Загрузить указанную сцену (уровень)
    /// </summary>
    /// <param name="_name_scene">Имя сцены</param>
    public void Load_scene(string _name_scene)
    {
        Start_loading(_name_scene);
        Time.timeScale = 1;
        Game_menu_activity(false);
    }



    /// <summary>
    /// Загрузить следующую сцену
    /// </summary>
    public void Next_scene()
    {
        int id_scene = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.sceneCountInBuildSettings > id_scene)
        {
            Start_loading(id_scene);
        }
        else
        {
            //Debug.Log("Сцены закончились, возвращаемся в главное меню.");
            //Load_main_menu();
            Start_loading(0);//Начинаем с нулевой сценой
        }
            
    }


    /// <summary>
    /// Выключить игру
    /// </summary>
    public void Exit_game()
    {
        Application.Quit();
    }









    
    enum Type_level_loading
    {
        Instant,
        Progressbar
    }

    [Space(20)]
    [Header("Тип загрузки")]
    [SerializeField]
    private Type_level_loading Type = Type_level_loading.Instant;

    [Tooltip("Плавность затемнения")]
    [SerializeField]
    float Speed_blackout = 1f;

    [Tooltip("Картинка которая будет затемнятся и осветлятся")]
    [SerializeField]
    Image Image_fon_black = null;

    int Id_number_load_scene = 0;//Номер загружаемой сцены

    string Id_name_load_scene = null;//Имя загружаемой сцены

    bool End = false;//рычаг который показывает конец

    bool Black_up = false;

    float Color_alpha = 1;


    private void FixedUpdate()
    {
        if (!End)
        {
            Active();
        }
    }

    /// <summary>
    /// Активировать
    /// </summary>
    void Active()
    {

        if (Black_up)
        {
            Color_alpha += Speed_blackout * Time.fixedDeltaTime;

            if (Color_alpha >= 1)
            {
                End = true;
                Load();
            }
        }
        else
        {
            Color_alpha -= Speed_blackout * Time.fixedDeltaTime;

            if (Color_alpha <= 0)
                End = true;
        }

        Image_fon_black.color = new Color(0, 0, 0, Color_alpha);

    }

    /// <summary>
    /// Начать загрузку сцены
    /// </summary>
    /// <param name="_number_scene">Номер сцены</param>
    void Start_loading(int _number_scene)
    {
        Id_number_load_scene = _number_scene;

        Black_up = true;
        End = false;
    }

    /// <summary>
    /// Начать загрузку сцены
    /// </summary>
    /// <param name="_name_scene">Имя сцены</param>
    void Start_loading(string _name_scene)
    {
        Id_name_load_scene = _name_scene;

        Black_up = true;
        End = false;
    }



    /// <summary>
    /// Загрузка
    /// </summary>
    void Load()
    {
        //Мгновенный вариант загрузки
        if (Type == Type_level_loading.Instant)
        {
            if (Id_name_load_scene != "" && Id_name_load_scene != " " && Id_name_load_scene != null)
                SceneManager.LoadScene(Id_name_load_scene);
            else
                SceneManager.LoadScene(Id_number_load_scene);
        }

        //Вариант загрузки через загрузочное меню
        else if (Type == Type_level_loading.Progressbar)
        {
            if (Id_name_load_scene == "")
            {
                PlayerPrefs.SetInt("Load_scene_ID", Id_number_load_scene);
                PlayerPrefs.SetString("Load_Id_name_load_scene", "");
            }
            else
                PlayerPrefs.SetString("Load_Id_name_load_scene", Id_name_load_scene);

            PlayerPrefs.Save();

            SceneManager.LoadScene("Loading");
        }

    }


}
