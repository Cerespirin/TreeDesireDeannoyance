using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public static class MyHelper
	{
		public static bool IsRelevantToTreeLovers(this Thing thing) => thing.def.plant.IsTree && thing.def.plant.treeLoversCareIfChopped;
		public static bool GetExtractSetting() => Current.Game.GetComponent<MyGameComponent>().alwaysExtractTrees;
		public static bool GetReplantSetting() => Current.Game.GetComponent<MyGameComponent>().autoReplantTrees;
		public static Area GetReplantArea(this Map map) => map.areaManager.GetLabeled("Replant");
		public static Area GetForageArea(this Map map) => map.areaManager.GetLabeled("Forage");
	}
}
