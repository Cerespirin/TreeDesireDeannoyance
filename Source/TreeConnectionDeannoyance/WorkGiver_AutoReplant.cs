using Cerespirin.TreeDesireDeannoyance;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeConnectionDeannoyance
{
	public class WorkGiver_AutoReplant : WorkGiver_Scanner
	{
		public override ThingRequest PotentialWorkThingRequest
		{
			get 
			{
				return ThingRequest.ForGroup(ThingRequestGroup.MinifiedThing);
			}
		}

		public override bool ShouldSkip(Pawn pawn, bool forced = false)
		{
			if (!Current.Game.GetComponent<MyGameComponent>().alwaysExtractTrees)
			{
				return true;
			}
			if (pawn.Map.zoneManager.AllZones.First(z => z.label == "Replant") == null)
			{
				return true;
			}
			return false;
		}

		public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			return base.JobOnThing(pawn, t, forced);
		}
	}
}
