using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Noise;
using Verse.Sound;

namespace Cerespirin.TreeDesireDeannoyance.AreaForage
{
	public class Dialog_ForageSettings : Window
	{
		public override void DoWindowContents(Rect inRect)
		{
			throw new NotImplementedException();
		}
		/*
		private void NoIdeaButCopiedFromITabStorage()
		{
			IStoreSettingsParent storeSettingsParent = this.SelStoreSettingsParent;
			StorageSettings settings = storeSettingsParent.GetStoreSettings();
			Rect rect = new Rect(0f, 0f, WinSize.x, WinSize.y).ContractedBy(10f);
			Widgets.BeginGroup(rect);
			ThingFilter parentFilter = null;
			if (storeSettingsParent.GetParentStoreSettings() != null)
			{
				parentFilter = storeSettingsParent.GetParentStoreSettings().filter;
			}
			Rect rect3 = new Rect(0f, this.TopAreaHeight, rect.width, rect.height - this.TopAreaHeight);
			ThingFilterUI.DoThingFilterConfigWindow(rect3, this.thingFilterState, settings.filter, parentFilter, 8, null, this.HiddenSpecialThingFilters(), false, false, false, null, null);
			Widgets.EndGroup();
		}
		*/
		private static void NoIdeaButCopiedFromThingFilterUI(Rect rect, ThingFilterUI.UIState state)
		{
			Widgets.DrawMenuSection(rect);
			float num = rect.width - 2f;
			Rect rect2 = new Rect(rect.x + 3f, rect.y + 3f, num / 2f - 3f - 1.5f, 24f);
			if (Widgets.ButtonText(rect2, "ClearAll".Translate(), true, true, true, null))
			{
				filter.SetDisallowAll(forceHiddenDefs, forceHiddenFilters);
				SoundDefOf.Checkbox_TurnedOff.PlayOneShotOnCamera(null);
			}
			if (Widgets.ButtonText(new Rect(rect2.xMax + 3f, rect2.y, rect2.width, 24f), "AllowAll".Translate(), true, true, true, null))
			{
				filter.SetAllowAll(parentFilter, false);
				SoundDefOf.Checkbox_TurnedOn.PlayOneShotOnCamera(null);
			}
			rect.yMin = rect2.yMax;
			Rect rect3 = new Rect(rect.x + 3f, rect.y + 3f, rect.width - 16f - 6f, 24f);
			state.quickSearch.OnGUI(rect3, null, null);
			rect.yMin = rect3.yMax + 3f;
			TreeNode_ThingCategory node = filter.RootNode;
			bool flag = true;
			bool flag2 = true;
			if (parentFilter != null)
			{
				node = parentFilter.DisplayRootCategory;
				flag = parentFilter.allowedHitPointsConfigurable;
				flag2 = parentFilter.allowedQualitiesConfigurable;
			}
			rect.xMax -= 4f;
			rect.yMax -= 6f;
			Rect viewRect = new Rect(0f, 0f, rect.width - 16f, ThingFilterUI.viewHeight);
			Rect visibleRect = new Rect(0f, 0f, rect.width, rect.height);
			visibleRect.position += state.scrollPosition;
			Widgets.BeginScrollView(rect, ref state.scrollPosition, viewRect, true);
			float num2 = 2f;
			if (flag && !forceHideHitPointsConfig)
			{
				ThingFilterUI.DrawHitPointsFilterConfig(ref num2, viewRect.width, filter);
			}
			if (flag2 && !forceHideQualityConfig)
			{
				ThingFilterUI.DrawQualityFilterConfig(ref num2, viewRect.width, filter);
			}
			if (ModsConfig.AnomalyActive && showMentalBreakChanceRange)
			{
				ThingFilterUI.DrawMentalBreakFilterConfig(ref num2, viewRect.width, filter);
			}
			float num3 = num2;
			Rect rect4 = new Rect(0f, num2, viewRect.width, 9999f);
			visibleRect.position -= rect4.position;
			Listing_TreeThingFilter listing_TreeThingFilter = new Listing_TreeThingFilter(filter, parentFilter, forceHiddenDefs, forceHiddenFilters, suppressSmallVolumeTags, state.quickSearch.filter);
			listing_TreeThingFilter.Begin(rect4);
			listing_TreeThingFilter.ListCategoryChildren(node, openMask, map, visibleRect);
			listing_TreeThingFilter.End();
			state.quickSearch.noResultsMatched = (listing_TreeThingFilter.matchCount == 0);
			if (Event.current.type == EventType.Layout)
			{
				ThingFilterUI.viewHeight = num3 + listing_TreeThingFilter.CurHeight + 90f;
			}
			Widgets.EndScrollView();
		}

		//private ThingFilterUI.UIState thingFilterState = new ThingFilterUI.UIState();
		//private static readonly Vector2 WinSize = new Vector2(300f, 480f);
	}
}
