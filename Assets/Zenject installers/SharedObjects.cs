using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SharedObjects : MonoInstaller
{
	[SerializeField] private List<TypeGOPrefab> typeGameObjectDatasList;

	public override void InstallBindings()
	{
		Container.Bind<SharedObjects>().AsSingle();
	}

	public GameObject GetLootPrefab(CharacterType type)
	{
		var data = typeGameObjectDatasList.FirstOrDefault(a => a.type == type);

		if (data == null)
			throw new NullReferenceException($"{type} is not present in SharedObjects");

		return data.prefab;
	}
}
