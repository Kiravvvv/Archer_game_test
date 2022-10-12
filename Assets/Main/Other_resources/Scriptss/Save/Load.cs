//Скрипт загрузки данных
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Библеотека для работы с XML
using System.Xml.Linq;
//Работа с сохранениями файлов
using System.IO;
using System;

public class Load : MonoBehaviour {
    /*

	//Путь сохраняемого файла с сохранениями Xml
	private string Path;

	[Tooltip("Запретить загрузку автоматически при старте")]
	[SerializeField]
	private bool No_start_load = false;

	private void Awake(){
		//Маршрут сохранения и загрузки
		//Если игра на ПК
		if (Application.platform != RuntimePlatform.Android) {
			Path = Application.dataPath + "/Main/Save_game/Save_" + Application.productName + ".xml";
		}
		//Если игра на Андроид
		else {
			Path = Application.persistentDataPath + "/Main/Save_game/Save_" + Application.productName + ".xml";
		}
			
			//Начать загрузку сохранений
		if (File.Exists (Path) && !No_start_load)
			Load_data_game();
	}

	//Загрузить сохранённые данные
	public void Load_data_game(){

		XElement root = null;

		//Если найдено сохранение
			root = XDocument.Parse (File.ReadAllText (Path)).Element ("Root");

		XElement instance_game = root.Element ("Game");


		//Перемещение из сохранения в файл игровых параметров 
		//Игровые очки 
		Data_game.Score = int.Parse(instance_game.Attribute ("Score").Value);

	}
    */
}
