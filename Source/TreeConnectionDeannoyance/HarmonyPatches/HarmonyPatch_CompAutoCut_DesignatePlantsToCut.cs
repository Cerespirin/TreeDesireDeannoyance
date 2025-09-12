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
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			/* So what I am trying to do here is change
			 *
			 *		map.designationManager.AddDesignation(new Designation(plant, DesignationDefOf.CutPlant, null));
			 *
			 *	into
			 *
			 *		bool alwaysExtract = MyHelper.GetExtractSetting();
			 *		(...)
			 *		map.designationManager.AddDesignation(new Designation(plant, GetAppropriateDesignation(plant, alwaysExtract), null));
			*/
			List<CodeInstruction> instructionsAsList = instructions.ToList();

			foreach (CodeInstruction instruction in instructionsAsList)
			{
				if (instruction.opcode == OpCodes.Stloc_0)
				{
					yield return instruction;
					yield return new CodeInstruction(OpCodes.Callvirt, (MethodInfo)instruction.operand == AccessTools.Method(typeof(MyHelper), nameof(MyHelper.GetExtractSetting)));
					yield return new CodeInstruction(OpCodes.Stloc_S, 130);
				}
				if (instruction.opcode == OpCodes.Ldsfld && (Type)instruction.operand == AccessTools.Field(typeof(DesignationDefOf), nameof(DesignationDefOf.CutPlant)))
				{
					// Overwrite this instruction with our own.
					yield return new CodeInstruction(OpCodes.Ldloc_S, 4);
					yield return new CodeInstruction(OpCodes.Ldloc_S, 130);
					yield return new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(HarmonyPatch_CompAutoCut_DesignatePlantsToCut), nameof(GetAppropriateDesignation)));
				}
			}

			/*
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
			*/
		}

		public static DesignationDef GetAppropriateDesignation(Thing plant, bool alwaysExtract)
		{
			if (alwaysExtract && plant.def.plant.treeLoversCareIfChopped)
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
