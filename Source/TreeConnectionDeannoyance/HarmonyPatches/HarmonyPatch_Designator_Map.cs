using HarmonyLib;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(Designator), nameof(Designator.Map), MethodType.Getter)]
	public static class HarmonyPatch_Designator_Map
	{
		public static bool Prefix(ref Map __result, Gizmo __instance)
		{
			MyGameComponent component = Current.Game.GetComponent<MyGameComponent>();

			if (component.designatorOwners.TryGetValue(__instance, out Thing owner))
			{
				__result = owner.Map;
				return false;
			}
			return true;
		}
	}
}
