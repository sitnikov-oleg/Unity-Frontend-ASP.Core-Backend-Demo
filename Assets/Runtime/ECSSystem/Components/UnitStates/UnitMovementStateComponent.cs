using Rukhanka;
using Unity.Entities;

public struct UnitMovementStateComponent : IComponentData, IUnitState
{
	private Entity entity;
	private bool started;
	public bool IsStarted { get => started; }
	private FastAnimatorParameter run;

	public void StartState(Entity e) { }

	public void StartState(Entity e, AnimatorParametersAspect aspect)
	{
		entity = e;
		started = true;
		run = new FastAnimatorParameter("Run");
	}

	public void SetAnimation(AnimatorParametersAspect aspect, bool value)
	{
		aspect.SetParameterValue(run, value);
	}

	public void OnFinish<T>() { }
	public void Dispose() { }
}
