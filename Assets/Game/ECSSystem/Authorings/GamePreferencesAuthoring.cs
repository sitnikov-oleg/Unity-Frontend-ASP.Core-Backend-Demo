using Unity.Entities;
using UnityEngine;

class GamePreferencesAuthoring : MonoBehaviour
{
	public int citizensAmount;
}

class GamePreferencesAuthoringBaker : Baker<GamePreferencesAuthoring>
{
	public override void Bake(GamePreferencesAuthoring authoring)
	{
		var entity = GetEntity(TransformUsageFlags.Dynamic);
		AddComponent(entity, new GamePreferencesComponent { citizensAmount = authoring.citizensAmount });
	}
}
