using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class WorkGiver_ReplantHarvest : WorkGiver_ReplantGrowBase
	{
		public override PathEndMode PathEndMode => PathEndMode.Touch;
	}
}
