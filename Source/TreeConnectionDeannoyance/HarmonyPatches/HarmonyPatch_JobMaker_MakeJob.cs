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
				DesignationManager designationManager = __result.targetA.Thing.Map.designationManager;
				bool hasDesignation = designationManager.HasMapDesignationOn(__result.targetA.Thing);
				if (!hasDesignation || designationManager.DesignationOn(__result.targetA.Thing, DesignationDefOf.ExtractTree) != null)
				{
					__result.def = JobDefOf.ExtractTree;
					if (!hasDesignation)
					{
						designationManager.AddDesignation(new Designation(__result.targetA.Thing, DesignationDefOf.ExtractTree));
					}
				}
			}
		}
	}
}
