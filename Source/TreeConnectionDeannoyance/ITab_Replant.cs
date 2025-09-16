using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class ITab_Replant : ITab
	{
		public Zone_AutoReplant SelZone => SelObject as Zone_AutoReplant;

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
			Zone_AutoReplant autoCut = SelZone;
			Rect rect = new Rect(0f, 0f, WinSize.x, WinSize.y).ContractedBy(10f);
			Widgets.BeginGroup(rect);
			float num = 0f;
			this.DrawReplantFilter(ref num, rect.width, rect.height - num, autoCut);
			Widgets.EndGroup();
		}

		private void DrawReplantFilter(ref float curY, float width, float height, Zone_AutoReplant zone)
		{
			Rect rect = new Rect(0f, curY, width, height);
			ThingFilter autoCutFilter = zone.ReplantFilter;
			ThingFilter fixedAutoCutFilter = zone.FixedReplantFilter;
			ThingFilterUI.DoThingFilterConfigWindow(rect, replantFilterState, autoCutFilter, fixedAutoCutFilter, 1, null, this.HiddenSpecialThingFilters());
		}





		private static readonly Vector2 WinSize = new Vector2(300f, 480f);
		private ThingFilterUI.UIState replantFilterState = new ThingFilterUI.UIState();
	}
}
