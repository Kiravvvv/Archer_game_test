//Скрипт подсветка кнопок и их звуки при наведение, убирание наведения и при нажатие.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Illumination : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler {

    [Header("Звуки")]
    [Tooltip("Звуки для кнопок")]
    [SerializeField]
    AudioClip Sound_enter = null;

    [Tooltip("Звуки для кнопок")]
    [SerializeField]
    AudioClip Sound_exit = null, Sound_homing = null;

    [Space(2)]
    [Header("Спрайты")]
    [Tooltip("Спрайт начальный")]
    [SerializeField]
    Sprite Sprite_def = null;

    [Tooltip("Спрайт меняющий стандартный при наведение")]
	[SerializeField]
	Sprite Sprite_enter = null;

    [Tooltip("Спрайт меняющий стандартный при нажатие")]
    [SerializeField]
    Sprite Sprite_homing = null;

	AudioSource audioo = null;

	//Подготовка
	void Start(){
		audioo = GetComponent<AudioSource> ();
        if(Sprite_def == null)
		Sprite_def = GetComponent<Image> ().sprite;
	}

	//При наведение
	public void OnPointerEnter(PointerEventData eventData)
	{
        if (Sprite_enter != null)
        {
            GetComponent<Image>().sprite = Sprite_enter;

            if (Sound_enter != null)
                audioo.PlayOneShot(Sound_enter);
        }
	}

	//При убирание курсора с кнопки
	public void OnPointerExit(PointerEventData eventData)
	{
        if (Sprite_enter != null)
        {
            GetComponent<Image>().sprite = Sprite_def;

            if (Sound_exit != null)
                audioo.PlayOneShot(Sound_exit);
        }
	}

	//При нажатие на кнопку
	public void OnPointerDown(PointerEventData eventData)
	{
        if (Sprite_homing != null)
        {
            GetComponent<Image>().sprite = Sprite_homing;

            if (Sound_exit != null)
                audioo.PlayOneShot(Sound_homing);
        }
	}

    //При отпускание на кнопку
    public void OnPointerUp(PointerEventData eventData)
    {
        if (Sprite_homing != null)
        {
            GetComponent<Image>().sprite = Sprite_def;

            if (Sound_exit != null)
                audioo.PlayOneShot(Sound_homing);
        }
    }

}
