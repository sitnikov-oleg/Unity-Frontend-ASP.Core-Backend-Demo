using ProjectDawn.Navigation;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

[BurstCompile]
public partial class UnitsMoveSystem : UnitStatesSystem
{
	private float3 targetPoint;

	protected override void OnCreate()
	{
		base.OnCreate();
		//EventBus.Subscribe<AgentECSDestinationPointEvnt>(OnAgentECSDestinationPointEvnt);
	}

	//private void OnAgentECSDestinationPointEvnt(AgentECSDestinationPointEvnt evnt)
	//{
	//	targetPoint = evnt.point;
	//}

	[BurstCompile]
	protected override void OnUpdate()
	{
		base.OnUpdate();
		var ecb = SystemAPI
			.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
			.CreateCommandBuffer(World.Unmanaged);

		var navMeshQuerySystem = SystemAPI.GetSingleton<NavMeshQuerySystem.Singleton>();
		var boundsEntity = SystemAPI.GetSingletonEntity<CitizensBoundsTag>();
		var bounds = SystemAPI.GetComponent<BoundsComponent>(boundsEntity);

		foreach (
			var (agent, idleStateComponent, entity) in SystemAPI
				.Query<RefRW<AgentBody>, RefRW<UnitIdleStateComponent>>()
				.WithEntityAccess()
		)
		{
			SetUnitState<UnitIdleStateComponent, UnitMovementStateComponent>(
				ecb,
				idleStateComponent.ValueRO,
				entity
			);
		}

		var moveJob = new UnitWalkStateJob()
		{
			singleton = navMeshQuerySystem,
			randomIndex = RandomGenerator.ValueRW.rnd.NextUInt(),
			boundsComponent = bounds
		}.ScheduleParallel(jobTrackerSystem.GetJob(typeof(UnitIdleStateJob)));

		jobTrackerSystem.AddJob(typeof(UnitWalkStateJob), moveJob);
		moveJob.Complete();
	}
}
