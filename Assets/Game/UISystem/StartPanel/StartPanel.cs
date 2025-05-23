using TMPro;
using UnityEngine;

public class StartPanel : AbstractPanel
{
	[SerializeField] private TMP_InputField inputField;

	public override void Init(object[] args)
	{
		gameObject.SetActive(true);
	}

	public void OnButtonClick()
	{
		if (inputField.text != "")
		{
			EventBus<PlayerNameEvnt>.Publish(new PlayerNameEvnt { name = inputField.text });
			gameObject.SetActive(false);
		}
	}

}
