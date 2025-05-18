using System;

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

	protected override void SaveDataObject()
	{
		ES3.Save(ToString(), this);
	}

	internal void SetPlayerName(string name)
	{
		playerName = name;
		SaveData(false);
	}
}
