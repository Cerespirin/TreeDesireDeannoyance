using HarmonyLib;
using RimWorld;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(GenConstruct), nameof(GenConstruct.HandleBlockingThingJob))]
	public static class HarmonyPatch_GenConstruct_HandleBlockingThingJob
	{
		public static void Postfix(ref Job __result)
		{
			MyHarmonyHelper.PostfixHelper(ref __result);
		}
	}
}
