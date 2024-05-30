using BimApiCadLink.BimApi;
using CadApi;
using IdeaStatiCa.BimApi;
using IdeaStatiCa.BimApiLink.Importers;

namespace BimApiCadLink.Importers
{
	public class CrossSectionImporter : StringIdentifierImporter<IIdeaCrossSection>
	{
		protected ICadGeometryApi Model { get; }

		public CrossSectionImporter(ICadGeometryApi model) 
		{
			Model = model;
		}

		public override IIdeaCrossSection Create(string id)
		{
			//For now only converting Cross-Section By Name,
			////Cross-sections can also be transfered from Parameters

			CadCrossSection css = Model.GetCrossSectionByName(id);

			return new CrossSectionByName(id)
			{
				MaterialNo = Model.GetMaterialByName(css.MaterialId).Id,
				Name = css.Name
			};
		}

		//private static CrossSectionByName GetCrossSectionByName(CadCrossSection css)
		//{
		//	return new CrossSectionByName(css.Name)
		//	{
		//		MaterialNo = css.,
		//		Name = css.Name
		//	};
		//}
	}
}