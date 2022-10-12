//Скрипт отвечающий за включие и отключение игровых объектов
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_object_button : MonoBehaviour {
	[Header("Активируемые объекты или объекты для кнопки")]
	[SerializeField]
	List<GameObject> On_and_button_objects = new List<GameObject>();


	[Header("Выключаемые объекты")]
	[SerializeField]
	List<GameObject> Off_objects = new List<GameObject>();

	//Переключатель для кнопки
	bool Active = false;

	/// <summary>
	/// Версия кнопки, включает и отключает объекты при повторных нажатиях
	/// </summary>
	public void Button_version(){
		Active = !Active;

		for (int x = 0; x < On_and_button_objects.Count; x++) {
			if(Active)
			On_and_button_objects [x].gameObject.SetActive (true);
			else
				On_and_button_objects [x].gameObject.SetActive (false);
		}
	}


	/// <summary>
	/// Включить объекты и выключить в зависимости от списка
	/// </summary>
	public void On_and_Off(){
		for (int x = 0; x < On_and_button_objects.Count; x++)
			On_and_button_objects [x].gameObject.SetActive (true);

		for (int x = 0; x < Off_objects.Count; x++) 
			Off_objects [x].gameObject.SetActive (false);
	}


}
