using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class WorkGiver_Forage : WorkGiver_PlantsCut
	{
		public override ThingRequest PotentialWorkThingRequest
		{
			get
			{
				return ThingRequest.ForGroup(ThingRequestGroup.Plant);
			}
		}

		public override bool ShouldSkip(Pawn pawn, bool forced = false)
		{
			if (pawn.Map.areaManager.GetLabeled("Forage") == null)
			{
				return true;
			}
			return false;
		}

		public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			if (t.def.plant?.isStump ?? false)
			{
				Area area = pawn.Map.areaManager.GetLabeled("Forage");
				IEnumerable<Thing> stumps = pawn.Map.listerThings.AllThings.Where(t2 => t2.def.plant?.isStump ?? false && area[t2.Position]);

				foreach (Thing stump in stumps)
				{
					if (stump.Map.designationManager.HasMapDesignationOn(stump))
					{
						stump.Map.designationManager.AddDesignation(new Designation(stump, DesignationDefOf.CutPlant));
					}
				}
				return base.JobOnThing(pawn, stumps.OrderBy(t3 => t3.Position.DistanceToSquared(pawn.Position)).First(), forced);
			}
			return null;
		}
	}
}
