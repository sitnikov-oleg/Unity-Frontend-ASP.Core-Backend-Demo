using System.Linq;
using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
	[Inject] private readonly CharacterFactory characterFactory;
	[Inject] private readonly SceneReferences sceneReferences;

	private void Start()
	{
		var list = characterFactory.Spawn(CharacterType.PirateBlackbeard, 1);
		sceneReferences.mainCamera.gameObject.SetActive(false);
		sceneReferences.folowCamera.gameObject.SetActive(true);
		var iplayer = (IPlayerCameraTarget)list.First();
		sceneReferences.cinemachineVirtualCamera.Follow = iplayer.GetCameraTarget();
		iplayer.GetPersonController().UpdateCamera(sceneReferences.folowCamera.gameObject);
	}
}
