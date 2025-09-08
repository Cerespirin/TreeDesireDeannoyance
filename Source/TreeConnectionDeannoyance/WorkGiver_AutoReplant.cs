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
	public class WorkGiver_AutoReplant : WorkGiver_Replant
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
			if (!Current.Game.GetComponent<MyGameComponent>().autoReplantTrees)
			{
				return true;
			}
			if (pawn.Map.zoneManager.AllZones.First(z => z.label == "Replant") == null)
			{
				return true;
			}
			return false;
		}

		public override Job JobOnCell(Pawn pawn, IntVec3 cell, bool forced = false)
		{
			return base.JobOnCell(pawn, cell, forced);
		}

		public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			MinifiedTree extractedPlant = t as MinifiedTree;
			if (extractedPlant != null)
			{
				Zone zone = t.Map.zoneManager.AllZones.First(z => z.RenamableLabel == "Replant");
				var cells = zone.Cells.Where(v => extractedPlant.def.CanEverPlantAt(v, t.Map, true));

				foreach (IntVec3 cell in cells)
				{
					if (ReservationUtility.HasReserved(pawn, cell))
					{
						Blueprint_Install blueprint = GenConstruct.PlaceBlueprintForInstall(extractedPlant, cell, pawn.Map, Rot4.North, Faction.OfPlayer);
						return base.JobOnThing(pawn, blueprint, forced);
					}
				}
			}
			return null;
		}
	}
}
