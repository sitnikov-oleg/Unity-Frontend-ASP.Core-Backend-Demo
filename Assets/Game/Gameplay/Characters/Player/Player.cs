using StarterAssets;
using UniRx;
using UnityEngine;

public class Player : AbstractUnit, IPlayerCameraTarget
{
	[SerializeField] private Transform cameraTarget;
	[SerializeField] private ThirdPersonController personController;
	private ReactiveProperty<PlayerRPData> PlayerData { get; set; } = new();

	public override void Init()
	{
		base.Init();
		SetPlayerRPData();
	}

	public Transform GetCameraTarget()
	{
		return cameraTarget;
	}

	public ThirdPersonController GetPersonController()
	{
		return personController;
	}

	protected override void Update()
	{
		base.Update();
		SetPlayerRPData();
	}

	private void SetPlayerRPData()
	{
		PlayerData.Value = new PlayerRPData { pos = transform.position, unit = this };
	}

	public override ReactiveProperty<T> GetReactiveProperty<T>()
	{
		if (typeof(T) == typeof(PlayerRPData))
			return (ReactiveProperty<T>)(object)PlayerData;
		return default;
	}
}
