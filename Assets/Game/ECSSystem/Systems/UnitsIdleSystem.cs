using Unity.Burst;
using Unity.Entities;

[BurstCompile]
[UpdateAfter(typeof(UnitsMoveSystem))]
public partial class UnitsIdleSystem : UnitStatesSystem
{
    [BurstCompile]
    protected override void OnUpdate()
    {
        foreach (
            var (component, entity) in SystemAPI
                .Query<RefRW<UnitMovementStateComponent>>()
                .WithPresent<UnitFinishMovementTag>()
                .WithEntityAccess()
        )
        {
            SetUnitState<UnitMovementStateComponent, UnitIdleStateComponent>(
                Ecb,
                new UnitMovementStateComponent(),
                entity
            );

            Ecb.RemoveComponent<UnitFinishMovementTag>(entity);
            Ecb.RemoveComponent<UnitMovementStateComponent>(entity);
        }

        var job = new UnitIdleStateJob()
        {
            deltaTime = World.MaximumDeltaTime,
            ecb = EcbParallel,
        }.ScheduleParallel(jobTrackerSystem.GetJob(typeof(UnitWalkStateJob)));

        jobTrackerSystem.AddJob(typeof(UnitIdleStateJob), job);
        job.Complete();
    }
}
