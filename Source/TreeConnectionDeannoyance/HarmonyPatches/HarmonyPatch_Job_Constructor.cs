using HarmonyLib;
using System;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(Job), MethodType.Constructor, new Type[] { typeof(JobDef), typeof(Thing) })]
	public static class HarmonyPatch_Job_Constructor
	{
		public static void Postfix(ref Job __instance)
		{
			MyHelper.PossiblyChangeCutJobToHarvest(ref __instance);
		}
	}
}
