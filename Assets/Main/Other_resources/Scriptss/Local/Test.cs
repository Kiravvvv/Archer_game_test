using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Small, Medium, Large
}

public class Test : MonoBehaviour
{
	[System.Serializable]
	class EnemyConfig
	{

		public GameObject prefab = default;

		[Range(0.5f, 2f)]
		public float scale =  1f;

		[Range(0.2f, 5f)]
		public float speed =  1f;

		[Range(-0.4f, 0.4f)]
		public float pathOffset = 0f;
	}

	[SerializeField]
	EnemyConfig small = default, medium = default, large = default;
}
