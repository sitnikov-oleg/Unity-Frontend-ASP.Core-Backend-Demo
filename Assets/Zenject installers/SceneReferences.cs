using UnityEngine;
using Zenject;

public class SceneReferences : MonoInstaller
{
	[SerializeField] private Collider charactersSpawnArea;
	[SerializeField] private Transform charactersFolder;

	public Collider CharactersSpawnArea { get => charactersSpawnArea; }
	public Transform CharactersFolder { get => charactersFolder; }

	public override void InstallBindings()
	{
		Container.Bind<SceneReferences>().AsSingle();
	}
}