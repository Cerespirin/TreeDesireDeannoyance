using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	[HarmonyPatch(typeof(PlaySettings), nameof(PlaySettings.DoPlaySettingsGlobalControls))]
	public static class HarmonyPatch_PlaySettings_DoPlaySettingsGlobalControls
	{
		public static void Postfix(WidgetRow row, bool worldView)
		{
			if (!worldView)
			{
				row.ToggleableIcon(ref Current.Game.GetComponent<MyGameComponent>().alwaysExtractTrees, ContentFinder<Texture2D>.Get("UI/Designators/ExtractTree"), "TreeDesireDeannoyance_ToggleButton".Translate(), SoundDefOf.Mouseover_ButtonToggle);
			}
		}
	}
}
