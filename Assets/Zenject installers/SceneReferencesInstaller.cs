using Cinemachine;
using UnityEngine;
using Zenject;

public class SceneReferencesInstaller : MonoInstaller
{
	public Collider charactersSpawnArea;
	public Transform charactersFolder;
	public CinemachineVirtualCamera cinemachineVirtualCamera;
	public Camera mainCamera, folowCamera;

	public override void InstallBindings()
	{
		Container.Bind<SceneReferences>().AsTransient().OnInstantiated<SceneReferences>(OnInstantiated);
	}

	private void OnInstantiated(InjectContext context, SceneReferences references)
	{
		references.charactersFolder = charactersFolder;
		references.charactersSpawnArea = charactersSpawnArea;
		references.cinemachineVirtualCamera = cinemachineVirtualCamera;
		references.mainCamera = mainCamera;
		references.folowCamera = folowCamera;
	}
}