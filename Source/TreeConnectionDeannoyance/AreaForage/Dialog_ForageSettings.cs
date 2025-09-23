using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance.AreaForage
{
	[StaticConstructorOnStartup]
	public class Dialog_ForageSettings : Window
	{
		public override void DoWindowContents(Rect inRect)
		{
			throw new NotImplementedException();
		}

		private static ForageCategory ForageSelectButton_ForageSetting(Plant plant)
		{
			return MyGameComponent.Cached.forageSettings.TryGetValue(plant.def);
		}

		private static readonly Texture2D ignoreIcon = ContentFinder<Texture2D>.Get("UI/Designators/Cancel");
		private static readonly Texture2D harvestIcon = ContentFinder<Texture2D>.Get("UI/Designators/Harvest");
		private static readonly Texture2D extractIcon = ContentFinder<Texture2D>.Get("UI/Designators/ExtractTree");
	}
}
