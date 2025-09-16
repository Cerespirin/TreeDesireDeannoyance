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
			Plant plant = c.GetPlant(pawn.Map);
			if (plant == null)
			{
				return false;
			}
			if (!plant.HarvestableNow || plant.LifeStage != PlantLifeStage.Mature)
			{
				return false;
			}
			if (!forced && plant.TryGetComp(out CompPlantPreventCutting compPlantPreventCutting) && compPlantPreventCutting.PreventCutting)
			{
				return false;
			}
			if (!plant.CanYieldNow())
			{
				return false;
			}
			if (!plant.def.plant.autoHarvestable && !forced)
			{
				return false;
			}
			//if (WorkGiver_Grower.wantedPlantDef == null)
			{
				//WorkGiver_Grower.wantedPlantDef = WorkGiver_Grower.CalculateWantedPlantDef(c, pawn.Map);
			}
			Zone_Growing zone_Growing = c.GetZone(pawn.Map) as Zone_Growing;
			return (zone_Growing == null || zone_Growing.allowCut /*|| plant.def == WorkGiver_Grower.wantedPlantDef*/) && PlantUtility.PawnWillingToCutPlant_Job(plant, pawn) && pawn.CanReserve(plant, 1, -1, null, forced);
		}

		public override bool ShouldSkip(Pawn pawn, bool forced = false)
		{
			return pawn.GetLord() != null || base.ShouldSkip(pawn, forced);
		}

		public override bool HasJobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
		{
			Plant plant = c.GetPlant(pawn.Map);
			if (plant == null) /**********************************************************************************************************/ { return false; }
			if (!plant.HarvestableNow || plant.LifeStage != PlantLifeStage.Mature) /******************************************************/ { return false; }
			if (!forced && plant.TryGetComp(out CompPlantPreventCutting compPlantPreventCutting) && compPlantPreventCutting.PreventCutting) { return false; }
			if (!plant.CanYieldNow()) /***************************************************************************************************/ { return false; }
			if (!plant.def.plant.autoHarvestable && !forced) /****************************************************************************/ { return false; }

			Zone_Replant zone = c.GetZone(pawn.Map) as Zone_Replant;
			return (zone?.allowCut ?? true) && PlantUtility.PawnWillingToCutPlant_Job(plant, pawn) && pawn.CanReserve(plant, 1, -1, null, forced);
		}

		public override bool ShouldSkip(Pawn pawn, bool forced = false)
		{
			return pawn.GetLord() != null || base.ShouldSkip(pawn, forced);
		}

		public override Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
		{
			Job job = JobMaker.MakeJob(JobDefOf.Harvest);
			Map map = pawn.Map;
			Room room = c.GetRoom(map);
			float totalWork = 0f;
			for (int i = 0; i < 40; i++)
			{
				IntVec3 radialCell = c + GenRadial.RadialPattern[i];
				if (radialCell.GetRoom(map) == room && HasJobOnCell(pawn, radialCell, forced))
				{
					Plant plant = radialCell.GetPlant(map);
					if (!(radialCell != c) || ((radialCell.GetZone(map) as Zone_Replant)?.settings.filter.Allows(plant) ?? false)) //plant.def == WorkGiver_Grower.CalculateWantedPlantDef(radialCell, map))
					{
						totalWork += plant.def.plant.harvestWork;
						if (radialCell != c && totalWork > 2400f)
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

		private static bool IsSupposedToBeThere(Plant plant)
		{
			if (!plant.def.plant.Sowable)
			{
				return false;
			}

			if (!plant.Spawned)
			{
				return false;
			}

			if (plant.Map.zoneManager.ZoneAt(plant.Position) is Zone_Replant zone && zone.ReplantFilter.Allows(plant))
			{
				return true;
			}

			return false;
		}
	}
}
