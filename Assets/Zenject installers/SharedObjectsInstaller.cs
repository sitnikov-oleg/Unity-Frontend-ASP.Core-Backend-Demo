using System.Collections.Generic;
using Zenject;

public class SharedObjectsInstaller : MonoInstaller
{
	public List<TypeGOPrefab> typeGameObjectDatasList;

	public override void InstallBindings()
	{
		Container.Bind<SharedObjects>().AsTransient().OnInstantiated<SharedObjects>(OnInstantiated);
	}

	private void OnInstantiated(InjectContext context, SharedObjects objects)
	{
		objects.SetTypeGameObjectDatasList(typeGameObjectDatasList);
	}
}