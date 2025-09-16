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
		public ITab_Replant()
		{
			size = WinSize;
			labelKey = "TreeDesireDeannoyance_TabReplant";
		}

		public override void OnOpen()
		{
			base.OnOpen();
			//plantFilterState.quickSearch.Reset();
		}

		protected override void FillTab()
		{
			//CompAutoCut autoCut = this.AutoCut;
			Rect rect = new Rect(0f, 0f, WinSize.x, WinSize.y).ContractedBy(10f);
			Widgets.BeginGroup(rect);
			float num = 0f;
			this.DrawPlantFilter(ref num, rect.width, rect.height - num, autoCut);
			Widgets.EndGroup();
		}

		private void DrawPlantFilter(ref float curY, float width, float height, CompAutoCut autoCut)
		{
			Rect rect = new Rect(0f, curY, width, height);
			ThingFilterUI.UIState state = plantFilterState;
			ThingFilter autoCutFilter = autoCut.AutoCutFilter;
			ThingFilter fixedAutoCutFilter = autoCut.GetFixedAutoCutFilter();
			int openMask = 1;
			IEnumerable<ThingDef> forceHiddenDefs = null;
			Map map = autoCut.parent.Map;
			ThingFilterUI.DoThingFilterConfigWindow(rect, state, autoCutFilter, fixedAutoCutFilter, openMask, forceHiddenDefs, this.HiddenSpecialThingFilters(), true, false, false, null, map);
		}





		private static readonly Vector2 WinSize = new Vector2(300f, 480f);
		private ThingFilterUI.UIState plantFilterState = new ThingFilterUI.UIState();
	}
}
