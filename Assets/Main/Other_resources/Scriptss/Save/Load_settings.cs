//Скрипт загрузки данных для настроек
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Библеотека для работы с XML
using System.Xml.Linq;
//Работа с сохранениями файлов
using System.IO;
using System;

public class Load_settings : MonoBehaviour {

    /*

	//Путь сохраняемого файла с сохранениями Xml
	private string Path;

	private void Awake(){
		//Маршрут сохранения и загрузки
		//Если игра на ПК
		if (Application.platform != RuntimePlatform.Android) {
			Path = Application.dataPath + "/Main/Save_game/Save_settings_" + Application.productName + ".xml";
		}
		//Если игра на Андроид
		else {
			Path = Application.persistentDataPath + "/Main/Save_game/Save_settings_" + Application.productName + ".xml";
		}
			
			//Начать загрузку сохранений
		if (File.Exists (Path))
			Load_data_settings();
	}

	//Загрузить сохранённые данные
	public void Load_data_settings(){

		XElement root = null;

		//Если найдено сохранение
			root = XDocument.Parse (File.ReadAllText (Path)).Element ("Root");

		XElement instance_settings = root.Element ("Settings");

			//Перемещение из сохранения в файл настроек 
		//Управление персонажем
		Data_settings.Walk_forward_key = (KeyCode)System.Enum.Parse(typeof(KeyCode), instance_settings.Attribute ("Walk_forward_key").Value);
		Data_settings.Walk_back_key = (KeyCode)System.Enum.Parse(typeof(KeyCode), instance_settings.Attribute ("Walk_back_key").Value);
		Data_settings.Walk_left_key = (KeyCode)System.Enum.Parse(typeof(KeyCode), instance_settings.Attribute ("Walk_left_key").Value);
		Data_settings.Walk_right_key = (KeyCode)System.Enum.Parse(typeof(KeyCode), instance_settings.Attribute ("Walk_right_key").Value);
		Data_settings.Jump_key = (KeyCode)System.Enum.Parse(typeof(KeyCode), instance_settings.Attribute ("Jump_key").Value);
		Data_settings.Attack_key = (KeyCode)System.Enum.Parse(typeof(KeyCode), instance_settings.Attribute ("Attack_key").Value);
		//Насйтроки звука
		Data_settings.Affect_value = float.Parse(instance_settings.Attribute ("Affect_value").Value);
		Data_settings.Music_value = float.Parse(instance_settings.Attribute ("Music_value").Value);
		//Насйтроки языка
		Data_settings.Language = int.Parse(instance_settings.Attribute ("Language").Value);

	}
    */
}
