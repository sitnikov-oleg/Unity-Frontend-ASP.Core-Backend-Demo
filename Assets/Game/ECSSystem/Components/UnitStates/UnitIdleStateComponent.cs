using Unity.Entities;
using Unity.Mathematics;

public struct UnitIdleStateComponent : IComponentData, IUnitState
{
	private const int IDLE_STATE_AMOUNT_MIN = 200;
	private const int IDLE_STATE_AMOUNT_MAX = 400;
	private bool started;
	private Entity entity;
	public Random rnd;
	private float idleStateAmount;
	private float counter;
	public bool IsStarted { get => started; }

	public void StartState(Entity e) { }

	public void StartState(Entity e, int index)
	{
		started = true;
		entity = e;

		if (rnd.state == 0)
			rnd = Random.CreateFromIndex((uint)index);

		idleStateAmount = rnd.NextFloat(IDLE_STATE_AMOUNT_MIN, IDLE_STATE_AMOUNT_MAX);
		counter = 0;
	}

	public bool TryIncreaseCounter(float deltaTime)
	{
		counter += deltaTime;
		return counter < idleStateAmount;
	}

	public void OnFinish<T>() { }
	public void Dispose() { }
}
