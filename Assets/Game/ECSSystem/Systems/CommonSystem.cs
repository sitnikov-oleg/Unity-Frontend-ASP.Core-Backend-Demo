using Unity.Entities;

public partial class CommonSystem : SystemBase
{
	protected RefRW<RandomGenerator> RandomGenerator => SystemAPI.GetSingletonRW<RandomGenerator>();

	protected override void OnUpdate()
	{
	}
}
