using IdeaStatiCa.BimApiLink.BimApi;
using IdeaStatiCa.BimApi;

namespace BimApiCadClient.BimApi
{
	internal class Member1D : IdeaMember1D
	{
		public string CrossSectionNo { get; set; }

		public override IIdeaCrossSection CrossSection => Get<IIdeaCrossSection>(CrossSectionNo);

		public Member1D(int no) : base(no)
		{
		}
	}
}