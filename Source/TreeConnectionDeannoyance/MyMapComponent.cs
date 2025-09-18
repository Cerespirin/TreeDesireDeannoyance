using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class MyMapComponent : MapComponent
	{
		public MyMapComponent(Map map) : base(map) { }

		public override void MapComponentTick()
		{
			// Long tick
			if (map.IsHashIntervalTick(2000))
			{
				DoAutoReplant();
			}
		}

		public void DoAutoReplant()
		{
			foreach (MinifiedTree thing in map.listerThings.ThingsMatching(ThingRequest.ForGroup(ThingRequestGroup.MinifiedThing)).OfType<MinifiedTree>())
			{
				if (InstallBlueprintUtility.ExistingBlueprintFor(thing) == null)
				{
					Gizmo gizmo = thing.GetGizmos().First(g => g.GetType() == typeof(Designator_Replant));
					MyGameComponent component = Current.Game.GetComponent<MyGameComponent>();
					Designator_Replant designator = (Designator_Replant)gizmo;

					// I *still* can't believe that designators find their owners based on what the player has selected...
					try
					{
						component.designatorOwners.Add(gizmo, thing);

						IEnumerable<IntVec3> cells = GetReplantCells(thing, designator); //t.Map.GetReplantArea().ActiveCells.Where(c1 => designator.CanDesignateCell(c1));
						if (!cells.Any()) { return; }

						designator.DesignateSingleCell(cells.OrderBy(c2 => c2.DistanceToSquared(thing.Position)).First());
					}
					finally
					{
						component.designatorOwners.Remove(gizmo);
					}
				}
			}
		}

		private static IEnumerable<IntVec3> GetReplantCells(Thing thing, Designator_Replant designator)
		{
			return from Zone_Replant zone in thing.Map.zoneManager.AllZones.Where(z => z.GetType() == typeof(Zone_Replant))
					 where zone.GetStoreSettings().filter.Allows(thing)
					 from IntVec3 cell in zone.Cells
					 where designator.CanDesignateCell(cell)
					 select cell;
		}
	}
}
