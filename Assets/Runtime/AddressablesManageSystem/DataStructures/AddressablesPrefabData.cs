using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class AddressablesPrefabData
{
	public AssetReferenceGameObject assetReferenceGO;
	public Transform folder, target;
	[NonSerialized] public bool isInstantiated;
	[NonSerialized] public GameObject instance;
}
