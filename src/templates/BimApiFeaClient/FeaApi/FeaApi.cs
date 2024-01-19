namespace CheckBotRunner.FeaApi
{
	public interface IFeaApi
	{
		IFeaGeometryApi Geometry { get; }
	}

	public class FeaApi : IFeaApi
	{
		public IFeaGeometryApi Geometry { get; } = new FeaGeometryApi();
	}
}