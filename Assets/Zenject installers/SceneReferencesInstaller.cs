using Cinemachine;
using UnityEngine;
using UnityStandardAssets.Cameras;
using Zenject;

public class SceneReferencesInstaller : MonoInstaller
{
	public Collider charactersSpawnArea;
	public Transform charactersFolder;
	public AutoCam followCamera;
	public Camera mainCamera;

	public override void InstallBindings()
	{
		Container.Bind<SceneReferences>().AsTransient().OnInstantiated<SceneReferences>(OnInstantiated);
	}

	private void OnInstantiated(InjectContext context, SceneReferences references)
	{
		references.charactersFolder = charactersFolder;
		references.charactersSpawnArea = charactersSpawnArea;
		references.followCamera = followCamera;
		references.mainCamera = mainCamera;
	}
}