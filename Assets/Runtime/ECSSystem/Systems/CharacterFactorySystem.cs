using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial class CharacterFactorySystem : SystemBase
{
	[BurstCompile]
	public void CreateCharacters(EntityCommandBuffer ecb, int count, FixedString32Bytes id)
	{
		Entity prefab = default;
		var buffer = SystemAPI.GetSingletonBuffer<IDGameObjectDataECS>();
		var refEntity = SystemAPI.GetSingletonEntity<ReferencesTag>();
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
			var pos = RandomGenerator.GenerateFloat3(bounds.min, bounds.max);
			//var pos = new float3(bounds.center.x, 3, bounds.center.z);
			ecb.SetComponent(newUnitEntity, LocalTransform.FromPosition(pos));
			ecb.AddComponent(newUnitEntity, new UnitIdleStateComponent());
		}
	}

	protected override void OnUpdate()
	{
	}
}
