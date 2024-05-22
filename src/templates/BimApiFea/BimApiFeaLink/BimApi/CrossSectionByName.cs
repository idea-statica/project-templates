﻿using IdeaStatiCa.BimApiLink.BimApi;
using IdeaStatiCa.BimApi;

namespace BimApiFeaLink.BimApi
{
	internal class CrossSectionByName : IdeaCrossSectionByName
	{
		public override IIdeaMaterial Material => Get<IIdeaMaterial>(MaterialNo);

		public int MaterialNo { get; set; }

		public CrossSectionByName(int no) : base(no)
		{
		}
	}
}