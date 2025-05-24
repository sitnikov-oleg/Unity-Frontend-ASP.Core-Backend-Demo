using ProjectDawn.Navigation;
using Rukhanka;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct UnitWalkStateJob : IJobEntity
{
	[ReadOnly]
	public NavMeshQuerySystem.Singleton singleton;
	public BoundsComponent boundsComponent;

	[BurstCompile]
	public void Execute(
		RefRW<UnitMovementStateComponent> component,
		Entity e,
		RefRW<AgentBody> agent,
		AnimatorParametersAspect aspect,
		RefRO<NavMeshPath> navMeshPath,
		RefRO<LocalToWorld> transform,
		[EntityIndexInChunk] int index
	)
	{
		if (!component.ValueRO.IsStarted)
		{
			component.ValueRW.StartState(e, index);
		}

		if (component.ValueRO.targetPos.Equals(float3.zero))
		{
			var pos = component.ValueRW.rnd.NextFloat3(boundsComponent.min, boundsComponent.max);

			var location = singleton.MapLocation(
				pos,
				navMeshPath.ValueRO.MappingExtent,
				navMeshPath.ValueRO.AgentTypeId,
				navMeshPath.ValueRO.AreaMask
			);

			component.ValueRW.targetPos = location.position;
		}
		else if (
			!component.ValueRO.targetPos.Equals(float3.zero)
			&& agent.ValueRO.Velocity.GetMagnitude() > 0
			&& agent.ValueRO.RemainingDistance < 0.5f
		)
		{
			component.ValueRW.targetPos = float3.zero;
		}
		else
		{
			agent.ValueRW.SetDestination(component.ValueRO.targetPos);
		}

		var walk = agent.ValueRO.Velocity.GetMagnitude() > 0;
		component.ValueRW.SetAnimation(aspect, walk);
	}
}
