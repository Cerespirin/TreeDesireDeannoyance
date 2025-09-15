using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class Zone_AutoReplant : Zone
	{
		public Zone_AutoReplant(ZoneManager zoneManager) : base("TreeDesireDeannoyance_ZoneReplant".Translate(), zoneManager)
		{
		}

		protected override Color NextZoneColor
		{
			get
			{
				return Color.Lerp(new Color(1f, 0.85f, 0f), Color.gray, 0.5f).WithAlpha(0.09f);
			}
		}
	}
}
