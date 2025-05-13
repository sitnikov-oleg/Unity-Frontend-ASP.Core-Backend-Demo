using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SharedObjects
{
	private List<TypeGOPrefab> typeGameObjectDatasList;

	public GameObject GetLootPrefab(CharacterType type)
	{
		var data = typeGameObjectDatasList.FirstOrDefault(a => a.Type == type);

		if (data == null)
			throw new NullReferenceException($"{type} is not present in SharedObjects");

		return data.Prefab;
	}

	public void SetTypeGameObjectDatasList(List<TypeGOPrefab> list) => typeGameObjectDatasList = list;
}
