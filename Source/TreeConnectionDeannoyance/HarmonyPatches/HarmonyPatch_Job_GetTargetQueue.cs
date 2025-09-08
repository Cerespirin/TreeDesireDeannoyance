using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(Job), nameof(Job.GetTargetQueue))]
	public static class HarmonyPatch_Job_GetTargetQueue
	{
		public static void Postfix(ref Job __instance)
		{
			if (!Current.Game.GetComponent<MyGameComponent>().extractTreesAggressively) return;

			if (__instance.def == JobDefOf.Harvest)
			{
				// GetTargetQueue cannot return null, but can return an empty list!
				LocalTargetInfo firstTarget = __instance.GetTargetQueue(TargetIndex.A).FirstOrFallback(null);

				if (firstTarget == null) return;

				if (firstTarget.Thing.def.plant.IsTree)
				{
					// CalculateWantedPlantDef can return null!
					ThingDef wantedPlantDef = WorkGiver_Grower.CalculateWantedPlantDef(firstTarget.Cell, firstTarget.Thing.Map);
					IEnumerable<LocalTargetInfo> newQueue = __instance.GetTargetQueue(TargetIndex.A).Where(t => t.Thing.def.plant.IsTree);

					if (wantedPlantDef != null && firstTarget.Thing.def == wantedPlantDef)
					{
						newQueue = newQueue.Where(t => t.Thing.def == wantedPlantDef);
					}
					else
					{
						// This shouldn't need a null check unless there are things without defs, which I don't think exist.
						newQueue = newQueue.Where(t => t.Thing.def != wantedPlantDef);
						Log.Message("[TreeDesireDeannoyance] HarmonyPatch_Job_GetTargetQueue: Postfix changed job def.");
						__instance.def = JobDefOf.ExtractTree;
						
						foreach (LocalTargetInfo target in newQueue)
						{
							target.Thing.Map.designationManager.AddDesignation(new Designation(target.Thing, DesignationDefOf.ExtractTree));
						}
					}
					__instance.targetQueueA = newQueue.ToList();
				}
				else
				{
					__instance.targetQueueA = __instance.targetQueueA.Where(t => !t.Thing.def.plant.IsTree).ToList();
				}
			}
		}
	}
}
