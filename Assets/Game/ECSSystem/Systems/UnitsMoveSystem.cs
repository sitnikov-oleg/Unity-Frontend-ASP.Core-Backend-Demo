using ProjectDawn.Navigation;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

[BurstCompile]
[UpdateAfter(typeof(SpawnCitizensSystem))]
public partial class UnitsMoveSystem : UnitStatesSystem
{
	[BurstCompile]
	protected override void OnUpdate()
	{
		base.OnUpdate();

		var query = SystemAPI
			.QueryBuilder()
			.WithPresentRW<AgentBody>()
			.WithAnyRW<UnitSpawnedStateComponent, UnitFinishIdleTag>()
			.Build();

		var entities = query.ToEntityArray(Allocator.TempJob);
		var ecb = new EntityCommandBuffer(Allocator.TempJob);

		var setWalkJob = Job.WithCode(() =>
			{
				foreach (var e in entities)
				{
					if (SystemAPI.HasComponent<UnitSpawnedStateComponent>(e))
					{
						var spawnComponent = SystemAPI.GetComponent<UnitSpawnedStateComponent>(e);
						spawnComponent.OnFinish<UnitMovementStateComponent>();
						spawnComponent.Dispose();
						ecb.RemoveComponent<UnitSpawnedStateComponent>(e);
						ecb.AddComponent<UnitMovementStateComponent>(e);
					}

					if (SystemAPI.HasComponent<UnitFinishIdleTag>(e))
					{
						var idleComponent = SystemAPI.GetComponent<UnitIdleStateComponent>(e);
						idleComponent.OnFinish<UnitMovementStateComponent>();
						idleComponent.Dispose();
						ecb.RemoveComponent<UnitIdleStateComponent>(e);
						ecb.RemoveComponent<UnitFinishIdleTag>(e);
						ecb.AddComponent<UnitMovementStateComponent>(e);
					}
				}
			})
			.Schedule(jobTrackerSystem.GetJob(typeof(UnitSpawnedStateJob)));

		setWalkJob.Complete();
		ecb.Playback(EntityManager);
		ecb.Dispose();
		entities.Dispose();

		var combinedJobs = JobHandle.CombineDependencies(
			setWalkJob,
			jobTrackerSystem.GetJob(typeof(UnitIdleStateJob))
		);

		var moveJob = new UnitWalkStateJob()
		{
			singleton = navMeshQuerySystem,
			boundsComponent = bounds,
			ecb = EcbParallel,
		}.ScheduleParallel(combinedJobs);

		jobTrackerSystem.AddJob(typeof(UnitWalkStateJob), moveJob);
		moveJob.Complete();
	}
}
