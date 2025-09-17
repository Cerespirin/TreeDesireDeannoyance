using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class Zone_Replant : Zone, IStoreSettingsParent
	{
		public Zone_Replant() { }

		public Zone_Replant(ZoneManager zoneManager) : base("TreeDesireDeannoyance_ZoneReplant".Translate(), zoneManager)
		{
			settings = new StorageSettings(this);

			foreach (ThingDef thingDef in GetParentStoreSettings().filter.AllowedThingDefs)
			{
				if (thingDef.IsRelevantToTreeLovers())
				{
					settings.filter.SetAllow(thingDef, true);
				}
			}
		}

		protected override Color NextZoneColor
		{
			get
			{
				return Color.Lerp(UnityEngine.Random.ColorHSV(0f, 0.196f, 1, 1, 1, 1), Color.gray, 0.5f).WithAlpha(0.09f);
			}
		}

		public bool StorageTabVisible => true;

		/*
		public void DesignatePlantsToReplant()
		{
			IEnumerable<Thing> things = Map.listerThings.AllThings.Where(t => t is MinifiedTree);
			MyGameComponent component = Current.Game.GetComponent<MyGameComponent>();

			foreach (Thing thing in things)
			{
				if (InstallBlueprintUtility.ExistingBlueprintFor(thing) != null) { continue; }

				Gizmo gizmo = thing.GetGizmos().First(g => g.GetType() == typeof(Designator_Replant));

				// I *still* can't believe that designators find their owners based on what the player has selected...
				try
				{
					Designator_Replant designator = (Designator_Replant)gizmo;
					component.designatorOwners.Add(gizmo, thing);

					IEnumerable<IntVec3> cells = Cells.Where(c1 => designator.CanDesignateCell(c1));
					if (cells.Any()) 
					{ 
						designator.DesignateSingleCell(cells.OrderBy(c2 => c2.DistanceToSquared(thing.Position)).First());
					}
				}
				finally
				{
					component.designatorOwners.Remove(gizmo);
				}
			}
		}
		*/
		public override void ExposeData()
		{
			base.ExposeData();
			//Scribe_Values.Look(ref enabled, "enabled", true);
			Scribe_Deep.Look(ref settings, "settings", new object[] { this });
		}

		public override IEnumerable<InspectTabBase> GetInspectTabs()
		{
			return ITabs;
		}

		public override IEnumerable<Gizmo> GetGizmos()
		{
			yield return new Command_Hide_ZoneReplant(this);
			foreach (Gizmo gizmo in base.GetGizmos()) { yield return gizmo; }
			foreach (Gizmo gizmo2 in StorageSettingsClipboard.CopyPasteGizmosFor(settings))
			{
				yield return gizmo2;
			}
		}

		public override IEnumerable<Gizmo> GetZoneAddGizmos()
		{
			yield return DesignatorUtility.FindAllowedDesignator<Designator_ZoneAdd_Replant_Expand>();
		}

		public StorageSettings GetStoreSettings()
		{
			return settings;
		}

		public StorageSettings GetParentStoreSettings()
		{
			if (cachedFixedSettings == null)
			{
				cachedFixedSettings = new StorageSettings();

				foreach (ThingDef thingDef in DefDatabase<ThingDef>.AllDefs.Where(t => t.IsPlant && t.Minifiable))
				{
					cachedFixedSettings.filter.SetAllow(thingDef, true);
				}
			}
			return cachedFixedSettings;
		}

		public void Notify_SettingsChanged()
		{
			// nothing
		}

		private static readonly ITab[] ITabs = new ITab[]
		{
			new ITab_Replant()
		};

		//public bool enabled;
		public StorageSettings settings;
		private static StorageSettings cachedFixedSettings;
	}
}
