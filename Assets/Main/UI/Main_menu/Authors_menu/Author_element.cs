//Элемент на котором записаны все данные
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Author_element : MonoBehaviour
{
    [Tooltip("Имя")]
    [SerializeField]
    Text Name = null;

    [Tooltip("Ссылка на автора в ВК")]
    [SerializeField]
    Link Link_VK = null;

    [Tooltip("Ссылка на автора в Телеграмм")]
    [SerializeField]
    Link Link_Telegram = null;

    public void Update_info(string _name, string _link_VK, string _link_Telegram)//Обновить данные
    {
        Name.text = _name;
        Link_VK.link = _link_VK;
        Link_Telegram.link = _link_Telegram;


        if (Link_VK.link == "")
            Link_VK.gameObject.SetActive(false);

        if (Link_Telegram.link == "")
            Link_Telegram.gameObject.SetActive(false);

    }
}
