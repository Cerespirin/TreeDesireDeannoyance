using HarmonyLib;
using RimWorld;
using System.Linq;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(WorkGiver_GrowerHarvest), nameof(WorkGiver_GrowerHarvest.JobOnCell))]
	public static class HarmonyPatch_WorkGiverGrowerHarvest_JobOnCell
	{
		public static void Postfix(ref Job __result)
		{
			if (!Current.Game.GetComponent<MyGameComponent>().alwaysExtractTrees) return;

			if (__result.def == JobDefOf.Harvest)
			{
				// GetTargetQueue cannot return null, but can return an empty list!
				LocalTargetInfo firstTarget = __result.targetQueueA.FirstOrFallback(null);

				if (firstTarget == null) return;

				if (firstTarget.Thing.IsRelevantToTreeLovers())
				{
					if (((Plant)firstTarget.Thing).DeliberatelyCultivated())
					{
						__result.targetQueueA = __result.targetQueueA.Where(t => t.Thing.def == firstTarget.Thing.def || !t.Thing.IsRelevantToTreeLovers()).ToList();
					}
					else
					{
						if (!firstTarget.Thing.Map.designationManager.HasMapDesignationOn(firstTarget.Thing))
						{
							__result.def = JobDefOf.ExtractTree;
							__result.targetA = firstTarget;
							__result.targetQueueA = null;

							firstTarget.Thing.Map.designationManager.AddDesignation(new Designation(firstTarget.Thing, DesignationDefOf.ExtractTree));
						}
					}
				}
				else
				{
					__result.targetQueueA = __result.targetQueueA.Where(t => !t.Thing.IsRelevantToTreeLovers()).ToList();
				}
			}
		}
	}
}
