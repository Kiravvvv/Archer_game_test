//Скрипт выбор уровня
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List_lvl : MonoBehaviour {
	
	[Tooltip("Номер сцены которую нужно загрузить")]
	[SerializeField]
	int Scene_number = 1;

	[Tooltip("Скрипт для смены загрузочной сцены")]
	[SerializeField]
	Load_scene Script_load_scene = null;

	public void OnEnable(){
		Script_load_scene.Scene_number = Scene_number;
	}
}
