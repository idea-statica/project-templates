using BimApiCadClient.CadApi;
using IdeaStatiCa.BimApi;
using IdeaStatiCa.BimApiLink.Importers;

namespace BimApiCadClient.Importers
{
	internal abstract class BaseImporter<T> : IntIdentifierImporter<T> where T : IIdeaObject
	{
		protected ICadGeometryApi Model { get; }

		protected BaseImporter(ICadGeometryApi model)
		{
			Model = model;
		}
	}
}
