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

		public static void DrawSettingButton(Rect rect, Plant plant)
		{
			Widgets.Dropdown(rect, plant, new Func<Plant, ForageCategory>(ForageSelectButton_ForageSetting), new Func<Plant, IEnumerable<Widgets.DropdownMenuElement<ForageCategory>>>(ForageSelectButton_GenerateMenu), null, MedicalCareIcon(MyGameComponent.Cached.forageSettings.TryGetValue(plant.def)), null, null, null, true);
		}

		private static ForageCategory ForageSelectButton_ForageSetting(Plant plant)
		{
			return MyGameComponent.Cached.forageSettings.TryGetValue(plant.def);
		}

		private static IEnumerable<Widgets.DropdownMenuElement<ForageCategory>> ForageSelectButton_GenerateMenu(Plant plant)
		{
			throw new NotImplementedException();
		}

		private static Texture2D MedicalCareIcon(ForageCategory category)
		{
			switch (category)
			{
				case ForageCategory.Ignore:
					return ignoreIcon;
				case ForageCategory.Harvest:
					return harvestIcon;
				case ForageCategory.Extract:
					return extractIcon;
				default:
					return null;
			}
		}
		private static readonly Texture2D ignoreIcon = ContentFinder<Texture2D>.Get("UI/Designators/Cancel");
		private static readonly Texture2D harvestIcon = ContentFinder<Texture2D>.Get("UI/Designators/Harvest");
		private static readonly Texture2D extractIcon = ContentFinder<Texture2D>.Get("UI/Designators/ExtractTree");
	}
}
