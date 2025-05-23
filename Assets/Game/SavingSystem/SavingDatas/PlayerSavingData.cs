public class PlayerSavingData : AbstractSavingData
{
	public string playerName;

	public override bool IsDataEmpty()
	{
		return playerName == null;
	}

	public override void ResetData(int flag = 0)
	{
		playerName = null;
	}

	public override void Init(AbstractSavingManager savingManager)
	{
		base.Init(savingManager);

		if (IsDataEmpty())
			EventBus<PlayerNameEvnt>.Subscribe(a => SetPlayerName(a.name));
	}

	protected override void SaveDataObject()
	{
		ES3.Save(ToString(), this);
	}

	private void SetPlayerName(string name)
	{
		playerName = name;
		SaveData(false);
	}
}
