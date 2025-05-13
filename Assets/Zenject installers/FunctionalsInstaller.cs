using Zenject;

public class FunctionalsInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<CharacterFactory>().AsTransient();
	}
}