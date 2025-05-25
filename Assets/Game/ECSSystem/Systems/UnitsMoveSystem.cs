using ProjectDawn.Navigation;
using Unity.Burst;
using Unity.Entities;

[BurstCompile]
[UpdateAfter(typeof(SpawnCitizensSystem))]
public partial class UnitsMoveSystem : UnitStatesSystem
{
	[BurstCompile]
	protected override void OnUpdate()
	{
		base.OnUpdate();

		foreach (
			var (agent, idleStateComponent, entity) in SystemAPI
				.Query<RefRW<AgentBody>, RefRW<UnitSpawnedStateComponent>>()
				.WithEntityAccess()
		)
		{
			SetUnitState<UnitSpawnedStateComponent, UnitMovementStateComponent>(
				Ecb,
				idleStateComponent.ValueRO,
				entity
			);
		}

		var moveJob = new UnitWalkStateJob()
		{
			singleton = navMeshQuerySystem,
			boundsComponent = bounds
		}.ScheduleParallel(jobTrackerSystem.GetJob(typeof(UnitSpawnedStateJob)));

		jobTrackerSystem.AddJob(typeof(UnitWalkStateJob), moveJob);
		moveJob.Complete();
	}
}
