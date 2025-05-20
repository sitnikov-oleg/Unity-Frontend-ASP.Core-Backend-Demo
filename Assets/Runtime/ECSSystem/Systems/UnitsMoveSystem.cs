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
		//var ecb = SystemAPI
		//	.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>()
		//	.CreateCommandBuffer(World.Unmanaged);
		////.AsParallelWriter();

		//if (!targetPoint.Equals(float3.zero))
		//{
		//	foreach (
		//		var (agent, idleStateComponent, entity) in SystemAPI
		//			.Query<RefRW<AgentBody>, RefRW<UnitIdleStateComponent>>()
		//			.WithEntityAccess()
		//	)
		//	{
		//		SetUnitState<UnitIdleStateComponent, UnitMovementStateComponent>(
		//			ecb,
		//			idleStateComponent.ValueRO,
		//			entity
		//		);
		//	}
		//}

		//var moveJob = new UnitWalkStateJob().ScheduleParallel(
		//	jobTrackerSystem.GetJob(typeof(UnitIdleStateJob))
		//);

		//jobTrackerSystem.AddJob(typeof(UnitWalkStateJob), moveJob);
		//moveJob.Complete();
	}
}
