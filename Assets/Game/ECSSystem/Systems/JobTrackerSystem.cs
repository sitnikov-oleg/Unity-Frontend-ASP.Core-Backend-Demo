using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;

[BurstCompile]
[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public partial class JobTrackerSystem : SystemBase
{
	private Dictionary<Type, JobHandle> activeJobs = new();

	[BurstCompile]
	protected override void OnUpdate()
	{
		UpdateActiveJobs();
	}

	[BurstCompile]
	public void AddJob(Type type, JobHandle job)
	{
		activeJobs.Add(type, job);
	}

	[BurstCompile]
	public void UpdateActiveJobs()
	{
		List<Type> list = new();

		foreach (var item in activeJobs)
		{
			if (item.Value.IsCompleted)
			{
				list.Add(item.Key);
			}
		}

		for (int i = 0; i < list.Count; i++)
		{
			activeJobs.Remove(list[i]);
		}
	}

	[BurstCompile]
	public JobHandle GetJob(Type type)
	{
		return activeJobs[type];
	}
}
