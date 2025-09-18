using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

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
			if (c.GetTerrain(Map).passability == Traversability.Impassable)
			{
				return false;
			}
			return base.CanDesignateCell(c);
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
	}
}
