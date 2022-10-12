//Скрипт который показывает описание при наведение на объект UI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Description_hint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


    GameObject Obj_description_hint = null;//Ссылка на объект содержащий подсказки

	//Выбранная подсказка
	private GameObject UI_hint = null;

	[Header("Точка к на позицию которой встаёт подсказка")]
	[Tooltip("Если не указать, то крепится к этому объекту")]
	[SerializeField]
	private Transform Position_object;

    [Header("Название папки с переводом")]
    [SerializeField]
    string Folder_name = "Main_menu";

    [Header("Раздел с текстом в документе(загрузка текста)")]
	[SerializeField]
	private string Indificator = "Menu";

	[Header("Название текста в документе(загрузка текста)")]
	[SerializeField]
	private string Text_name = "Start";

    private void Start()
    {
        Obj_description_hint = GameObject.Find("Description_hint").gameObject;
    }


    public void OnPointerEnter(PointerEventData eventData)
	{

        string name_hint = "";

		if (Position_object == null)
			Position_object = transform;

        if (Position_object.localPosition.x < 0 && Position_object.localPosition.y > 0) //(Cam.ScreenToWorldPoint(Position_object.position).x < 0 && Cam.ScreenToWorldPoint(Position_object.position).y > 0) 
            name_hint = "Hint_up_left";
        else if (Position_object.localPosition.x > 0 && Position_object.localPosition.y > 0)
            name_hint = "Hint_up_right";
        else if (Position_object.localPosition.x < 0 && Position_object.localPosition.y < 0)
            name_hint = "Hint_down_left";
        else if (Position_object.localPosition.x > 0 && Position_object.localPosition.y < 0)
            name_hint = "Hint_down_right";

        UI_hint = Obj_description_hint.transform.Find(name_hint).gameObject;

        UI_hint.GetComponent<Lang_manager>().Folder_name = Folder_name;
        UI_hint.GetComponent<Lang_manager>().Indificator = Indificator;
        UI_hint.GetComponent<Lang_manager>().Text_name = Text_name;

        Obj_description_hint.transform.Find(name_hint).gameObject.SetActive(true);
        Obj_description_hint.transform.Find(name_hint).transform.position = Position_object.position;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		UI_hint.SetActive(false);
	}

	private void OnDisable(){
        if(UI_hint)
        UI_hint.SetActive(false);
    }

}
