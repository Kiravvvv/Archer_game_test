//Скрипт ссылка на сайт или ещё куда
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Link : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[Tooltip("Ссылка")]
	public string link = "https://vk.com/pixel_star";

	[Tooltip("Цвет при наведение и убирание курсора")]
	[SerializeField]
	Color color_enter = Color.green, color_exit = Color.black;

    [Tooltip("Текст")]
    [SerializeField]
    Text Text_ = null;

	public void OnPointerEnter(PointerEventData eventData)
	{
        if(Text_)
            Text_.color = color_enter; 
	}

	public void OnPointerExit(PointerEventData eventData)
	{
        if (Text_)
            Text_.color = color_exit; 
	}


	public void Active() 
	{ 
		Application.OpenURL (link); 
	} 
}
