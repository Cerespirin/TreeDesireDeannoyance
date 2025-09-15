using RimWorld;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class Designator_ZoneAdd_Replant_Expand : Designator_ZoneAdd_Replant
	{
		protected override bool ShowRightClickHideOptions
		{
			get
			{
				return false;
			}
		}

		public Designator_ZoneAdd_Replant_Expand()
		{
			this.defaultLabel = "DesignatorZoneExpand".Translate();
			this.hotKey = KeyBindingDefOf.Misc6;
		}
	}
}
