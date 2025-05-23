using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial class UnitStatesSystem : CommonSystem
{
	protected JobTrackerSystem jobTrackerSystem;

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

	protected override void OnCreate()
	{
		base.OnCreate();
		jobTrackerSystem = World.GetExistingSystemManaged<JobTrackerSystem>();
	}

	[BurstCompile]
	protected override void OnUpdate()
	{
		var job = new UnitIdleStateJob().ScheduleParallel(this.Dependency);
		jobTrackerSystem.AddJob(typeof(UnitIdleStateJob), job);
		job.Complete();
	}
}
