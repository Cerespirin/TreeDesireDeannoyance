using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	internal static class MyHarmonyHelper
	{
		/*
		private static List<CodeInstruction> GenerateCode(CodeInstruction referenceToTargetTree, ILGenerator ilGenerator)
		{
			Label label = ilGenerator.DefineLabel();

			//new StackFrame(1).GetMethod().Name

			return new List<CodeInstruction>
			{
				// if (PlantUtility.TreeMarkedForExtraction(plant))
				// {
				referenceToTargetTree,
				new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PlantUtility), nameof(PlantUtility.TreeMarkedForExtraction))),
				new CodeInstruction(OpCodes.Brfalse_S, label),
				// return null;
				new CodeInstruction(OpCodes.Ldnull),
				new CodeInstruction(OpCodes.Ret),
				// }
				new CodeInstruction(OpCodes.Nop).WithLabels(label),
			};
		}

		internal static List<CodeInstruction> TranspilerHelper(CodeInstruction referenceToTargetTree, IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator, int targetIndex, int targetOffset = 0)
		{
			List<CodeInstruction> instructionsAsList = instructions.ToList();

			if (targetIndex < 0)
			{
				Log.Error($"[TreeDesireDeannoyance] {new StackFrame(1).GetMethod().Name}: Transpiler target field not found.");
			}
			else
			{
				instructionsAsList.InsertRange(targetIndex + targetOffset, MyHarmonyHelper.GenerateCode(referenceToTargetTree, ilGenerator));
			}
			return instructionsAsList;
		}
		*/
		internal static void PostfixHelper(ref Job job)
		{
			if (job == null || !Current.Game.GetComponent<MyGameComponent>().extractTreesAggressively) return;

			if (job.targetA.Thing?.def.plant?.IsTree ?? false)
			{
				if (job.def == JobDefOf.CutPlant)
				{
					Log.Message($"[TreeDesireDeannoyance] {new StackFrame(1).GetMethod().Name}: Postfix changed job def.");
					job.def = JobDefOf.ExtractTree;
					job.targetA.Thing.Map.designationManager.AddDesignation(new Designation(job.targetA.Thing, DesignationDefOf.ExtractTree));
				}
			}
		}
	}
}
