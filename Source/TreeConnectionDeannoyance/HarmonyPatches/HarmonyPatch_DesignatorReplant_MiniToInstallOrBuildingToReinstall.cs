using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch]
	public static class HarmonyPatch_DesignatorReplant_MiniToInstallOrBuildingToReinstall
	{
		public static IEnumerable<MethodInfo> TargetMethods()
		{
			yield return typeof(Designator_Replant).GetProperty("MiniToInstallOrBuildingToReinstall", BindingFlags.NonPublic | BindingFlags.Instance).GetGetMethod();
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
