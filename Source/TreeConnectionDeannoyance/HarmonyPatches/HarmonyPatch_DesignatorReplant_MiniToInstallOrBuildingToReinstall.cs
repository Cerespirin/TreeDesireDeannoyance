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
			Type type = typeof(Designator_Install);
			PropertyInfo prop = type.GetProperty("MiniToInstallOrBuildingToReinstall", BindingFlags.NonPublic | BindingFlags.Instance);
			MethodInfo meth = prop.GetGetMethod();

			Log.Message($"type: {type}, prop: {prop}, meth: {meth}.");

			return meth;
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
