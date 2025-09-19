using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		// This method runs every single tick for every cell the user is dragging the designator over, so heavily optimize this!
		public override AcceptanceReport CanDesignateCell(IntVec3 c)
		{
			if (!base.CanDesignateCell(c)) { return false; }

			Map map = Map;
			TerrainDef terrain = c.GetTerrain(map);

			// This is probably the biggest optimization possible. If we've done the work before, don't do it again!
			if (cachedTerrains.TryGetValue(terrain, out bool value)) { return value; }

			bool result = false;

			if (terrain.passability != Traversability.Impassable)
			{
				bool canEverTerraform = CompTerraformer.CanEverConvertCell(c, map, null);
				foreach (ThingDef plantDef in cachedExtractables)
				{
					if (plantDef.plant.fertilityMin > terrain.fertility && !plantDef.plant.completelyIgnoreFertility) { continue; }
					if (plantDef.plant.terraformable && !canEverTerraform) /****************************************/ { continue; }
					if (plantDef.plant.WildTerrainTags.Count > 0 && !plantDef.plant.WildTerrainTags.Overlaps(terrain.tags.OrElseEmptyEnumerable())) { continue; }
					if (plantDef.plant.terrainBlacklist?.Contains(terrain) ?? false) /*******************************/{ continue; }
					result = true;
					break;
				}
			}
			cachedTerrains.Add(terrain, result);
			return result;
		}

		public override IEnumerable<FloatMenuOption> RightClickFloatMenuOptions
		{
			get
			{
				if (ShowRightClickHideOptions)
				{
					foreach (FloatMenuOption floatMenuOption in Command_Hide_ZoneReplant.GetHideOptions())
					{
						yield return floatMenuOption;
					}
				}
				yield break;
			}
		}

		private static readonly IEnumerable<ThingDef> cachedExtractables = DefDatabase<ThingDef>.AllDefs.Where(t => t.IsPlant && t.Minifiable);
		private static readonly Dictionary<TerrainDef, bool> cachedTerrains = new Dictionary<TerrainDef, bool>();
	}
}
