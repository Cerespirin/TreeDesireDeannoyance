using System.Collections.Generic;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class MyGameComponent : GameComponent
	{
		public bool alwaysExtractTrees = false;
		public bool autoReplantTrees = false;
		public Dictionary<Gizmo, Thing> designatorOwners = new Dictionary<Gizmo, Thing>();

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Is part of a shipped public API.")]
		public MyGameComponent(Game game) { }

		public override void ExposeData()
		{
			Scribe_Values.Look(ref alwaysExtractTrees, "TreeDesireDeannoyance_AlwaysExtractTrees");
			Scribe_Values.Look(ref autoReplantTrees, "TreeDesireDeannoyance_AutoReplantTrees");
		}
	}
}
