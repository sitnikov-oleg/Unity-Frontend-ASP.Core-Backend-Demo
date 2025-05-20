using ProjectDawn.Navigation;
using Rukhanka;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

[BurstCompile]
public partial struct UnitWalkStateJob : IJobEntity
{
	private float3 targetPos;

	[BurstCompile]
	public void Execute(
		RefRW<UnitMovementStateComponent> component,
		Entity e,
		RefRW<AgentBody> agent,
		AnimatorParametersAspect aspect,
		NavMeshQuerySystem.Singleton singleton,
		BoundsComponent boundsComponent,
		NavMeshPath navMeshPath
	)
	{
		if (!component.ValueRO.IsStarted)
		{
			component.ValueRW.StartState(e, aspect);
		}

		if (targetPos.Equals(float3.zero))
		{
			var location = singleton.MapLocation(
				RandomGenerator.GenerateFloat3(boundsComponent.min, boundsComponent.max),
				navMeshPath.MappingExtent,
				navMeshPath.AgentTypeId,
				navMeshPath.AreaMask
			);

			targetPos = location.position;
		}
		else if (!targetPos.Equals(float3.zero) && agent.ValueRO.RemainingDistance < 0.5f)
			targetPos = float3.zero;
		else
		{
			agent.ValueRW.SetDestination(targetPos);
			var zeroSpeed = agent.ValueRO.Velocity.Equals(float3.zero);
			component.ValueRW.SetAnimation(aspect, !zeroSpeed);
		}
	}
}
