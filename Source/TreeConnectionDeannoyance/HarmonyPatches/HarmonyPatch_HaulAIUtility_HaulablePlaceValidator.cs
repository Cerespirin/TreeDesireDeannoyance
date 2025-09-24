using Cerespirin.TreeDesireDeannoyance.ZoneReplant;
using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
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
			LocalBuilder newLocal = generator.DeclareLocal(typeof(Zone));
			Label newLabel = generator.DefineLabel();
			byte state = 0;

			foreach (CodeInstruction instruction in instructions)
			{
				if (instruction.opcode == OpCodes.Isinst && (Type)instruction.operand == typeof(Zone_Growing))
				{
					state = 1;
					yield return new CodeInstruction(OpCodes.Stloc_S, newLocal.LocalIndex);
					yield return new CodeInstruction(OpCodes.Ldloc_S, newLocal.LocalIndex);
					yield return instruction;
				}
				else if (state == 1 && instruction.opcode == OpCodes.Brfalse_S)
				{
					state = 2;
					yield return new CodeInstruction(OpCodes.Brtrue_S, newLabel);
					yield return new CodeInstruction(OpCodes.Ldloc_S, newLocal.LocalIndex);
					yield return new CodeInstruction(OpCodes.Isinst, typeof(Zone_Replant));
					yield return instruction;
					yield return new CodeInstruction(OpCodes.Nop).WithLabels(newLabel);
				}
				else
				{
					yield return instruction;
				}
			}
			if (state < 2)
			{
				Log.Error("[TreeDesireDeannoyance] HarmonyPatch_HaulAIUtility_HaulablePlaceValidator: unable to find injection point. This was likely due to a mod incompatibility; please report this to the mod author.");
			}
		}
	}
}
