using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class Zone_Replant : Zone
	{
		public Zone_Replant() { }

		public Zone_Replant(ZoneManager zoneManager) : base("TreeDesireDeannoyance_ZoneReplant".Translate(), zoneManager)
		{
			replantFilter = new ThingFilter();
			replantFilter.CopyAllowancesFrom(DefaultAutoCutFilter);
		}

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
					foreach (ThingDef thingDef in Map.wildPlantSpawner.AllWildPlants.Where(t => t.Minifiable)) //DefDatabase<ThingDef>.AllDefs.Where(t => t.IsPlant && t.Minifiable))
					{
						fixedReplantFilter.SetAllow(thingDef, true);
					}
				}
				return fixedReplantFilter;
			}
		}

		public ThingFilter DefaultAutoCutFilter
		{
			get
			{
				if (defaultReplantFilter == null)
				{
					defaultReplantFilter = new ThingFilter();
					foreach (ThingDef thingDef in FixedReplantFilter.AllowedThingDefs)
					{
						if (thingDef.IsRelevantToTreeLovers())
						{
							defaultReplantFilter.SetAllow(thingDef, true);
						}
					}
				}
				return defaultReplantFilter;
			}
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
			Scribe_Deep.Look(ref replantFilter, "replantFilter", Array.Empty<object>());
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
		private ThingFilter defaultReplantFilter;
	}
}
