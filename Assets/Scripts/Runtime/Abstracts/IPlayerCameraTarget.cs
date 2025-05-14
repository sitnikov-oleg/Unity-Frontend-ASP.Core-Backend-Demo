using StarterAssets;
using UnityEngine;

public interface IPlayerCameraTarget
{
	Transform GetCameraTarget();
	ThirdPersonController GetPersonController();
}
