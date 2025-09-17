using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class ITab_Replant : ITab_Storage
	{
		protected override bool IsPrioritySettingVisible => false;

		public ITab_Replant()
		{
			labelKey = "TreeDesireDeannoyance_TabReplant";
		}
	}
}
