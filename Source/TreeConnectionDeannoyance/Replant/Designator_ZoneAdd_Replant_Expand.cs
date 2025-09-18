using RimWorld;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class Designator_ZoneAdd_Replant_Expand : Designator_ZoneAdd_Replant
	{
		protected override bool ShowRightClickHideOptions => false;

		public Designator_ZoneAdd_Replant_Expand()
		{
			defaultLabel = "DesignatorZoneExpand".Translate();
			hotKey = KeyBindingDefOf.Misc6;
		}
	}
}
