using System.Linq;
using Zenject;

public class GamePreparer
{
	[Inject] private readonly CharacterFactory characterFactory;
	[Inject] private readonly SceneReferences sceneReferences;

	public void PrepareGame()
	{
		var list = characterFactory.Spawn(CharacterType.PirateBlackbeard, 1);
		sceneReferences.mainCamera.gameObject.SetActive(false);
		sceneReferences.folowCamera.gameObject.SetActive(true);
		var abstractUnit = list.First();
		var playerCameraTarget = (IPlayerCameraTarget)abstractUnit;
		sceneReferences.cinemachineVirtualCamera.Follow = playerCameraTarget.GetCameraTarget();
		playerCameraTarget.GetPersonController().UpdateCamera(sceneReferences.folowCamera.gameObject);
		EventBus<PlayerReadyEvnt>.Publish(new PlayerReadyEvnt { unit = abstractUnit });
	}
}
