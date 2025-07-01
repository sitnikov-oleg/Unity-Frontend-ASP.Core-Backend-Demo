using Unity.Entities;

public partial class CommonSystem : SystemBase
{
	protected RefRW<RandomGenerator> RandomGenerator => SystemAPI.GetSingletonRW<RandomGenerator>();
	protected EntityCommandBuffer.ParallelWriter EcbParallel =>
		SystemAPI
			.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
			.CreateCommandBuffer(World.Unmanaged).AsParallelWriter();

	protected EntityCommandBuffer Ecb =>
		SystemAPI
			.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
			.CreateCommandBuffer(World.Unmanaged);

	protected override void OnUpdate()
	{
	}
}
