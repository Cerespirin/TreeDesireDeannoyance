using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(CompAutoCut), nameof(CompAutoCut.DesignatePlantsToCut))]
	public static class HarmonyPatch_CompAutoCut_DesignatePlantsToCut
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			/* So what I am trying to do here is change
			 * 
			 *		map.designationManager.AddDesignation(new Designation(plant, DesignationDefOf.CutPlant, null));
			 *		
			 *	into
			 *	
			 *		if (plant.def.plant.treeLoversCareIfChopped)
			 *		{
			 *			map.designationManager.AddDesignation(new Designation(plant, DesignationDefOf.ExtractTree, null));
			 *		}
			 *		else
			 *		{
			 *			map.designationManager.AddDesignation(new Designation(plant, DesignationDefOf.CutPlant, null));
			 *		}
			*/
			List<CodeInstruction> instructionsAsList = instructions.ToList();
			CodeInstruction targetStart = null;
			CodeInstruction targetEnd = null;
			Label afterLabel = generator.DefineLabel();

			foreach (CodeInstruction instruction in instructionsAsList)
			{
				if (instruction.opcode == OpCodes.Ldloc_0)
				{
					targetStart = instruction;
				}
				else if (instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == AccessTools.Method(typeof(DesignationManager), nameof(DesignationManager.AddDesignation)))
				{
					targetEnd = instruction;
					break;
				}
			}

			if (targetStart == null || targetEnd == null) 
			{
				Log.Error("[TreeDesireDeannoyance] HarmonyPatch_CompAutoCut_DesignatePlantsToCut: failed to find injection points.");
				return instructions;
			}

			List<CodeInstruction> newInstructions = new List<CodeInstruction>() 
			{
				// if (plant.def.plant.treeLoversCareIfChopped) {
				new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(Thing), nameof(Thing.def))),
				new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(ThingDef), nameof(ThingDef.plant))),
				new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(PlantProperties), nameof(PlantProperties.treeLoversCareIfChopped))),
				new CodeInstruction(OpCodes.Brfalse_S, targetStart.labels),
				// map.designationManager.AddDesignation(new Designation(plant, DesignationDefOf.ExtractTree, null));
				new CodeInstruction(OpCodes.Ldloc_0),
				new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(Map), nameof(Map.designationManager))),
				new CodeInstruction(OpCodes.Ldloc_S, 4),
				new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(LocalTargetInfo), "op_Implicit", new Type[] { typeof(Thing) })),
				new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(DesignationDefOf), nameof(DesignationDefOf.ExtractTree))),
				new CodeInstruction(OpCodes.Ldnull),
				new CodeInstruction(OpCodes.Newobj, typeof(Designation)),
				new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(DesignationManager), nameof(DesignationManager.AddDesignation))),
				// }
				new CodeInstruction(OpCodes.Br_S, afterLabel)
				// else
			};

			instructionsAsList.InsertRange(instructionsAsList.IndexOf(targetEnd) + 1, new List<CodeInstruction> { new CodeInstruction(OpCodes.Nop).WithLabels(afterLabel) });
			instructionsAsList.InsertRange(instructionsAsList.IndexOf(targetStart), newInstructions);

			return instructionsAsList;
		}
	}
}
