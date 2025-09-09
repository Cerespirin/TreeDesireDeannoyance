using RimWorld;
using System;
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
			if (!Current.Game.GetComponent<MyGameComponent>().autoReplantTrees)
			{
				return true;
			}
			if (pawn.Map.areaManager.GetLabeled("Replant") == null)
			{
				return true;
			}
			return false;
		}

		public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			if (t is MinifiedTree)
			{
				if (InstallBlueprintUtility.ExistingBlueprintFor(t) == null)
				{
					IEnumerable<IntVec3> cells = t.Map.areaManager.GetLabeled("Replant").ActiveCells;

					if (!cells.Any()) return null;

					Gizmo gizmo = t.GetGizmos().First(g => g.GetType() == typeof(Designator_Replant));
					MyGameComponent component = Current.Game.GetComponent<MyGameComponent>();

					// I *still* can't believe that designators find their owners based on what the player has selected...
					try
					{
						Designator_Replant designator = gizmo as Designator_Replant;

						component.designatorOwners.Add(gizmo, t);
						designator.DesignateSingleCell(cells.Where(c1 => designator.CanDesignateCell(c1)).OrderBy(c2 => c2.DistanceToSquared(t.Position)).First());
					}
					catch (Exception e)
					{
						Log.Warning(e.Message);
						return null;
					}
					finally
					{
						component.designatorOwners.Remove(gizmo);
					}
				}
				return base.JobOnThing(pawn, InstallBlueprintUtility.ExistingBlueprintFor(t), forced);
			}
			return null;
		}
	}
}
