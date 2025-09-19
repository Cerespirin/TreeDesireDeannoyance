using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public static class MyHelper
	{
		public static bool ExtractSetting => Current.Game.GetComponent<MyGameComponent>().alwaysExtractTrees;

		public static bool IsRelevantToTreeLovers(this Thing thing) => thing.def.plant.IsTree && thing.def.plant.treeLoversCareIfChopped;
		public static bool IsRelevantToTreeLovers(this ThingDef def) => def.plant.IsTree && def.plant.treeLoversCareIfChopped;
		public static Area GetForageArea(this Map map) => map.areaManager.GetLabeled("Forage");

		public static readonly IEnumerable<ThingDef> cachedExtractables = DefDatabase<ThingDef>.AllDefs.Where(t => t.IsPlant && t.Minifiable);
	}
}
