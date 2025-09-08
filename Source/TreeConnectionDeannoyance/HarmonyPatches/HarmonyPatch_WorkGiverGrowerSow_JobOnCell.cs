using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(WorkGiver_GrowerSow), nameof(WorkGiver_GrowerSow.JobOnCell))]
	public static class HarmonyPatch_WorkGiverGrowerSow_JobOnCell
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator)
		{
			FieldInfo targetField = typeof(PlantProperties).GetField(nameof(PlantProperties.blockAdjacentSow));
			int targetIndex = instructions.FirstIndexOf(i => (i.operand as FieldInfo) == targetField);

			return MyHarmonyHelper.TranspilerHelper(new CodeInstruction(OpCodes.Ldloc_S, 4), instructions, ilGenerator, targetIndex, 2);
		}

		public static void Postfix(ref Job __result)
		{
			MyHarmonyHelper.PostfixHelper(ref __result);
		}
	}
}
