namespace CadApi
{
	public interface ICadApi
	{
		ICadGeometryApi Geometry { get; }

		/// <summary>
		/// Get directory of FEA model on a disk
		/// </summary>
		/// <returns></returns>
		string GetProjectDir();
	}

	public class CadApp : ICadApi
	{
		public ICadGeometryApi Geometry { get; } = new CadGeometryApi();

		public string ProjectDir { get; set; }

		public string GetProjectDir()
		{
			return ProjectDir;
		}
	}
}