//Скрипт который содержит звуки.
//Вешать на всё, что издаёт звуки.
// Для настройки уровня звука во время игры требуется на сцене Setting_menu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

enum Type_sound
{
    Music,
    Effect
}

enum Type_gameObject
{
    Destroy,
    No_destroy
}

public class Sound_control : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
    [Tooltip("В зависимости от этого будет применятся настройка громкости музыки или звуков")]
    [Header("Тип издаваемых звуков")]
    [SerializeField]
    Type_sound Type = Type_sound.Music;

    [Tooltip("В зависимости от типа будет искаться проигрыватель для звуков")]
    [Header("Тип игрового объекта")]
    [SerializeField]
    Type_gameObject Type_gameObject_enum = Type_gameObject.Destroy;

    [Header("Компонент динамик")]
    public AudioSource Audioo = null;

    [Header("Активность объекта")]
    [Tooltip("Звук который будет проигрыватся при включение объекта")]
    [SerializeField]
    AudioClip Sound_enable = null;

    [Tooltip("Будет ли звук зацикленным")]
    [SerializeField]
    bool Sound_enable_bool = false;

    [Tooltip("Звук который будет проигрыватся при выключение/уничтожение объекта")]
    [SerializeField]
    AudioClip Sound_disable = null;

    [Tooltip("Будет ли звук зацикленным")]
    [SerializeField]
    bool Sound_disable_bool = false;

    [Header("Если это UI")]
    [Tooltip("Звук который будет проигрыватся при наведение курсора на объект")]
    [SerializeField]
    AudioClip Sound_OnPointerEnter = null;

    [Tooltip("Будет ли звук зацикленным")]
    [SerializeField]
    bool Sound_OnPointerEnter_bool = false;

    [Tooltip("Звук который будет проигрыватся при нажимание")]
    [SerializeField]
    AudioClip Sound_OnPointerDown = null;

    [Tooltip("Будет ли звук зацикленным")]
    [SerializeField]
    bool Sound_OnPointerDown_bool = false;

    [Tooltip("Звук который будет проигрыватся при отпускание")]
    [SerializeField]
    AudioClip Sound_OnPointerUp = null;

    [Tooltip("Будет ли звук зацикленным")]
    [SerializeField]
    bool Sound_OnPointerUp_bool = false;

    [Header("Если это игровой объект")]
    [Tooltip("Звук который будет проигрыватся при клике по этому объекту")]
    [SerializeField]
    AudioClip Sound_click = null;

    [Tooltip("Будет ли звук зацикленным")]
    [SerializeField]
    bool Sound_click_bool = false;

    [Header("Список для прочих звуков")]
    [SerializeField]
    Sound_class[] Sound_array = new Sound_class[0];

    float Setting_administrator_Sound = 0;//Громкость звука заданный в глобальных настройках

    private float Default_value;

    void Awake()
    {
        if (Type == Type_sound.Music)
            Setting_administrator_Sound = Save_PlayerPrefs.Know_parameter(Type_parameter_value_float.Sound_music);
        else if (Type == Type_sound.Effect)
            Setting_administrator_Sound = Save_PlayerPrefs.Know_parameter(Type_parameter_value_float.Sound_effect);

        if (Type_gameObject_enum == Type_gameObject.Destroy)
        {
            if (GameObject.Find("Audio"))
            {
                if (Type == Type_sound.Effect)
                {
                    if (GameObject.Find("Audio").transform.Find("Effect"))
                        Audioo = GameObject.Find("Audio").transform.Find("Effect").GetComponent<AudioSource>();
                    else
                    {
                        if (!GameObject.Find("Audio"))
                        {
                            GameObject gm = new GameObject();
                            gm.name = "Audio";
                        }

                        GameObject go = new GameObject();
                        go.name = "Effect";
                        go.transform.SetParent(GameObject.Find("Audio").transform);
                        Audioo = go.AddComponent<AudioSource>();
                    }
                }
                else if (Type == Type_sound.Music)
                {
                    if (GameObject.Find("Audio").transform.Find("Music"))
                        Audioo = GameObject.Find("Audio").transform.Find("Music").GetComponent<AudioSource>();
                    else
                    {
                        if (!GameObject.Find("Audio"))
                        {
                            GameObject gm = new GameObject();
                            gm.name = "Audio";
                        }

                        GameObject go = new GameObject();
                        go.name = "Music";
                        go.transform.SetParent(GameObject.Find("Audio").transform);
                        Audioo = go.AddComponent<AudioSource>();
                    }
                }
            }
        }
        else {
            if (Audioo == null)
            {
                if (GetComponent<AudioSource>())
                    Audioo = GetComponent<AudioSource>();
                else
                    Audioo = gameObject.AddComponent<AudioSource>();
            }
        }


        Default_value = Audioo.volume;

    }

    void OnEnable()
    {
        Preparation_sound();

        if (FindObjectOfType<Setting_menu>())
        {
            if (Type == Type_sound.Music)
                Setting_menu.Instance.Music_d += Change_value;
            else
                Setting_menu.Instance.Effect_sound_d += Change_value;
        }

        if (Sound_enable != null)
            Play_sound(Sound_enable, Sound_enable_bool);
    }

    private void OnDisable()
    {
        if (Sound_disable != null && Audioo != null)
            Play_sound(Sound_disable, Sound_disable_bool);

        if (FindObjectOfType<Setting_menu>())
        {
            if (Type == Type_sound.Music)
                Setting_menu.Instance.Music_d -= Change_value;
            else
                Setting_menu.Instance.Effect_sound_d -= Change_value;
        }
    }

    /// <summary>
    /// Настройка звука в начале
    /// </summary>
    public void Preparation_sound()
    {
        Audioo.volume = Default_value * Setting_administrator_Sound;
    }

    private void OnMouseDown()
    {
        if (Sound_click != null)
            Play_sound(Sound_click, Sound_click_bool);
    }

    public void OnPointerEnter(PointerEventData eventData)//Звук который будет проигрыватся при наведение курсора на объект
    {
        if (Sound_OnPointerEnter != null)
            Play_sound(Sound_OnPointerEnter, Sound_OnPointerEnter_bool);
    }

    public void OnPointerDown(PointerEventData eventData)//Звук который будет проигрыватся при нажимание на кнопку
    {
        if (Sound_OnPointerDown != null)
            Play_sound(Sound_OnPointerDown, Sound_OnPointerDown_bool);
    }

    public void OnPointerUp(PointerEventData eventData)//Звук который будет проигрыватся при отпускание кнопки
    {
        if (Sound_OnPointerUp != null)
            Play_sound(Sound_OnPointerUp, Sound_OnPointerUp_bool);
    }

    /// <summary>
    /// Изменение громкости
    /// </summary>
    /// <param name="_value">Значение громкости</param>
    void Change_value(float _value)
    {
        if (Audioo)
        {
            Audioo.volume = Default_value * _value;
        }
    }

    /// <summary>
    /// Запустить звук
    /// </summary>
    /// <param name="_clip">Клип звука</param>
    /// <param name="_loop">Зациклено?</param>
    void Play_sound(AudioClip _clip, bool _loop)
    {
        if (Audioo)
        {
            Audioo.loop = _loop;
            if (_loop)
            {
                Audioo.clip = _clip;
                Audioo.Play();
            }
            else
            {
                Audioo.PlayOneShot(_clip);
            }
        }
    }

    /// <summary>
    /// Включить один раз воспроизведение звука из списка
    /// </summary>
    /// <param name="_id_sound">ID звука из списка</param>
    public void Sound_PlayOneShot(int _id_sound)
    {
        if (Audioo)
        {
            Audioo.volume = Default_value * Sound_array[_id_sound].Sound_volume * Setting_administrator_Sound;


            Audioo.loop = false;
            Audioo.PlayOneShot(Sound_array[_id_sound].Sound);
        }
    }

    /// <summary>
    /// Включить зацикленный звук из списка
    /// </summary>
    /// <param name="_id_sound">ID звука из списка</param>
    public void Sound_play_Loop(int _id_sound)
    {
        if (Audioo)
        {
            Audioo.volume = Default_value * Sound_array[_id_sound].Sound_volume * Setting_administrator_Sound;

            Audioo.clip = Sound_array[_id_sound].Sound;
            Audioo.loop = true;
            Audioo.Play();
        }
    }

    [System.Serializable]
    protected class Sound_class//Класс содержащий описание и ссылку на сам звук
    {
        [Tooltip("Описание для звука, что бы не путаться")]
        [SerializeField]
        public string Name = "Название звука";

        [Tooltip("Ссылка на звук")]
        [SerializeField]
        public AudioClip Sound = null;

        [Range(0f, 2f)]
        [Tooltip("Индвидуальная громкость звука(если изначальный звук слишком громкий или тихий")]
        [SerializeField]
        public float Sound_volume = 1;
    }

}
