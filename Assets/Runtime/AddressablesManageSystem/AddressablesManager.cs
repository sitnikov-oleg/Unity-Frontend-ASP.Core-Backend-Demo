using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class AddressablesManager : MonoBehaviour
{
	[Inject]
	private SceneReferences sceneReferences;

	[SerializeField]
	private List<AddressablesPrefabData> addressablesPrefabDatasList;
	private bool canCalculate;
	private const float MIN_INSTANTIATE_ANGLE = 40;
	private const float MAX_RELEASE_ANGLE = 45;


	private void Awake()
	{
		EventBus<PlayerReadyEvnt>.Subscribe(OnPlayerReadyEvnt);
	}

	private void OnPlayerReadyEvnt(PlayerReadyEvnt evnt)
	{
		canCalculate = true;
	}

	private void Update()
	{
		if (canCalculate)
		{
			CheckAddressables();
		}
	}

	private void CheckAddressables()
	{
		for (int i = 0; i < addressablesPrefabDatasList.Count; i++)
		{
			var item = addressablesPrefabDatasList[i];

			var angle = StaticFunctions.CalculateAngle(
				sceneReferences.folowCamera.transform,
				item.Target.position
			);

			angle = Mathf.Abs(angle);

			if (angle < MIN_INSTANTIATE_ANGLE)
				InstantiateAddressable(i);
			else if (angle > MAX_RELEASE_ANGLE)
				ReleaseAddressable(i);
		}
	}

	private void InstantiateAddressable(int index)
	{
		var data = addressablesPrefabDatasList[index];

		if (data.isInstantiated)
			return;

		data.isInstantiated = true;

		data
			.AssetReferenceGO.InstantiateAsync(data.Folder)
			.Completed += (h) =>
		{
			if (h.Status == AsyncOperationStatus.Succeeded)
				data.instance = h.Result;
			else
				data.isInstantiated = false;
		};
	}

	private void ReleaseAddressable(int index)
	{
		var data = addressablesPrefabDatasList[index];

		if (!data.isInstantiated)
			return;

		data.isInstantiated = !Addressables.ReleaseInstance(data.instance);
	}
}
