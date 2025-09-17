using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(HaulAIUtility), "HaulablePlaceValidator")]
	public static class HarmonyPatch_HaulAIUtility_HaulablePlaceValidator
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> instructionsAsList = instructions.ToList();
			LocalBuilder extractSetting = generator.DeclareLocal(typeof(bool));
			object lastBranchLabel = null;

			foreach (CodeInstruction instruction in instructionsAsList)
			{
				if (instruction.opcode == OpCodes.Brfalse_S)
				{
					lastBranchLabel = instruction.operand;
				}
				if (instruction.opcode == OpCodes.Isinst && instruction.operand is Zone_Growing)
				{
					yield return new CodeInstruction(OpCodes.Stloc_S, extractSetting.LocalIndex);
					yield return instruction;
					yield return new CodeInstruction(OpCodes.Brfalse_S, lastBranchLabel);
					yield return new CodeInstruction(OpCodes.Ldloc_S, extractSetting.LocalIndex);
					yield return new CodeInstruction(OpCodes.Isinst, typeof(Zone_Replant));
				}
				else
				{
					yield return instruction;
				}
			}
		}
	}
}
