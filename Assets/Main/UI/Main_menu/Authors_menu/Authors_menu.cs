//Инструмент для занесение новых имён в меню авторов
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum Type_author//Тип авторов
{
    Game_designer,
    Programmer,
    Artist_2D,
    Artist_3D,
    Interface_Artist,
    Little_author,
    Tester,
    Sound_designer,
    Voice_actor,
    Level_designer,
    Screenwriter,
    Special_thanks
}

public class Authors_menu : MonoBehaviour
{

    [Header("Настройки строчки автора")]
    [Tooltip("В какую категорию добавить автора")]
    [SerializeField]
    Type_author Author_category = Type_author.Programmer;

    [Tooltip("Имя автора")]
    [SerializeField]
    string Name_Author = "Имя Фамилия";

    [Tooltip("Ссылка на автора в ВК")]
    [SerializeField]
    string Link_VK = null;

    [Tooltip("Ссылка на автора в Телеграмм")]
    [SerializeField]
    string Link_Telegram = null;




    [Header("Прочие праметры")]
    [Space(20)]
    [Tooltip("Префаб строчки автора")]
    [SerializeField]
    GameObject Authors_prefab_menu = null;

    [Tooltip("Родительские объекты к которым будет привязыватся имена авторов")]
    [SerializeField]
    Transform[] Parrents_transform = new Transform[0];

    private void Start()
    {
        Preparation();
    }

    void Preparation()//Проверка и подготовка
    {
        for (int x = 0; x < Parrents_transform.Length; x++)
        {
            if (Parrents_transform[x].childCount <= 1)
            {
                Parrents_transform[x].gameObject.SetActive(false);
            }
        }
    }

    public void Generation_author()//Добавить автора в категорию программистов
    {
        Transform parrent_transform = null;

        switch (Author_category)
        {
            case Type_author.Game_designer:
                parrent_transform = Parrents_transform[0];
                break;
            case Type_author.Programmer:
                parrent_transform = Parrents_transform[1];
                break;
            case Type_author.Artist_2D:
                parrent_transform = Parrents_transform[2];
                break;
            case Type_author.Artist_3D:
                parrent_transform = Parrents_transform[3];
                break;
            case Type_author.Interface_Artist:
                parrent_transform = Parrents_transform[4];
                break;
            case Type_author.Little_author:
                parrent_transform = Parrents_transform[5];
                break;
            case Type_author.Tester:
                parrent_transform = Parrents_transform[6];
                break;
            case Type_author.Sound_designer:
                parrent_transform = Parrents_transform[7];
                break;
            case Type_author.Voice_actor:
                parrent_transform = Parrents_transform[8];
                break;
            case Type_author.Level_designer:
                parrent_transform = Parrents_transform[9];
                break;
            case Type_author.Screenwriter:
                parrent_transform = Parrents_transform[10];
                break;
            case Type_author.Special_thanks:
                parrent_transform = Parrents_transform[11];
                break;
        }

        GameObject obj = Instantiate(Authors_prefab_menu, parrent_transform);
        obj.GetComponent<Author_element>().Update_info(Name_Author, Link_VK, Link_Telegram);
    }
}
