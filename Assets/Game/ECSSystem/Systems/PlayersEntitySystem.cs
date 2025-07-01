using System;
using System.Collections.Generic;
using UniRx;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
public partial class PlayersEntitySystem : CommonSystem
{
	private Dictionary<Guid, PlayerECSModel> pairs = new();
	private bool playersCreated;
	private const string PLAYER_ECS_PREFAB = "PlayerECS";

	[BurstCompile]
	protected override void OnCreate()
	{
		base.OnCreate();
		EventBus<PlayerReadyEvnt>.Subscribe(OnPlayerReadyEvnt);
		RequireForUpdate<IDGameObjectDataECS>();
	}

	[BurstCompile]
	private void OnPlayerReadyEvnt(PlayerReadyEvnt evnt)
	{
		pairs.Add(evnt.unit.GetID(), new PlayerECSModel { pos = evnt.unit.transform.position, unit = evnt.unit });
		var rp = evnt.unit.GetReactiveProperty<PlayerRPData>();
		rp.Subscribe(a => SetPlayerECSData(a));

		if (playersCreated)
			CreatePlayerEntity(evnt.unit);
	}

	[BurstCompile]
	protected override void OnUpdate()
	{
		base.OnUpdate();

		if (!playersCreated)
		{
			playersCreated = true;

			foreach (var pair in pairs)
			{
				CreatePlayerEntity(pair.Value.unit);
			}
		}

		SetPositionsToEntities();
	}

	[BurstCompile]
	private void CreatePlayerEntity(AbstractUnit unit)
	{
		var characterFactory = World.GetExistingSystemManaged<CharacterFactorySystem>();
		var e = characterFactory.CreatePlayer(Ecb, PLAYER_ECS_PREFAB, unit.transform.position, unit.GetID());
		pairs[unit.GetID()].entity = e;
	}

	[BurstCompile]
	private void SetPlayerECSData(PlayerRPData data)
	{
		pairs[data.unit.GetID()].pos = data.pos;
	}

	[BurstCompile]
	private void SetPositionsToEntities()
	{
		foreach (var (component, entity) in SystemAPI.Query<RefRW<PlayerECSComponent>>().WithEntityAccess())
		{
			var pos = pairs[component.ValueRO.id].pos;
			Ecb.SetComponent(entity, LocalTransform.FromPosition(pos));
		}
	}
}
