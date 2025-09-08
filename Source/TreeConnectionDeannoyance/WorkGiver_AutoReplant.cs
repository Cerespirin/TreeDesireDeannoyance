using RimWorld;
using System.Linq;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class WorkGiver_AutoReplant : WorkGiver_Scanner
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
			if (pawn.Map.areaManager.AllAreas.First(z => z.RenamableLabel == "Replant") == null)
			{
				return true;
			}
			return false;
		}

		public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			MinifiedTree extractedPlant = t as MinifiedTree;
			if (extractedPlant != null)
			{
				Designator_Replant designator = (Designator_Replant)extractedPlant.GetGizmos().First(g => g.GetType() == typeof(Designator_Replant));

				Area area = t.Map.areaManager.AllAreas.First(z => z.RenamableLabel == "Replant");
				IOrderedEnumerable<IntVec3> cells = area.ActiveCells.Where(v => designator.CanDesignateCell(v)).OrderBy(v => v.DistanceToSquared(pawn.Position));

				foreach (IntVec3 cell in cells)
				{
					designator.DesignateSingleCell(cell);
				}
			}
			return null;
		}
	}
}
