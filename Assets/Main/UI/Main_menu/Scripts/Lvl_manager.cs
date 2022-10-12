//Массив выбираемых уровней
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl_manager : MonoBehaviour {

	[Header("Список уровней")]
	[SerializeField]
	private List<GameObject> Lvl = new List<GameObject>();

	int N_lvl = 1;

	public void Next_lvl(){
		N_lvl++;
		if (N_lvl > Lvl.Count)
			N_lvl = 1;

		for (int x = 0; x < Lvl.Count; x++) {
			Lvl [x].gameObject.SetActive (false);
		}

		Lvl [N_lvl-1].gameObject.SetActive (true);
	}

	public void Previous_lvl(){
		N_lvl--;
		if (N_lvl < 1)
			N_lvl = Lvl.Count;

		for (int x = 0; x < Lvl.Count; x++) {
			Lvl [x].gameObject.SetActive (false);
		}

		Lvl [N_lvl-1].gameObject.SetActive (true);
	}
}
