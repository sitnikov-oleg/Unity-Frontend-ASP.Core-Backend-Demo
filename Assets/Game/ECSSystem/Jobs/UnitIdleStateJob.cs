using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial struct UnitIdleStateJob : IJobEntity
{
	public float deltaTime;
	public EntityCommandBuffer.ParallelWriter ecb;

	[BurstCompile]
	public void Execute(
		RefRW<UnitIdleStateComponent> component,
		Entity e,
		[ChunkIndexInQuery] int index
	)
	{
		if (!component.ValueRO.IsStarted)
			component.ValueRW.StartState(e, index);

		if(!component.ValueRW.TryIncreaseCounter(deltaTime))
			ecb.AddComponent<UnitFinishIdleTag>(index, e);
	}
}
