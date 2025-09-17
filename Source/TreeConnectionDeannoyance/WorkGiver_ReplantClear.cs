using RimWorld;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class WorkGiver_ReplantClear : WorkGiver_ReplantGrowBase
	{
		public override PathEndMode PathEndMode => PathEndMode.ClosestTouch;
		
		public override Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
		{
			Map map = pawn.Map;
			Zone_Replant zone = c.GetZone(map) as Zone_Replant;
			Plant plant = c.GetPlant(map);

			foreach (Thing thing in c.GetThingList(map))
			{
				if (!thing.def.BlocksPlanting(true)) /**********/ { continue; }
				if (!pawn.CanReserve(thing, 1, -1, null, forced)) { return null; }

				if (thing.def.category == ThingCategory.Plant)
				{
					if (thing.IsForbidden(pawn)) /**********************************************************/ { return null; }
					if (zone != null && !zone.allowCut) /***************************************************/ { return null; }
					if (!forced && plant.TryGetComp(out CompPlantPreventCutting comp) && comp.PreventCutting) { return null; }
					if (!PlantUtility.PawnWillingToCutPlant_Job(thing, pawn)) /*****************************/ { return null; }
					// Would return null in GrowerSow but, honestly, as long as we're here we might as well extract the fucking tree.
					if (PlantUtility.TreeMarkedForExtraction(thing)) /**************************************/ { return JobMaker.MakeJob(JobDefOf.ExtractTree, thing); }
					if (zone.settings.filter.Allows(thing)) /************************************************/{ return null; }

					return JobMaker.MakeJob(JobDefOf.CutPlant, thing);
				}
				if (thing.def.EverHaulable) 
				{ 
					//return HaulAIUtility.HaulAsideJobFor(pawn, thing); 
				}
				return null;
			}
			return null;
		}
	}
}
