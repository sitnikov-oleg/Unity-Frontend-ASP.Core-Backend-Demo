using UnityEngine;

public class UIManager : MonoBehaviour
{
	public AbstractPanel startPanel;

	private void Awake()
	{
		EventBus<ShowStartPanelEvnt>.Subscribe(a => { startPanel.Init(null); });
	}

}
