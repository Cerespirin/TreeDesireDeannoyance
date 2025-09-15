using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Cerespirin.TreeDesireDeannoyance
{
	public class ITab_Replant : ITab
	{
		public ITab_Replant()
		{
			size = WinSize;
			labelKey = "TabWindTurbineAutoCut";
		}

		public override void OnOpen()
		{
			base.OnOpen();
			//plantFilterState.quickSearch.Reset();
		}

		protected override void FillTab()
		{
			//CompAutoCut autoCut = this.AutoCut;
			Rect rect = new Rect(0f, 0f, WinSize.x, WinSize.y).ContractedBy(10f);
			Widgets.BeginGroup(rect);
			float num = 0f;
			this.DrawAutoCutOptions(ref num, rect.width, autoCut);
			num += 4f;
			this.DrawPlantFilter(ref num, rect.width, rect.height - num, autoCut);
			Widgets.EndGroup();
		}





		private static readonly Vector2 WinSize = new Vector2(300f, 480f);
	}
}
