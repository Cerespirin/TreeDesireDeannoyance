using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
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
			if (!MyHelper.ReplantSetting)
			{
				return true;
			}
			return false;
		}

		public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			if (t is MinifiedTree)
			{
				Blueprint_Install blueprint = InstallBlueprintUtility.ExistingBlueprintFor(t);
				if (blueprint != null) { return base.JobOnThing(pawn, blueprint, forced); }

				Gizmo gizmo = t.GetGizmos().First(g => g.GetType() == typeof(Designator_Replant));
				MyGameComponent component = Current.Game.GetComponent<MyGameComponent>();
				Designator_Replant designator = (Designator_Replant)gizmo;

				// I *still* can't believe that designators find their owners based on what the player has selected...
				try
				{
					component.designatorOwners.Add(gizmo, t);

					IEnumerable<IntVec3> cells = MyHelper.GetReplantCells(t, designator); //t.Map.GetReplantArea().ActiveCells.Where(c1 => designator.CanDesignateCell(c1));
					if (!cells.Any()) { return null; }

					designator.DesignateSingleCell(cells.OrderBy(c2 => c2.DistanceToSquared(t.Position)).First());
				}
				finally
				{
					component.designatorOwners.Remove(gizmo);
				}
			}
			return null;
		}
	}
}
