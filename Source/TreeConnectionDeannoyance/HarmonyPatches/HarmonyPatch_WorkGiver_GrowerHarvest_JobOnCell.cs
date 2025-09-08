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

				int toStart = __result.targetQueueA.Count;

				if (firstTarget.Thing.def.plant.IsTree)
				{
					// CalculateWantedPlantDef can return null!
					ThingDef wantedPlantDef = WorkGiver_Grower.CalculateWantedPlantDef(firstTarget.Cell, firstTarget.Thing.Map);
					IEnumerable<LocalTargetInfo> newQueue = __result.targetQueueA.Where(t => t.Thing.def.plant.IsTree);

					if (wantedPlantDef != null && firstTarget.Thing.def == wantedPlantDef)
					{
						newQueue = newQueue.Where(t => t.Thing.def == wantedPlantDef);
					}
					else
					{
						// This shouldn't need a null check unless there are things without defs, which I don't think exist.
						newQueue = newQueue.Where(t => t.Thing.def != wantedPlantDef);
						Log.Message("[TreeDesireDeannoyance] HarmonyPatch_WorkGiver_GrowerHarvest_JobOnCell: Postfix changed job def.");
						__result.def = JobDefOf.ExtractTree;
						
						foreach (LocalTargetInfo target in newQueue)
						{
							if (!target.Thing.Map.designationManager.HasMapDesignationOn(target.Thing))
							{
								target.Thing.Map.designationManager.AddDesignation(new Designation(target.Thing, DesignationDefOf.ExtractTree));
							}
						}
					}
					__result.targetQueueA = newQueue.ToList();
				}
				else
				{
					__result.targetQueueA = __result.targetQueueA.Where(t => !t.Thing.def.plant.IsTree).ToList();
				}
				Log.Message($"[TreeDesireDeannoyance] HarmonyPatch_WorkGiver_GrowerHarvest_JobOnCell: targetQueueA started witn {toStart} and ended with {__result.targetQueueA.Count}.");
			}
		}
	}
}
