//Скрипт изменяющий спрайт UI при улучшении или ухудшении уровня
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_visual_image_ui : MonoBehaviour {

	[Tooltip("Спрайты которые будут менятся")]
	[SerializeField]
	private List<Sprite> Sprite_lvl = new List<Sprite>();

	//Номер спрайта используемый сейчас
	private int Nomer_sprite;

	[Tooltip("Компонент интерфейса для замены картинки")]
	[SerializeField]
	private Image Image_UI = null;

	[Tooltip("Компонент объекта для замены спрайта")]
	[SerializeField]
	private Sprite Sprite_obj;

	/// <summary>
	/// Улучшить (следующая картинка)
	/// </summary>
	public void Up(){
		if (Sprite_lvl.Count-1 > Nomer_sprite) {
			Nomer_sprite += 1;

			if (Image_UI)
				Image_UI.sprite = Sprite_lvl [Nomer_sprite];
		
			if (Sprite_obj)
				Sprite_obj = Sprite_lvl [Nomer_sprite];
		}
	}


	/// <summary>
	/// Ухудшить (прошлая картинка)
	/// </summary>
	public void  Down(){
		if (0 < Nomer_sprite-1) {
			Nomer_sprite -= 1;

			if (Image_UI)
				Image_UI.sprite = Sprite_lvl [Nomer_sprite];

			if (Sprite_obj)
				Sprite_obj = Sprite_lvl [Nomer_sprite];
		}
	}
}
