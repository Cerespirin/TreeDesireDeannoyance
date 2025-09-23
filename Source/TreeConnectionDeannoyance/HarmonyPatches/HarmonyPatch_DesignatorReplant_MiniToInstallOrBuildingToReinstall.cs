using HarmonyLib;
using RimWorld;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(Designator_Install), "MiniToInstallOrBuildingToReinstall", MethodType.Getter)]
	public static class HarmonyPatch_DesignatorReplant_MiniToInstallOrBuildingToReinstall
	{
		public static bool Prefix(ref Thing __result, Gizmo __instance)
		{
			if (MyGameComponent.Cached.designatorOwners.TryGetValue(__instance, out Thing owner))
			{
				__result = owner;
				return false;
			}
			return true;
		}
	}
}
