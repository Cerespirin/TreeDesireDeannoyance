using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(CompAutoCut), nameof(CompAutoCut.DesignatePlantsToCut))]
	public static class HarmonyPatch_CompAutoCut_DesignatePlantsToCut
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			bool didTheThing = false;

			foreach (CodeInstruction instruction in instructions)
			{
				if (instruction.opcode == OpCodes.Ldsfld && (FieldInfo)instruction.operand == AccessTools.Field(typeof(DesignationDefOf), nameof(DesignationDefOf.CutPlant)))
				{
					didTheThing = true;
					// Overwrite this instruction with our own.
					yield return new CodeInstruction(OpCodes.Ldloc_S, 4);
					yield return new CodeInstruction(OpCodes.Call, AccessTools.Property(typeof(MyGameComponent), nameof(MyGameComponent.Cached)).GetGetMethod());
					yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(MyGameComponent), nameof(MyGameComponent.alwaysExtractTrees)));
					yield return new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(HarmonyPatch_CompAutoCut_DesignatePlantsToCut), nameof(GetAppropriateDesignation)));
				}
				else
				{
					yield return instruction;
				}
			}
			if (!didTheThing)
			{
				Log.Error("[TreeDesireDeannoyance] HarmonyPatch_CompAutoCut_DesignatePlantsToCut: unable to find injection point. This was likely due to a mod incompatibility; please report this to the mod author.");
			}
		}

		public static DesignationDef GetAppropriateDesignation(Thing plant, bool alwaysExtract)
		{
			if (alwaysExtract && plant.IsRelevantToTreeLovers())
			{
				return DesignationDefOf.ExtractTree;
			}
			else
			{
				return DesignationDefOf.CutPlant;
			}
		}
	}
}
