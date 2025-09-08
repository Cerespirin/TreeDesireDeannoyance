using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(WorkGiver_GrowerHarvest), nameof(WorkGiver_GrowerHarvest.JobOnCell))]
	public static class HarmonyPatch_WorkGiver_GrowerHarvest_JobOnCell
	{
		public static void Postfix(ref Job __result)
		{
			if (!Current.Game.GetComponent<MyGameComponent>().alwaysExtractTrees) return;

			if (__result.def == JobDefOf.Harvest)
			{
				// GetTargetQueue cannot return null, but can return an empty list!
				LocalTargetInfo firstTarget = __result.targetQueueA.FirstOrFallback(null);

				if (firstTarget == null) return;

				if (firstTarget.Thing.def.plant.IsTree)
				{
					// CalculateWantedPlantDef can return null!
					ThingDef wantedPlantDef = WorkGiver_Grower.CalculateWantedPlantDef(firstTarget.Cell, firstTarget.Thing.Map);

					if (wantedPlantDef != null && firstTarget.Thing.def == wantedPlantDef)
					{
						__result.targetQueueA = __result.targetQueueA.Where(t => t.Thing.def == wantedPlantDef).ToList();
					}
					else
					{
						Log.Message("[TreeDesireDeannoyance] HarmonyPatch_WorkGiver_GrowerHarvest_JobOnCell: Postfix changed job def.");
						__result.def = JobDefOf.ExtractTree;
						__result.targetA = firstTarget;
						__result.targetQueueA = null;

						if (!firstTarget.Thing.Map.designationManager.HasMapDesignationOn(firstTarget.Thing))
						{
							firstTarget.Thing.Map.designationManager.AddDesignation(new Designation(firstTarget.Thing, DesignationDefOf.ExtractTree));
						}
					}
				}
				else
				{
					__result.targetQueueA = __result.targetQueueA.Where(t => !t.Thing.def.plant.IsTree).ToList();
				}
			}
		}
	}
}
