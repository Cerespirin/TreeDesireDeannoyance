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

		public override bool ShouldSkip(Pawn pawn, bool forced = false)
		{
			return pawn.GetLord() != null || base.ShouldSkip(pawn, forced);
		}

		public override Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
		{
			throw new NotImplementedException();
		}

		protected override bool ExtraRequirements(Zone_Replant zone, Pawn pawn)
		{
			throw new NotImplementedException();
		}
	}
}
