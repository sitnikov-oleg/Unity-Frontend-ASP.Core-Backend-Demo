using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SharedObjects
{
	private List<TypeGOPrefab> typeGameObjectDatasList;

	public GameObject GetLootPrefab(CharacterType type)
	{
		var data = typeGameObjectDatasList.FirstOrDefault(a => a.type == type.ToString());

		if (data.prefab == null)
			throw new NullReferenceException($"{type} is not present in SharedObjects");

		return data.prefab;
	}

	public void SetTypeGameObjectDatasList(List<TypeGOPrefab> list) => typeGameObjectDatasList = list;
}
