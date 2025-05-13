using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
	[Inject] private readonly CharacterFactory characterFactory;

	private void Start()
	{
		characterFactory.Spawn(CharacterType.PirateBlackbeard, 1);
	}
}
