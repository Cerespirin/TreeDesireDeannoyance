using HarmonyLib;
using RimWorld;
using System;
using System.Reflection;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch]
	public static class HarmonyPatch_DesignatorReplant_MiniToInstallOrBuildingToReinstall
	{
		public static MethodBase TargetMethod()
		{
			return typeof(Designator_Install).GetProperty("MiniToInstallOrBuildingToReinstall", BindingFlags.NonPublic | BindingFlags.Instance).GetGetMethod(true);
		}

		public static bool Prefix(ref Thing __result, Gizmo __instance)
		{
			if (Current.Game.GetComponent<MyGameComponent>().designatorOwners.TryGetValue(__instance, out Thing owner))
			{
				__result = owner;
				return false;
			}
			return true;
		}
	}
}
