//Скрипт для назначения клавиш управления
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input_setting : MonoBehaviour
{
    [Tooltip("Текст кнопки")]
    [SerializeField]
    private Text Button_text = null;

    [Tooltip("Ключ при сохранение данных")]
    [SerializeField]
    private string Save_key_name = null;

    [Tooltip("Клавиша по умолчанию")]
    [SerializeField]
    private KeyCode Default_key_code = KeyCode.None;

    KeyCode Active_key_code;//Кнопка с которой будем работать

    private IEnumerator Coroutine;//Работающая сейчас карутина

    private void OnEnable()
    {
        if (Setting_menu.Instance)
            Setting_menu.Instance.Reset_d += Reset_button;
    }

    private void OnDisable()
    {
        if (Setting_menu.Instance)
            Setting_menu.Instance.Reset_d -= Reset_button;
    }

    private void Start()
    {
        Load_key();
    }

    private void Reset_button()
    {
        Active_key_code = Default_key_code;
        PlayerPrefs.SetString(Save_key_name, Active_key_code.ToString());
        Button_text.text = Active_key_code.ToString();
    }

    void Load_key()//Загрузить кнопку из памяти
    {
        if (PlayerPrefs.HasKey(Save_key_name))
        {
            string key_name = PlayerPrefs.GetString(Save_key_name);
            Active_key_code = (KeyCode)System.Enum.Parse(typeof(KeyCode), key_name);
            Button_text.text = PlayerPrefs.GetString(Save_key_name);
        }
        else
        {
            Reset_button();
        }
    }

    public void ButtonSetKey() // событие кнопки, для перехода в режим ожидания
    {
        if (Coroutine == null)
        {
            Button_text.text = "???";
            Coroutine = Wait();//Заносит в переменную карутину
            StartCoroutine(Coroutine);//Запускает карутину
        }
    }

    // Ждем, когда игрок нажмет какую-нибудь клавишу, для привязки
    // Если будет нажата клавиша 'Escape', то отмена
    IEnumerator Wait()
    {
        while (true)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StopCoroutine(Coroutine);
                Coroutine = null;
                Load_key();
            }

            else
            {
                foreach (KeyCode _key in KeyCode.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(_key))
                    {
                        Active_key_code = _key;
                        Button_text.text = _key.ToString();
                        PlayerPrefs.SetString(Save_key_name, _key.ToString());

                        if (FindObjectOfType<Setting_menu>())
                            Setting_menu.Instance.Input_key_control();//Высылает всем заинтересованным в изменение управления, через другой скрипт

                        StopCoroutine(Coroutine);
                        Coroutine = null;
                    }
                }
            }

        }
    }
}
