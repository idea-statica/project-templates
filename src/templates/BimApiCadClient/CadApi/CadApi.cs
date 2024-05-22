namespace BimApiCadClient.CadApi
{
	public interface ICadApi
	{
		ICadGeometryApi Geometry { get; }
	}

	public class CadApi : ICadApi
	{
		public ICadGeometryApi Geometry { get; } = new CadGeometryApi();
	}
}