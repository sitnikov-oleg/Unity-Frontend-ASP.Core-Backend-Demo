using Rukhanka;
using Unity.Entities;
using Unity.Mathematics;

public struct UnitMovementStateComponent : IComponentData, IUnitState
{
	private Entity entity;
	private bool started;
	public bool IsStarted { get => started; }
	private FastAnimatorParameter walk;
	private FastAnimatorParameter speed;
	public float3 targetPos;
	public Random rnd;
	public bool IsTargetReached { get; set; }

	public void StartState(Entity e, int index)
	{
		entity = e;
		started = true;
		walk = new FastAnimatorParameter("Walk");
		speed = new FastAnimatorParameter("Speed");
		rnd = Random.CreateFromIndex((uint)index);
	}

	public void SetAnimation(AnimatorParametersAspect aspect, bool value)
	{
		aspect.SetParameterValue(walk, value);
	}

	public void SetSpeed(AnimatorParametersAspect aspect, float value)
	{
		aspect.SetParameterValue(speed, value);
	}

	public void OnFinish<T>() { }
	public void Dispose() { }

	public void StartState(Entity e)
	{
	}
}
