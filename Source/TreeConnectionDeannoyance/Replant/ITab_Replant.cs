using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class ITab_Replant : ITab
	{
		public Zone_Replant SelZone => SelObject as Zone_Replant;

		private IEnumerable<SpecialThingFilterDef> HiddenSpecialThingFilters
		{
			get
			{
				yield return SpecialThingFilterDefOf.AllowFresh;
				yield break;
			}
		}

		public ITab_Replant()
		{
			size = WinSize;
			labelKey = "TreeDesireDeannoyance_TabReplant";
		}

		public override void OnOpen()
		{
			base.OnOpen();
			replantFilterState.quickSearch.Reset();
		}

		protected override void FillTab()
		{
			Zone_Replant zone = SelZone;
			Rect rect = new Rect(0f, 0f, WinSize.x, WinSize.y).ContractedBy(10f);
			Widgets.BeginGroup(rect);
			float num = 0f;
			DrawReplantOptions(ref num, rect.width, zone);
			num += 4f;
			DrawReplantFilter(ref num, rect.width, rect.height - num, zone);
			Widgets.EndGroup();
		}

		private void DrawReplantOptions(ref float curY, float width, Zone_Replant zone)
		{
			Designator_ExtractTree designator = Find.ReverseDesignatorDatabase.Get<Designator_ExtractTree>();
			Rect position = new Rect(0f, curY, 24f, 24f);
			Rect rect = new Rect(position.xMax + 4f, curY, width, 24f);
			Rect rect2 = new Rect(0f, rect.yMax + 4f, 150f, 27f);
			GUI.DrawTexture(position, designator.icon);
			Text.Font = GameFont.Tiny;
			Widgets.CheckboxLabeled(rect, "TreeDesireDeannoyance_ZoneReplant_Enabled".Translate(), ref zone.enabled, false, null, null, true);
			Text.Font = GameFont.Small;
			if (Widgets.ButtonText(rect2, "TreeDesireDeannoyance_ZoneReplant_ReplantNow".Translate(), true, true, true, null))
			{
				//zone.DesignatePlantsToCut();
				SoundDef soundSucceeded = designator.soundSucceeded;
				soundSucceeded?.PlayOneShotOnCamera(null);
			}
			curY = rect2.yMax;
		}

		private void DrawReplantFilter(ref float curY, float width, float height, Zone_Replant zone)
		{
			Rect rect = new Rect(0f, curY, width, height);
			ThingFilter replantFilter = zone.ReplantFilter;
			ThingFilter fixedReplantFilter = zone.FixedReplantFilter;
			ThingFilterUI.DoThingFilterConfigWindow(rect, replantFilterState, replantFilter, fixedReplantFilter, 1, null, HiddenSpecialThingFilters);
		}





		private static readonly Vector2 WinSize = new Vector2(300f, 480f);
		#pragma warning disable IDE0044 // Add readonly modifier
		private ThingFilterUI.UIState replantFilterState = new ThingFilterUI.UIState();
		#pragma warning restore IDE0044 // Add readonly modifier
	}
}
