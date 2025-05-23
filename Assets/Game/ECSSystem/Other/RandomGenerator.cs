using Unity.Entities;
using Unity.Mathematics;

public struct RandomGenerator : IComponentData
{
	public Random rnd;
	private const int RANDOM_INDEX = 10;

	public void Init()
	{
		rnd = Random.CreateFromIndex(RANDOM_INDEX);
	}
}
