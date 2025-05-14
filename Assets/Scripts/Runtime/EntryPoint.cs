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
		sceneReferences.followCamera.gameObject.SetActive(true);
		sceneReferences.followCamera.FindAndTargetPlayer();
	}
}
