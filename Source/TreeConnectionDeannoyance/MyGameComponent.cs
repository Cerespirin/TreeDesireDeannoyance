using Cerespirin.TreeDesireDeannoyance.AreaForage;
using System.Collections.Generic;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class MyGameComponent : GameComponent
	{
		public static MyGameComponent Cached
		{
			get
			{
				if (cachedComponent == null)
				{
					cachedComponent = Current.Game.GetComponent<MyGameComponent>();
				}
				return cachedComponent;
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Is part of a shipped public API.")]
		public MyGameComponent(Game game) { }

		public override void ExposeData()
		{
			Scribe_Values.Look(ref alwaysExtractTrees, "alwaysExtractTrees");
		}

		public bool alwaysExtractTrees = false;
		public Dictionary<ThingDef, ForageCategory> forageSettings = new Dictionary<ThingDef, ForageCategory>();
		public readonly Dictionary<Gizmo, Thing> designatorOwners = new Dictionary<Gizmo, Thing>();
		private static MyGameComponent cachedComponent;
	}
}
