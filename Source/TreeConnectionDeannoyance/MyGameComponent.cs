using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class MyGameComponent : GameComponent
	{
		public bool extractTreesAggressively = false;

		public MyGameComponent(Game game) { }

		public override void ExposeData()
		{
			Scribe_Values.Look(ref extractTreesAggressively, "TreeDesireDeannoyance_ExtractTreesAggressively");
		}
	}
}
