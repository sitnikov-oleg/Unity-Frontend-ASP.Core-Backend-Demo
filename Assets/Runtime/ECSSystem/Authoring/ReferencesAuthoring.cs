using Unity.Entities;
using UnityEngine;

class ReferencesAuthoring : MonoBehaviour
{
	public BoxCollider spawnArea;
}

class ReferencesAuthoringBaker : Baker<ReferencesAuthoring>
{
	public override void Bake(ReferencesAuthoring authoring)
	{
		var entity = GetEntity(TransformUsageFlags.Dynamic);
		AddComponent(entity, new ReferencesTag());

		if (authoring.spawnArea)
		{
			AddComponent(entity, new BoundsComponent
			{
				center = authoring.spawnArea.bounds.center,
				min = authoring.spawnArea.bounds.min,
				max = authoring.spawnArea.bounds.max,
				size = authoring.spawnArea.bounds.size
			});
		}

	}
}
