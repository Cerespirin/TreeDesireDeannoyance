using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	[StaticConstructorOnStartup]
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

		protected override Color NextZoneColor => Color.Lerp(UnityEngine.Random.ColorHSV(0f, 0.196f, 1, 1, 1, 1), Color.gray, 0.5f).WithAlpha(0.09f);

		public bool StorageTabVisible => true;

		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look(ref allowReplant, "allowReplant", true);
			Scribe_Values.Look(ref allowCut, "allowCut", false);
			Scribe_Values.Look(ref allowHarvest, "allowHarvest", false);
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

			yield return new Command_Toggle
			{
				defaultLabel = "TreeDesireDeannoyance_ZoneReplant_AllowReplant".Translate(),
				defaultDesc = "TreeDesireDeannoyance_ZoneReplant_AllowReplantDesc".Translate(),
				hotKey = KeyBindingDefOf.Command_ItemForbid,
				icon = cachedIconReplant,
				isActive = () => allowReplant,
				toggleAction = delegate ()
				{
					allowReplant = !allowReplant;
				}
			};
			yield return new Command_Toggle
			{
				defaultLabel = "TreeDesireDeannoyance_ZoneReplant_AllowCut".Translate(),
				defaultDesc = "TreeDesireDeannoyance_ZoneReplant_AllowCutDesc".Translate(),
				icon = Designator_PlantsCut.IconTex,
				isActive = () => allowCut,
				toggleAction = delegate ()
				{
					allowCut = !allowCut;
				}
			};
			yield return new Command_Toggle
			{
				defaultLabel = "TreeDesireDeannoyance_ZoneReplant_AllowHarvest".Translate(),
				defaultDesc = "TreeDesireDeannoyance_ZoneReplant_AllowHarvestDesc".Translate(),
				icon = cachedIconHarcest,
				isActive = () => allowHarvest,
				toggleAction = delegate ()
				{
					allowHarvest = !allowHarvest;
				}
			};
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

				foreach (ThingDef thingDef in MyHelper.cachedExtractables)
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

		public bool allowReplant = true;
		public bool allowCut = false;
		public bool allowHarvest = false;
		public StorageSettings settings;
		private static StorageSettings cachedFixedSettings;
		private static readonly Texture2D cachedIconReplant = ContentFinder<Texture2D>.Get("UI/Designators/ReplantTree");
		private static readonly Texture2D cachedIconHarcest = ContentFinder<Texture2D>.Get("UI/Designators/Harvest");
	}
}
