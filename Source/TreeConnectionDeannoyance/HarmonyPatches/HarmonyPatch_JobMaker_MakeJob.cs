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
		public static void Postfix(ref Job __result)
		{
			if (!Current.Game.GetComponent<MyGameComponent>().alwaysExtractTrees) return;

			if (__result.def == JobDefOf.CutPlant && __result.targetA.Thing.IsRelevantToTreeLovers())
			{
				if (!__result.targetA.Thing.Map.designationManager.HasMapDesignationOn(__result.targetA.Thing))
				{
					__result.def = JobDefOf.ExtractTree;
					__result.targetA.Thing.Map.designationManager.AddDesignation(new Designation(__result.targetA.Thing, DesignationDefOf.ExtractTree));
				}
			}
		}
	}
}
