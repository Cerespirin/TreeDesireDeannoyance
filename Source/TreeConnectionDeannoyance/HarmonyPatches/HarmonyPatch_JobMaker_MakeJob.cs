using HarmonyLib;
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
			MyHelper.PossiblyChangeCutJobToHarvest(ref __result);
		}
	}
}
