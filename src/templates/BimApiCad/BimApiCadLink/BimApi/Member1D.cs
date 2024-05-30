using IdeaStatiCa.BimApiLink.BimApi;
using IdeaStatiCa.BimApi;

namespace BimApiCadLink.BimApi
{
	public class Member1D : IdeaMember1D
	{
		public string CrossSectionNo { get; set; }

		public override IIdeaCrossSection CrossSection => Get<IIdeaCrossSection>(CrossSectionNo);

		public Member1D(int no) : base(no)
		{
		}
	}
}