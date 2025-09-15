using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class Command_Hide_ZoneReplant : Command_Hide
	{
		public Command_Hide_ZoneReplant(IHideable hideable) : base(hideable) { }

		protected override IEnumerable<FloatMenuOption> GetOptions()
		{
			return GetHideOptions();
		}

		public static IEnumerable<FloatMenuOption> GetHideOptions()
		{
			yield return new FloatMenuOption("ShowAllZones".Translate(), delegate () { ToggleAll(false); });
			yield return new FloatMenuOption("HideAllZones".Translate(), delegate () { ToggleAll(true); });
			if (cachedIcon == null)
			{
				cachedIcon = ContentFinder<Texture2D>.Get(IconTexPath);
			}
			foreach (FloatMenuOption floatMenuOption in ZoneTypeOptions<Zone_AutoReplant>("TreeDesireDeannoyance_ReplantGroup".Translate(), cachedIcon))
			{
				yield return floatMenuOption;
			}
			yield break;
		}

		private const string IconTexPath = "ZoneCreate_Replant";
		private static Texture2D cachedIcon;
	}
}
