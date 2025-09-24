using RimWorld;

namespace Cerespirin.TreeDesireDeannoyance.ZoneReplant
{
	public class ITab_Replant : ITab_Storage
	{
		protected override bool IsPrioritySettingVisible => false;

		public ITab_Replant()
		{
			labelKey = "TreeDesireDeannoyance_ITab_Replant";
		}
	}
}
