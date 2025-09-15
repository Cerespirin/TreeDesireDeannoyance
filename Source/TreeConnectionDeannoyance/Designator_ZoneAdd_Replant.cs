using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class Designator_ZoneAdd_Replant : Designator_ZoneAdd
	{
		protected override string NewZoneLabel
		{
			get
			{
				return "TreeDesireDeannoyance_ZoneReplant".Translate();
			}
		}

		protected virtual bool ShowRightClickHideOptions
		{
			get
			{
				return true;
			}
		}

		protected override Zone MakeNewZone()
		{
			return new Zone_AutoReplant(Find.CurrentMap.zoneManager);
		}
	}
}
