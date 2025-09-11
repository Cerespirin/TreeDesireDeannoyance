using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public static class MyHelper
	{
		public static Area GetAutoReplantArea(this Map map) => map.areaManager.GetLabeled("Replant");
		public static Area GetForageArea(this Map map) => map.areaManager.GetLabeled("Forage");
	}
}
