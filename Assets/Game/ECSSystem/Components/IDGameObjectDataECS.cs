using Unity.Collections;
using Unity.Entities;

public struct IDGameObjectDataECS : IBufferElementData
{
	public FixedString32Bytes id;
	public Entity entity;
}
