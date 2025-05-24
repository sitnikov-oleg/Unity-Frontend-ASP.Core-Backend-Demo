using ProjectDawn.Navigation;
using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial class UnitStatesSystem : CommonSystem
{
	protected JobTrackerSystem jobTrackerSystem;
	protected NavMeshQuerySystem.Singleton navMeshQuerySystem;
	protected BoundsComponent bounds;

	protected EntityCommandBuffer.ParallelWriter EcbParallel =>
		SystemAPI
			.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
			.CreateCommandBuffer(World.Unmanaged).AsParallelWriter();

	protected EntityCommandBuffer Ecb =>
		SystemAPI
			.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
			.CreateCommandBuffer(World.Unmanaged);

	/// <summary>
	/// O - old unit state component, N - new unit state component
	/// </summary>
	/// <typeparam name="O"></typeparam>
	/// <typeparam name="N"></typeparam>
	/// <param name="ecb"></param>
	/// <param name="component"></param>
	/// <param name="e"></param>
	[BurstCompile]
	public virtual void SetUnitState<O, N>(EntityCommandBuffer ecb, O component, Entity e)
		where O : unmanaged, IComponentData, IUnitState
		where N : unmanaged, IComponentData, IUnitState
	{
		component.OnFinish<N>();
		component.Dispose();
		ecb.RemoveComponent<O>(e);
		ecb.AddComponent<N>(e);
	}

	protected override void OnStartRunning()
	{
		base.OnStartRunning();
		jobTrackerSystem = World.GetExistingSystemManaged<JobTrackerSystem>();
		navMeshQuerySystem = SystemAPI.GetSingleton<NavMeshQuerySystem.Singleton>();
		var boundsEntity = SystemAPI.GetSingletonEntity<CitizensBoundsTag>();
		bounds = SystemAPI.GetComponent<BoundsComponent>(boundsEntity);
	}

	[BurstCompile]
	protected override void OnUpdate()
	{
		var job = new UnitSpawnedStateJob()
		{
			boundsComponent = bounds,
			ecb = EcbParallel,
			singleton = navMeshQuerySystem
		}.ScheduleParallel(this.Dependency);

		jobTrackerSystem.AddJob(typeof(UnitSpawnedStateJob), job);
		job.Complete();
	}
}
