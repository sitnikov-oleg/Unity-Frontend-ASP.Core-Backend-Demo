using System;
using UnityEngine;

[Serializable]
public class TypeGOPrefab
{
	[SerializeField] private CharacterType type;
	[SerializeField] private GameObject prefab;

	public CharacterType Type { get => type; }
	public GameObject Prefab { get => prefab; }
}

