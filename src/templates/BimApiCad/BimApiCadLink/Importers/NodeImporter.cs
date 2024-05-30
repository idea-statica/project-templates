using CadApi;
using IdeaStatiCa.BimApi;
using IdeaStatiCa.BimApiLink.BimApi;
using IdeaStatiCa.BimApiLink.Importers;

namespace BimApiCadLink.Importers
{
	public class NodeImporter : StringIdentifierImporter<IIdeaNode>
	{
		protected ICadGeometryApi Model { get; }

		public NodeImporter(ICadGeometryApi model) 
		{
			Model = model;
		}

		public override IIdeaNode Create(string id) => new IdeaNode(id)
		{
			Name = id.ToString(),
			Vector = GetNodePosition(Utils.PointTranslator.GetPoint3D(id))
		};

		private IdeaVector3D GetNodePosition(CadPoint3D point)
		{
			return new IdeaVector3D(
						point.X,
						point.Y,
						point.Z);
		}
	}
}