//Скрипт который заменяет текст элемента на котором он находится в зависимости от настройки языка
//Повесить на каждый объект которому требуется перевод
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
//Библеотека для работы с XML
using System.Xml.Linq;

public class Lang_manager : MonoBehaviour {

	[Header("Элемент UI где отображается текст")]
	[Header("Если не указать, то берёт элемент на объекте сам")]
	[SerializeField]
	private Text UI_text;

    [Header("Название папки с переводом")]
    public string Folder_name = "Main_menu";

    [Header("Раздел с текстом")]
    public string Indificator = "Menu";

	[Header("Название текста")]
	public string Text_name = "Start";

	//Путь сохраняемого файла с сохранениями Xml
	private string Path;

	private void Start(){
        if (FindObjectOfType<Setting_menu>())
        {
            Setting_menu.Instance.Language_d += Load;
        }
	}

	void OnEnable(){
		if (UI_text == null)
			UI_text = GetComponent<Text> ();

		//Начать загрузку текста
        if(PlayerPrefs.HasKey("Language_option"))
		Load(PlayerPrefs.GetInt("Language_option"));
        else
            Load(0);

        //FindObjectOfType<Language_and_sound_system> ().List_lang_manager.Add (GetComponent<Lang_manager>());

    }

	/// <summary>
	/// Загрузить текст
	/// </summary>
	/// <param name="_index">Индекс языка</param>
	public void Load(int _index){

        string language = "";

        switch (_index)
        {
            case 0:
                language = "Ru";
                break;
            case 1:
                language = "Eng";
                break;
            case 2:
                language = "Jap";
                break;
            case 3:
                language = "Ukr";
                break;
            default:
                language = "Ru";
                break;
        }

        XElement root = null;

		//Маршрут сохранения и загрузки
		//Если игра на ПК
		if (Application.platform != RuntimePlatform.Android) {
			Path = Application.streamingAssetsPath + "/Languages/" + Folder_name + "/" + language + ".xml";
			//Path = File.ReadAllText (Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString("Language") + ".xml");
			//Path = Application.dataPath + "/Save_game/Save.xml";


			//Если найден файл
			if (File.Exists (Path)) {

				root = XDocument.Parse (File.ReadAllText (Path)).Element ("Root");

				//Разделы с текстом
				XElement Indificat = root.Element (Indificator);

				//Перемещение из сохранения в файл
				UI_text.text = Indificat.Attribute (Text_name).Value;

			}
		}
		//Если игра на Андроид
		else if(Application.platform == RuntimePlatform.Android)  {

			StartCoroutine(LoadFile(language));
		}

	}
	    

	private IEnumerator LoadFile(string _language) 
	{

        string filePath  = System.IO.Path.Combine(Application.streamingAssetsPath, "Languages" + Folder_name + "/");
		filePath = System.IO.Path.Combine(filePath , _language + ".xml");

        //if(filePath.Contains("://")) 
        //{
#pragma warning disable CS0618 // Тип или член устарел
        WWW www = new WWW(filePath);
#pragma warning restore CS0618 // Тип или член устарел

        yield  return www; 
		//	if(string.IsNullOrEmpty(www.error)) 
		//	{ 
		string Pathh = www.text; 
		//	} 
		//} 
		//	else 
		//{ 
		//	Path = System.IO.File.ReadAllText(filePath); 
		//} 

		XElement root = XDocument.Parse (Pathh).Element ("Root");

		//Разделы с текстом
		XElement Indificat = root.Element (Indificator);

		//Перемещение из сохранения в файл
		UI_text.text = Indificat.Attribute (Text_name).Value;

	}
}
