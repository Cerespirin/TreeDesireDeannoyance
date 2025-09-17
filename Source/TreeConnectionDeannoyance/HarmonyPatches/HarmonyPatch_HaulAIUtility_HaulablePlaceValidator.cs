using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(HaulAIUtility), "HaulablePlaceValidator")]
	public static class HarmonyPatch_HaulAIUtility_HaulablePlaceValidator
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			LocalBuilder extractSetting = generator.DeclareLocal(typeof(bool));
			object lastBranchLabel = null;
			bool didTheThing = false;

			foreach (CodeInstruction instruction in instructions)
			{
				if (instruction.opcode == OpCodes.Brfalse_S)
				{
					lastBranchLabel = instruction.operand;
				}
				if (instruction.opcode == OpCodes.Isinst && (Type)instruction.operand == typeof(Zone_Growing))
				{
					didTheThing = true;
					yield return new CodeInstruction(OpCodes.Stloc_S, extractSetting.LocalIndex);
					yield return instruction;
					yield return new CodeInstruction(OpCodes.Brfalse_S, lastBranchLabel);
					yield return new CodeInstruction(OpCodes.Ldloc_S, extractSetting.LocalIndex);
					yield return new CodeInstruction(OpCodes.Isinst, typeof(Zone_Replant));
					continue;
				}
				else
				{
					yield return instruction;
				}
			}
			if (!didTheThing)
			{
				Log.Error("[TreeDesireDeannoyance] HarmonyPatch_HaulAIUtility_HaulablePlaceValidator: unable to find injection point. This was likely due to a mod incompatibility; please report this to the mod author.");
			}
		}
	}
}
