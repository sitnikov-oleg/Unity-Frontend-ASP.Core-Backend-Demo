using UnityEngine;

public class UIManager : MonoBehaviour
{
	public AbstractPanel startPanel, usernamePanel;

	private void Awake()
	{
		EventBus<ShowStartPanelEvnt>.Subscribe(a => startPanel.Init(null));
	}


	private void Start()
	{
		usernamePanel.Init(null);
	}
}
