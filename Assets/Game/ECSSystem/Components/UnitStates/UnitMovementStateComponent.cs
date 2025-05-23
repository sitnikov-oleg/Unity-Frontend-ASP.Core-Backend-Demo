using Rukhanka;
using Unity.Entities;
using Unity.Mathematics;

public struct UnitMovementStateComponent : IComponentData, IUnitState
{
	private Entity entity;
	private bool started;
	public bool IsStarted { get => started; }
	private FastAnimatorParameter walk;
	public float3 targetPos;

	public void StartState(Entity e)
	{
		entity = e;
		started = true;
		walk = new FastAnimatorParameter("Walk");
	}

	public void SetAnimation(AnimatorParametersAspect aspect, bool value)
	{
		aspect.SetParameterValue(walk, value);
	}

	public void OnFinish<T>() { }
	public void Dispose() { }
}
