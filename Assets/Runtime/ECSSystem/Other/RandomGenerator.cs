using Unity.Mathematics;

public static class RandomGenerator
{
	private static Random rnd;
	private const int RANDOM_INDEX = 5;

	static RandomGenerator()
	{
		rnd = Random.CreateFromIndex(RANDOM_INDEX);
	}

	public static int GenerateInt(int min, int max)
	{
		return rnd.NextInt(min, max);
	}

	public static float GenerateFloat(float min, float max)
	{
		return rnd.NextFloat(min, max);
	}

	public static float3 GenerateFloat3(float3 min, float3 max)
	{
		return rnd.NextFloat3(min, max);
	}
}
