using Unity.Entities;
using Unity.Mathematics;

public struct UnitSpawnedStateComponent : IComponentData, IUnitState
{
	public float3 spawnPos;
	public bool IsStarted { get; private set; }

	public void Dispose()
	{
	}

	public void OnFinish<T>()
	{
	}

	public void StartState(Entity e)
	{
		IsStarted = true;
	}
}
