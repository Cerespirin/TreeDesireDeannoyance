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
		
		public override bool HasJobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
		{
			throw new NotImplementedException();
		}

		public override Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
		{
			Job job = JobMaker.MakeJob(JobDefOf.Harvest);
			Map map = pawn.Map;
			Room room = c.GetRoom(map);
			float num = 0f;
			for (int i = 0; i < 40; i++)
			{
				IntVec3 intVec = c + GenRadial.RadialPattern[i];
				if (intVec.GetRoom(map) == room && HasJobOnCell(pawn, intVec, forced))
				{
					Plant plant = intVec.GetPlant(map);
					if (!(intVec != c) || plant.def == WorkGiver_Grower.CalculateWantedPlantDef(intVec, map))
					{
						num += plant.def.plant.harvestWork;
						if (intVec != c && num > 2400f)
						{
							break;
						}
						job.AddQueuedTarget(TargetIndex.A, plant);
					}
				}
			}
			if (job.targetQueueA != null && job.targetQueueA.Count >= 3)
			{
				job.targetQueueA.SortBy(targ => targ.Cell.DistanceToSquared(pawn.Position));
			}
			return job;
		}
	}
}
