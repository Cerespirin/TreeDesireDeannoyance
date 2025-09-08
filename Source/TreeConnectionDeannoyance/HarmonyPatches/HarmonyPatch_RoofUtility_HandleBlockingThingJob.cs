using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(RoofUtility), nameof(RoofUtility.HandleBlockingThingJob))]
	public static class HarmonyPatch_RoofUtility_HandleBlockingThingJob
	{
		/*
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator)
		{
			return MyHarmonyHelper.TranspilerHelper(new CodeInstruction(OpCodes.Ldarg_0), instructions, ilGenerator, 0, 0);
		}
		*/
		public static void Postfix(ref Job __result)
		{
			MyHarmonyHelper.PostfixHelper(ref __result);
		}
	}
}
