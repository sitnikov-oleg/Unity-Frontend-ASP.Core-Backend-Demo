using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

class SharedObjectsAuthoring : MonoBehaviour
{
	public List<IDGameObjectData> idGameObjectDatas = new();
}

class SharedObjectsAuthoringBaker : Baker<SharedObjectsAuthoring>
{
	public override void Bake(SharedObjectsAuthoring authoring)
	{
		var entity = GetEntity(TransformUsageFlags.Dynamic);
		var list = authoring.idGameObjectDatas;
		
		if (list.Count > 0)
		{
			DynamicBuffer<IDGameObjectDataECS> buffer = AddBuffer<IDGameObjectDataECS>(entity);

			for (int i = 0; i < list.Count; i++)
			{
				FixedString32Bytes id = new FixedString32Bytes(list[i].id);
				var prefabEntity = GetEntity(list[i].gameObject, TransformUsageFlags.Dynamic);
				buffer.Add(new IDGameObjectDataECS { id = id, entity = prefabEntity });
			}
		}
	}
}
