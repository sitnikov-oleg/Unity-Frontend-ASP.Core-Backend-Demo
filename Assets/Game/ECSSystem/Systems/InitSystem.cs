using Unity.Entities;

public partial class InitSystem : SystemBase
{
	protected override void OnCreate()
	{
		base.OnCreate();
		CreateRandomGenerator();
		Enabled = false;
	}

	private void CreateRandomGenerator()
	{
		var randomGenerator = new RandomGenerator();
		randomGenerator.Init();
		EntityManager.CreateSingleton(randomGenerator, "RandomGenerator");
	}

	protected override void OnUpdate()
	{
	}
}
