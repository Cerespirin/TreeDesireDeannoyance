using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Cerespirin.TreeDesireDeannoyance.AreaForage
{
	[StaticConstructorOnStartup]
	public static class ForageUtility
	{
		// two methods: Reset, MedicalCareSetter

		public static string GetLabel(this ForageCategory cat)
		{
			return ("ForageCategory_" + cat.ToString()).Translate();
		}

		// one method: AllowsMedicine

		public static void ForageSelectButton(Rect rect, ThingDef plant)
		{
			Widgets.Dropdown(rect, plant, new Func<ThingDef, ForageCategory>(ForageSelectButton_GetForage), new Func<ThingDef, IEnumerable<Widgets.DropdownMenuElement<ForageCategory>>>(ForageSelectButton_GenerateMenu), null, ForageIcon(MyGameComponent.Cached.forageSettings.TryGetValue(plant)), null, null, null, true);
		}

		private static ForageCategory ForageSelectButton_GetForage(ThingDef plant)
		{
			return MyGameComponent.Cached.forageSettings.TryGetValue(plant);
		}

		private static IEnumerable<Widgets.DropdownMenuElement<ForageCategory>> ForageSelectButton_GenerateMenu(ThingDef plant)
		{
			Widgets.DropdownMenuElement<ForageCategory> dropdownMenuElement;
			int num;
			for (int i = 0; i < 3; i = num + 1)
			{
				ForageCategory forageCategory = (ForageCategory)i;
				dropdownMenuElement = new Widgets.DropdownMenuElement<ForageCategory>
				{
					option = new FloatMenuOption(forageCategory.GetLabel().CapitalizeFirst(), delegate ()
					{
						MyGameComponent.Cached.forageSettings.SetOrAdd(plant, forageCategory);
					}, ForageIcon(forageCategory), Color.white),
					payload = forageCategory
				};
				yield return dropdownMenuElement;
				num = i;
			}
			yield break;
		}

		private static Texture2D ForageIcon(ForageCategory category)
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
