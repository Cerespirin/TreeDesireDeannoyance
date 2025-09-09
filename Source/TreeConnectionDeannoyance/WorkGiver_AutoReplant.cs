using RimWorld;
using System;
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
			if (pawn.Map.areaManager.AllAreas.First(z => z.RenamableLabel == "Replant") == null)
			{
				return true;
			}
			return false;
		}

		public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			Log.Message("WorkGiver_AutoReplant.JobOnThing called.");
			MinifiedTree extractedPlant = t as MinifiedTree;
			if (extractedPlant != null)
			{
				Log.Message("WorkGiver_AutoReplant.JobOnThing extractedPlant not null.");
				Designator_Replant designator = (Designator_Replant)extractedPlant.GetGizmos().First(g => g.GetType() == typeof(Designator_Replant));
				MyGameComponent component = Current.Game.GetComponent<MyGameComponent>();
				Area area = extractedPlant.Map.areaManager.AllAreas.First(z => z.RenamableLabel == "Replant");

				// I *still* can't believe that designators find their owners based on what the player has selected...
				try
				{
					component.designatorOwners.Add(designator, extractedPlant);
					designator.DesignateSingleCell(area.ActiveCells.Where(v => designator.CanDesignateCell(v)).OrderBy(v => v.DistanceToSquared(pawn.Position)).First());
				}
				catch (Exception e)
				{ 
					Log.Warning(e.Message);
					return null;
				}
				finally
				{
					component.designatorOwners.Remove(designator);
				}
				return base.JobOnThing(pawn, InstallBlueprintUtility.ExistingBlueprintFor(extractedPlant), forced);
			}
			return null;
		}
	}
}
