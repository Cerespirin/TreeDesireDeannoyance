using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	public static class MyHelper
	{
		public static bool IsRelevantToTreeLovers(this Thing thing) => thing.def.plant.IsTree && thing.def.plant.treeLoversCareIfChopped;
		public static bool IsRelevantToTreeLovers(this ThingDef def) => def.plant.IsTree && def.plant.treeLoversCareIfChopped;

		public static void PossiblyChangeCutJobToHarvest(ref Job job)
		{
			if (MyGameComponent.Cached.alwaysExtractTrees && job.def == JobDefOf.CutPlant && job.targetA.Thing.IsRelevantToTreeLovers())
			{
				DesignationManager designationManager = job.targetA.Thing.Map.designationManager;
				if (designationManager.DesignationOn(job.targetA.Thing, DesignationDefOf.CutPlant) == null)
				{
					job.def = JobDefOf.ExtractTree;
					job.ignoreDesignations = true;
				}
			}
		}

		public static readonly IEnumerable<ThingDef> cachedExtractables = DefDatabase<ThingDef>.AllDefs.Where(t => t.IsPlant && t.Minifiable);
	}
}
