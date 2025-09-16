using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public static class MyHelper
	{
		public static bool ExtractSetting => Current.Game.GetComponent<MyGameComponent>().alwaysExtractTrees;
		public static bool ReplantSetting => Current.Game.GetComponent<MyGameComponent>().autoReplantTrees;
		public static bool IsRelevantToTreeLovers(this Thing thing) => thing.def.plant.IsTree && thing.def.plant.treeLoversCareIfChopped;
		public static bool IsRelevantToTreeLovers(this ThingDef def) => def.plant.IsTree && def.plant.treeLoversCareIfChopped;
		//public static Area GetReplantArea(this Map map) => map.areaManager.GetLabeled("Replant");
		public static Area GetForageArea(this Map map) => map.areaManager.GetLabeled("Forage");

		public static IEnumerable<IntVec3> GetReplantCells(Thing thing, Designator_Replant designator)
		{
			return from Zone_Replant zone in thing.Map.zoneManager.AllZones.Where(z => z.GetType() == typeof(Zone_Replant))
					 where zone.ReplantFilter.Allows(thing)
					 from IntVec3 cell in zone.Cells
					 where designator.CanDesignateCell(cell)
					 select cell;
		}
	}
}
