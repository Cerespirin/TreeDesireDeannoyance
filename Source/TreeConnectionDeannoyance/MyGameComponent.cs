using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class MyGameComponent : GameComponent
	{
		public bool alwaysExtractTrees = false;

		public MyGameComponent(Game game) { }

		public override void ExposeData()
		{
			Scribe_Values.Look(ref alwaysExtractTrees, "TreeDesireDeannoyance_ExtractTreesAggressively");
		}
	}
}
