//Скрипт который считывает версию приложения
//Вешается на объект содержащий информацию о версии
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Version_manager : MonoBehaviour {

	[Header("Текст UI")]
	[SerializeField]
	private Text Version_text = null;

	void Start () {
		
		Version_text.text = "Версия"+": V"+Application.version;
	}

}
