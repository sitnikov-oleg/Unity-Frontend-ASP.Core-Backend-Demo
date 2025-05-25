using ProjectDawn.Navigation;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct UnitSpawnedStateJob : IJobEntity
{
	[ReadOnly]
	public NavMeshQuerySystem.Singleton singleton;
	public BoundsComponent boundsComponent;
	public EntityCommandBuffer.ParallelWriter ecb;

	[BurstCompile]
	public void Execute(
		Entity entity,
		RefRW<UnitSpawnedStateComponent> component,
		RefRO<NavMeshPath> navMeshPath,
		[EntityIndexInChunk] int sortKey
	)
	{
		if (component.ValueRO.IsStarted)
			return;
		else component.ValueRW.StartState(entity);

		var location = singleton.MapLocation(
			component.ValueRO.spawnPos,
			navMeshPath.ValueRO.MappingExtent,
			navMeshPath.ValueRO.AgentTypeId,
			navMeshPath.ValueRO.AreaMask
		);

		var pos = location.position;

		if (pos.Equals(float3.zero))
		{
			location = singleton.MapLocation(
				boundsComponent.center,
				navMeshPath.ValueRO.MappingExtent,
				navMeshPath.ValueRO.AgentTypeId,
				navMeshPath.ValueRO.AreaMask
			);

			pos = location.position;
		}

		ecb.SetComponent(sortKey, entity, LocalTransform.FromPosition(pos));
	}
}
