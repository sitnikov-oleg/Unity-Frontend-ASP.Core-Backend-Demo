using Unity.Entities;

public struct UnitIdleStateComponent : IComponentData, IUnitState
{
	private Entity entity;
	private bool started;
	public bool IsStarted { get => started; }


	public void StartState(Entity e)
	{
		entity = e;
		started = true;
	}

	public void OnFinish<T>() { }
	public void Dispose() { }
}
