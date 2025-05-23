using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
	[Inject] private readonly AbstractSavingManager savingManager;
	[Inject] private readonly DiContainer diContainer;

	private void Start()
	{
		LoadSavings();

		if (savingManager.IsSavingDatasEmpty())
		{
			EventBus<PlayerNameEvnt>.Subscribe(a => PrepareForGame());
			EventBus<ShowStartPanelEvnt>.Publish(new ShowStartPanelEvnt());
		}
		else
			PrepareForGame();
	}

	private void LoadSavings()
	{
		savingManager.LoadData();
	}

	private void PrepareForGame()
	{
		var preparer = new GamePreparer();
		diContainer.Inject(preparer);
		preparer.PrepareGame();
	}

	private void OnDestroy()
	{
		EventBus<PlayerNameEvnt>.Dispose();
	}
}
