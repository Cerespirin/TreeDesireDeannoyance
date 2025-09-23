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

		public static void DrawSettingButton(Rect rect, ThingDef plant)
		{
			Widgets.Dropdown(rect, plant, new Func<ThingDef, ForageCategory>(ForageSelectButton_ForageSetting), new Func<ThingDef, IEnumerable<Widgets.DropdownMenuElement<ForageCategory>>>(ForageSelectButton_GenerateMenu), null, MedicalCareIcon(MyGameComponent.Cached.forageSettings.TryGetValue(plant)), null, null, null, true);
		}

		private static ForageCategory ForageSelectButton_ForageSetting(ThingDef plant)
		{
			return MyGameComponent.Cached.forageSettings.TryGetValue(plant);
		}

		private static IEnumerable<Widgets.DropdownMenuElement<ForageCategory>> ForageSelectButton_GenerateMenu(ThingDef plant)
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
