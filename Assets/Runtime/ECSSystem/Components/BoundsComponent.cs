using Unity.Entities;
using Unity.Mathematics;

public struct BoundsComponent : IComponentData
{
	public float3 min, max, center, size;
}
