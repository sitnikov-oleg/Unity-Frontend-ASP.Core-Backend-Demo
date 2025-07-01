using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial class CharacterFactorySystem : CommonSystem
{
	[BurstCompile]
	public void CreateCharacters(EntityCommandBuffer ecb, int count, FixedString32Bytes id)
	{
		Entity prefab = default;
		var buffer = SystemAPI.GetSingletonBuffer<IDGameObjectDataECS>();
		var refEntity = SystemAPI.GetSingletonEntity<CitizensBoundsTag>();
		var bounds = EntityManager.GetComponentData<BoundsComponent>(refEntity);


		foreach (var item in buffer)
		{
			if (item.id.Equals(id))
			{
				prefab = item.entity;
				break;
			}
		}

		for (int i = 0; i < count; i++)
		{
			var newUnitEntity = ecb.Instantiate(prefab);
			var pos = RandomGenerator.ValueRW.rnd.NextFloat3(bounds.min, bounds.max);
			ecb.SetComponent(newUnitEntity, LocalTransform.FromPosition(pos));
			ecb.AddComponent(newUnitEntity, new UnitSpawnedStateComponent() { spawnPos = pos });
		}
	}

	[BurstCompile]
	public Entity CreatePlayer(EntityCommandBuffer ecb, FixedString32Bytes prefabId, float3 pos, Guid guidId)
	{
		Entity prefab = default;
		var buffer = SystemAPI.GetSingletonBuffer<IDGameObjectDataECS>();

		foreach (var item in buffer)
		{
			if (item.id.Equals(prefabId))
			{
				prefab = item.entity;
				break;
			}
		}

		var newUnitEntity = ecb.Instantiate(prefab);
		ecb.SetComponent(newUnitEntity, LocalTransform.FromPosition(pos));
		ecb.AddComponent(newUnitEntity, new PlayerECSComponent { id = guidId });
		return newUnitEntity;
	}
}
