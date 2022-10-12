//Скрипт отвечающий за ход времени в игре
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_game_change : MonoBehaviour {

	/// <summary>
	/// Остановить время
	/// </summary>
	public void Time_stop(){
		Time.timeScale = 0;
	}

	/// <summary>
	/// Остановить время
	/// </summary>
	public void Time_play(){
		Time.timeScale = 1;
	}

	/// <summary>
	/// Замедлить время
	/// </summary>
	/// <param name="tm">Значение</param>
	public void Time_slowdown(float _value){
		Time.timeScale = _value;
	}
}
