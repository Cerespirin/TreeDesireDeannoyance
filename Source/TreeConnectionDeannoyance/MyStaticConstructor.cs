using HarmonyLib;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	[StaticConstructorOnStartup]
	public static class MyStaticConstructor
	{
		static MyStaticConstructor()
		{
			Harmony harmony = new Harmony("rimworld.cerespirin.treeconnectiondeannoyance");
			harmony.PatchAll();
		}
	}
}
