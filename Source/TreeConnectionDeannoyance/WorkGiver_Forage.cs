using RimWorld;
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
				if (pawn.Map.areaManager.GetLabeled("Forage")?[t.Position] ?? false)
				{
					if (t.Map.designationManager.HasMapDesignationOn(t))
					{
						t.Map.designationManager.AddDesignation(new Designation(t, DesignationDefOf.CutPlant));
					}
					return base.JobOnThing(pawn, t, forced);
				}
			}
			return null;
		}
	}
}
