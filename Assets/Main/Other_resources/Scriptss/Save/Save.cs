//Скрипт сохранения данных
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Библеотека для работы с XML
using System.Xml.Linq;
//Работа с сохранениями файлов
using System.IO;
using System;

public class Save : MonoBehaviour {
    /*

	//Путь сохраняемого файла с сохранениями Xml
	private string Path;

	private void Awake(){
		//Маршрут сохранения и загрузки
		//Если игра на ПК
		if (Application.platform != RuntimePlatform.Android) {
			System.IO.Directory.CreateDirectory(Application.dataPath + "/Main/Save_game");
			Path = Application.dataPath + "/Main/Save_game/Save_game_" + Application.productName + ".xml";
		}
		//Если игра на Андроид
		else {
			System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/Save_game");
			Path = Application.persistentDataPath + "/Main/Save_game/Save_game_" + Application.productName + ".xml";
		}
			
	}

	//Сохранение данных
	public void Save_data_game (){
		//Создаёт корневое название для документа сохранения
		XElement root = new XElement ("Root"); 



		//Сохраняемые параметры игры в атрибут XML
		//Игровые очки
		XAttribute Score = new XAttribute("Score", Data_game.Score);



		//Составление атрибута XML для игровых переменных
		XElement element_game = new XElement ("Game", Score);


		root.Add (element_game);

		//Сохранить документ XML
		XDocument Save_document = new XDocument (root);

		File.WriteAllText(Path, Save_document.ToString());

	}
    */
}
