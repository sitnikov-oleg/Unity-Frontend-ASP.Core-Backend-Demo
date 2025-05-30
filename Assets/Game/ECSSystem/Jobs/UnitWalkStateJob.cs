using ProjectDawn.Navigation;
using Rukhanka;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct UnitWalkStateJob : IJobEntity
{
	private const float MIN_SPEED = 1f;
	private const float MAX_SPEED = 2.2f;
	private const int MAX_CHECK_MOVE_VALUE = 20;
	private const int IDLE_VALUE = 10;
	private const int RANDOM_INCREMENT = 10;
	private const float MIN_REMAINING_DISTANCE = 0.5f;
	[ReadOnly]
	public NavMeshQuerySystem.Singleton singleton;
	public BoundsComponent boundsComponent;
	public EntityCommandBuffer.ParallelWriter ecb;

	[BurstCompile]
	public void Execute(
		RefRW<UnitMovementStateComponent> component,
		Entity e,
		RefRW<AgentBody> agent,
		RefRW<AgentSteering> agentSteering,
		AnimatorParametersAspect aspect,
		RefRO<NavMeshPath> navMeshPath,
		RefRO<LocalToWorld> transform,
		[EntityIndexInChunk] int index
	)
	{
		if (component.ValueRO.IsTargetReached)
			return;

		if (!component.ValueRO.IsStarted)
		{
			component.ValueRW.StartState(e, index + RANDOM_INCREMENT);
			var rndValue = component.ValueRW.rnd.NextFloat(MIN_SPEED, MAX_SPEED);
			agentSteering.ValueRW.Speed = rndValue;
			component.ValueRW.SetSpeed(aspect, rndValue);
		}

		if (component.ValueRO.targetPos.Equals(float3.zero))
		{
			var posX = component.ValueRW.rnd.NextFloat(
				boundsComponent.min.x,
				boundsComponent.max.x
			);
			var posZ = component.ValueRW.rnd.NextFloat(
				boundsComponent.min.z,
				boundsComponent.max.z
			);
			var pos = new float3(posX, boundsComponent.center.y, posZ);

			var location = singleton.MapLocation(
				pos,
				navMeshPath.ValueRO.MappingExtent,
				navMeshPath.ValueRO.AgentTypeId,
				navMeshPath.ValueRO.AreaMask
			);

			component.ValueRW.targetPos = location.position;

			if (location.position != Vector3.zero)
				agent.ValueRW.SetDestination(component.ValueRO.targetPos);
		}
		else if (
			!component.ValueRO.targetPos.Equals(float3.zero)
			&& agent.ValueRO.Velocity.GetMagnitude() > 0
			&& agent.ValueRO.RemainingDistance < MIN_REMAINING_DISTANCE
		)
		{
			CheckMoveNext(component, agent, aspect, index, e);
		}
		else if (
			!component.ValueRO.targetPos.Equals(float3.zero)
			&& agent.ValueRO.Velocity.GetMagnitude() == 0
			&& agent.ValueRO.RemainingDistance > MIN_REMAINING_DISTANCE
		)
		{
			CheckMoveNext(component, agent, aspect, index, e);
		}
		else
		{
			agent.ValueRW.SetDestination(component.ValueRO.targetPos);
		}

		var walk = agent.ValueRO.Velocity.GetMagnitude() > 0;
		component.ValueRW.SetAnimation(aspect, walk);
	}

	[BurstCompile]
	private void CheckMoveNext(
		RefRW<UnitMovementStateComponent> component,
		RefRW<AgentBody> agent,
		AnimatorParametersAspect aspect,
		int index,
		Entity e)
	{
		var rndValue = component.ValueRW.rnd.NextInt(0, MAX_CHECK_MOVE_VALUE);

		if (rndValue != IDLE_VALUE)
			component.ValueRW.targetPos = float3.zero;
		else
		{
			component.ValueRW.IsTargetReached = true;
			component.ValueRW.SetAnimation(aspect, false);
			agent.ValueRW.Stop();
			ecb.AddComponent<UnitFinishMovementTag>(index, e);
		}
	}
}
