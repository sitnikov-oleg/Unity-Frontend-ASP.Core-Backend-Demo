using System.Linq;
using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial class SpawnCitizensSystem : SystemBase
{
	private const string NPC_CITIZEN = "NPCCitizen";
	private const int NPC_CITIZEN_AMOUNT = 10;

	[BurstCompile]
	protected override void OnStartRunning()
	{
		base.OnStartRunning();
		var characterFactorySystem = World.GetExistingSystemManaged<CharacterFactorySystem>();
		var idGameObjectDatasBuffer = SystemAPI.GetSingletonBuffer<IDGameObjectDataECS>();

		var ecb = SystemAPI
			.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
			.CreateCommandBuffer(World.Unmanaged);

		var arr = idGameObjectDatasBuffer.AsNativeArray();
		var npces = arr.Where(a => a.id.Value.Contains(NPC_CITIZEN)).ToArray();

		for (int i = 0; i < NPC_CITIZEN_AMOUNT; i++)
		{
			var npc = npces[RandomGenerator.GenerateInt(0, npces.Length)];
			characterFactorySystem.CreateCharacters(ecb, 1, npc.id);
		}

		arr.Dispose();
	}

	protected override void OnUpdate()
	{
		Enabled = false;
	}
}
