using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(WorkGiver_GrowerHarvest), nameof(WorkGiver_GrowerHarvest.JobOnCell))]
	public static class HarmonyPatch_WorkGiverGrowerHarvest_JobOnCell
	{
		public static void Postfix(ref Job __result, IntVec3 c)
		{
			if (__result == null || !Current.Game.GetComponent<MyGameComponent>().extractTreesAggressively) return;

			if (__result.targetA.Thing?.def.plant?.IsTree ?? false)
			{
				if (__result.def == JobDefOf.Harvest && __result.targetA.Thing.def != WorkGiver_Grower.CalculateWantedPlantDef(c, __result.targetA.Thing.Map))
				{
					Log.Message("[TreeDesireDeannoyance] HarmonyPatch_WorkGiverGrowerHarvest_JobOnCell: Postfix changed job def.");
					__result.def = JobDefOf.ExtractTree;
					__result.targetA.Thing.Map.designationManager.AddDesignation(new Designation(__result.targetA.Thing, DesignationDefOf.ExtractTree));
				}
			}
		}
	}
}
