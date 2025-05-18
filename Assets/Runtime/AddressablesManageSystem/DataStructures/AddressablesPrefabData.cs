using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class AddressablesPrefabData
{
	[SerializeField] private AssetReferenceGameObject assetReferenceGO;
	[SerializeField] private Transform target;
	[NonSerialized] public bool isInstantiated;
	[NonSerialized] public GameObject instance;
	[SerializeField] private Transform folder;

	public AssetReferenceGameObject AssetReferenceGO { get => assetReferenceGO; }
	public Transform Folder { get => folder; }
	public Transform Target { get => target; }
}
