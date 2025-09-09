using HarmonyLib;
using RimWorld;
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
			MyGameComponent component = Current.Game.GetComponent<MyGameComponent>();

			if (component.designatorOwners.TryGetValue(__instance, out Thing owner))
			{
				if (owner.DestroyedOrNull())
				{
					Log.Warning("[TreeDesireDeannoyance] HarmonyPatch_DesignatorReplant_MiniToInstallOrBuildingToReinstall: designatorOwners had null or destroyed owner. Fixing.");
					component.designatorOwners.Remove(__instance);
					return true;
				}
				__result = owner;
				return false;
			}
			return true;
		}
	}
}
