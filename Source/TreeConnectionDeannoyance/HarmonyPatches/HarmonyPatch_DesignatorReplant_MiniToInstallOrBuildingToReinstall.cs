using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch]
	public static class HarmonyPatch_DesignatorReplant_MiniToInstallOrBuildingToReinstall
	{
		public static IEnumerable<MethodInfo> TargetMethods()
		{
			yield return typeof(Designator_Replant).GetProperty("MiniToInstallOrBuildingToReinstall", BindingFlags.NonPublic | BindingFlags.Instance).GetGetMethod();
		}

		public static bool Prefix()
		{
			return true;
		}
	}
}
