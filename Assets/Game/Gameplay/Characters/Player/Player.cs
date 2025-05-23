using StarterAssets;
using UnityEngine;

public class Player : AbstractUnit, IPlayerCameraTarget
{
	[SerializeField] private Transform cameraTarget;
	[SerializeField] private ThirdPersonController personController;

	public Transform GetCameraTarget()
	{
		return cameraTarget;
	}

	public ThirdPersonController GetPersonController()
	{
		return personController;
	}

}
