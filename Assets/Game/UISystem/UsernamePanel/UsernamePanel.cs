using TMPro;
using Zenject;

public class UsernamePanel : AbstractPanel
{
	[Inject]
	AbstractSavingManager savingManager;
	public TMP_Text usernameText;

	public override void Init(object[] args)
	{
		var psd = savingManager.GetSavingData<PlayerSavingData>(SavingDataType.Player);

		if (psd.IsDataEmpty())
			EventBus<PlayerNameEvnt>.Subscribe(a => SetUsername(a.name));
		else
			SetUsername(psd.playerName);

	}

	private void SetUsername(string username)
	{
		gameObject.SetActive(true);
		usernameText.text = username;
	}
}
