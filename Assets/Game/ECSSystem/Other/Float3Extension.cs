using Unity.Mathematics;

public static class Float3Extension
{
	public static float GetMagnitude(this float3 float3)
	{
		return math.sqrt(float3.x * float3.x + float3.y * float3.y + float3.z * float3.z);
	}
}
