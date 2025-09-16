using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

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
				/*
				if (ModsConfig.IdeologyActive)
				{
					yield return SpecialThingFilterDefOf.AllowVegetarian;
					yield return SpecialThingFilterDefOf.AllowCarnivore;
					yield return SpecialThingFilterDefOf.AllowCannibal;
					yield return SpecialThingFilterDefOf.AllowInsectMeat;
				}
				*/
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
			Zone_Replant autoCut = SelZone;
			Rect rect = new Rect(0f, 0f, WinSize.x, WinSize.y).ContractedBy(10f);
			Widgets.BeginGroup(rect);
			float num = 0f;
			this.DrawReplantFilter(ref num, rect.width, rect.height - num, autoCut);
			Widgets.EndGroup();
		}

		private void DrawReplantFilter(ref float curY, float width, float height, Zone_Replant zone)
		{
			Rect rect = new Rect(0f, curY, width, height);
			ThingFilter replantFilter = zone.ReplantFilter;
			ThingFilter fixedReplantFilter = zone.FixedReplantFilter;
			ThingFilterUI.DoThingFilterConfigWindow(rect, replantFilterState, replantFilter, fixedReplantFilter, 1, null, HiddenSpecialThingFilters);
		}





		private static readonly Vector2 WinSize = new Vector2(300f, 480f);
		private ThingFilterUI.UIState replantFilterState = new ThingFilterUI.UIState();
	}
}
