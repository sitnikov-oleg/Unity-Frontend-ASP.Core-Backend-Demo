using System.Linq;
using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial class SpawnCitizensSystem : CommonSystem
{
	private const string NPC_CITIZEN = "NPCCitizen";

	[BurstCompile]
	protected override void OnStartRunning()
	{
		base.OnStartRunning();
		var characterFactorySystem = World.GetExistingSystemManaged<CharacterFactorySystem>();
		var idGameObjectDatasBuffer = SystemAPI.GetSingletonBuffer<IDGameObjectDataECS>();
		var gamePreferences = SystemAPI.GetSingleton<GamePreferencesComponent>();

		var ecb = SystemAPI
			.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
			.CreateCommandBuffer(World.Unmanaged);

		var arr = idGameObjectDatasBuffer.AsNativeArray();
		var npces = arr.Where(a => a.id.Value.Contains(NPC_CITIZEN)).ToArray();

		for (int i = 0; i < gamePreferences.citizensAmount; i++)
		{
			var index = RandomGenerator.ValueRW.rnd.NextInt(0, npces.Length);
			var npc = npces[index];
			characterFactorySystem.CreateCharacters(ecb, 1, npc.id);
		}

		arr.Dispose();
		Enabled = false;
	}
}
