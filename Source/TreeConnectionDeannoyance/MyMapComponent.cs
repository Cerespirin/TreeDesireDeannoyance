using System.Collections.Generic;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class MyMapComponent : MapComponent
	{
		public MyMapComponent(Map map) : base(map) { }

		public override void MapComponentTick()
		{
			// Long tick
			if (Gen.IsHashIntervalTick(map, 2000))
			{

			}
		}
	}
}
