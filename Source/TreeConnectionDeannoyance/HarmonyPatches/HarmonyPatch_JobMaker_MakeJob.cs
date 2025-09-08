using HarmonyLib;
using RimWorld;
using System;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(JobMaker), nameof(JobMaker.MakeJob), new Type[] { typeof(JobDef), typeof(LocalTargetInfo) })]
	public static class HarmonyPatch_JobMaker_MakeJob 
	{
		public static void Postfix(ref Job __result, IntVec3 targetA)
		{
			if (!Current.Game.GetComponent<MyGameComponent>().extractTreesAggressively) return;

			if (__result.def == JobDefOf.CutPlant)
			{
				if (__result?.targetA.Thing?.def.plant?.IsTree ?? false)
				{
					Log.Message("[TreeDesireDeannoyance] HarmonyPatch_JobMaker_MakeJob: Postfix changed job def.");
					__result.def = JobDefOf.ExtractTree;
					__result.targetA.Thing.Map.designationManager.AddDesignation(new Designation(__result.targetA.Thing, DesignationDefOf.ExtractTree));
				}
			}
		}
	}
}
