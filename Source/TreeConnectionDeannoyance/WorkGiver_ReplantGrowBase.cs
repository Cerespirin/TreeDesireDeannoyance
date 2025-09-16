using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	public abstract class WorkGiver_ReplantGrowBase : WorkGiver_Scanner
	{
		public override bool AllowUnreachable
		{
			get
			{
				return true;
			}
		}

		public override IEnumerable<IntVec3> PotentialWorkCellsGlobal(Pawn pawn)
		{
			foreach (Zone_Replant zone in pawn.Map.zoneManager.AllZones.OfType<Zone_Replant>())
			{
				if (!zone.ContainsStaticFire && pawn.CanReach(zone.Cells[0], PathEndMode.OnCell, pawn.NormalMaxDanger()))
				{
					foreach (IntVec3 cell in zone.cells)
					{
						yield return cell;
					}
				}
			}
			yield break;
		}
	}
}
