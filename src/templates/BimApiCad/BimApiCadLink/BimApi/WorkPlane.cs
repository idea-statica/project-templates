﻿
using IdeaStatiCa.BimApiLink.BimApi;
using IdeaStatiCa.BimApi;

namespace BimApiCadLink.BimApi
{
	internal class WorkPlane : IdeaWorkPlane
	{
		public override IIdeaNode Origin => Get<IIdeaNode>(OriginNo);

		public string OriginNo { get; set; }

		public WorkPlane(string no)
			: base(no)
		{
		}
	}
}