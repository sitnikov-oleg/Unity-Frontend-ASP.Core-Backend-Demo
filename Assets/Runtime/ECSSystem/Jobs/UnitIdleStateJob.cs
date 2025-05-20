using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial struct UnitIdleStateJob : IJobEntity
{
	[BurstCompile]
	public void Execute(RefRW<UnitIdleStateComponent> component, Entity e)
	{
		if (!component.ValueRO.IsStarted) component.ValueRW.StartState(e);
	}
}
