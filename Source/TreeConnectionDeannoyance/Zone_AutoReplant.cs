using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class Zone_AutoReplant : Zone
	{

		public ThingFilter ReplantFilter
		{
			get
			{
				return replantFilter;
			}
		}

		public ThingFilter FixedReplantFilter
		{
			get
			{
				if (fixedReplantFilter == null)
				{
					fixedReplantFilter = new ThingFilter();
					foreach (ThingDef thingDef in DefDatabase<ThingDef>.AllDefs.Where(t => t.IsRelevantToTreeLovers()))
					{
						fixedReplantFilter.SetAllow(thingDef, true);
					}
				}
				return fixedReplantFilter;
			}
		}

		public Zone_AutoReplant(ZoneManager zoneManager) : base("TreeDesireDeannoyance_ZoneReplant".Translate(), zoneManager) 
		{ 
			replantFilter = new ThingFilter();
		}

		protected override Color NextZoneColor
		{
			get
			{
				return Color.Lerp(UnityEngine.Random.ColorHSV(0f, 0.196f, 1, 1, 1, 1), Color.gray, 0.5f).WithAlpha(0.09f);
			}
		}

		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Deep.Look<ThingFilter>(ref replantFilter, "replantFilter", Array.Empty<object>());
		}

		public override IEnumerable<InspectTabBase> GetInspectTabs()
		{
			return ITabs;
		}

		public override IEnumerable<Gizmo> GetGizmos() 
		{
			yield return new Command_Hide_ZoneReplant(this);
			foreach (Gizmo gizmo in base.GetGizmos()) { yield return gizmo; }
		}

		public override IEnumerable<Gizmo> GetZoneAddGizmos()
		{
			yield return DesignatorUtility.FindAllowedDesignator<Designator_ZoneAdd_Replant_Expand>();
			yield break;
		}

		private static readonly ITab[] ITabs = new ITab[] 
		{
			new ITab_Replant()
		};

		private ThingFilter fixedReplantFilter;
		private ThingFilter replantFilter;
	}
}
