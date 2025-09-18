using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class Designator_ZoneAdd_Replant : Designator_ZoneAdd
	{
		protected override string NewZoneLabel => "TreeDesireDeannoyance_ZoneReplant".Translate();

		protected virtual bool ShowRightClickHideOptions => true;

		protected override Zone MakeNewZone()
		{
			return new Zone_Replant(Find.CurrentMap.zoneManager);
		}

		public Designator_ZoneAdd_Replant()
		{
			zoneTypeToPlace = typeof(Zone_Replant);
			defaultLabel = "TreeDesireDeannoyance_ZoneReplant".Translate();
			defaultDesc = "TreeDesireDeannoyance_DesignatorReplantZoneDesc".Translate();
			icon = ContentFinder<Texture2D>.Get("ZoneCreate_Replant", true);
			soundSucceeded = SoundDefOf.Designate_ExtractTree;
		}

		public override AcceptanceReport CanDesignateCell(IntVec3 c)
		{
			Map map = Map;
			if (!base.CanDesignateCell(c)) /******************************/ { return false; }
			if (c.GetTerrain(map).passability == Traversability.Impassable) { return false; }

			TerrainDef terrain = c.GetTerrain(map);

			foreach (ThingDef plantDef in Extractables)
			{
				return true;
			}
			return false;
		}

		public override IEnumerable<FloatMenuOption> RightClickFloatMenuOptions
		{
			get
			{
				if (!ShowRightClickHideOptions)
				{
					yield break;
				}
				foreach (FloatMenuOption floatMenuOption in Command_Hide_ZoneReplant.GetHideOptions())
				{
					yield return floatMenuOption;
				}
				yield break;
			}
		}

		public static IEnumerable<ThingDef> Extractables
		{
			get
			{
				if (cachedExtractables == null)
				{
					cachedExtractables = DefDatabase<ThingDef>.AllDefs.Where(t => t.IsPlant && t.Minifiable);
				}
				return cachedExtractables;
			}
		}

		private static IEnumerable<ThingDef> cachedExtractables;
	}
}
