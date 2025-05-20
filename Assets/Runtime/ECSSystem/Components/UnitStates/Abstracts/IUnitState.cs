using System;
using Unity.Entities;

public interface IUnitState : IDisposable
{
	bool IsStarted { get; }
	void StartState(Entity e);
	/// <summary>
	/// T - component who call finish
	/// </summary>
	/// <typeparam name="T"></typeparam>
	void OnFinish<T>();
}
