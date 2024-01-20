using System;
using System.IO;

namespace CheckBotRunner.FeaApi
{
	public interface IFeaApi
	{
		/// <summary>
		/// Get directory of FEA model on a disk
		/// </summary>
		/// <returns></returns>
		string GetProjectDir();

		IFeaGeometryApi Geometry { get; }
	}

	public class FeaApi : IFeaApi
	{
		public IFeaGeometryApi Geometry { get; } = new FeaGeometryApi();

		/// <inheritdoc cref="IFeaApi.GetProjectDir"/>
		public string GetProjectDir()
		{
			var projectDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CheckBotRunner");
			if (!Directory.Exists(projectDir))
			{
				Directory.CreateDirectory(projectDir);
			}

			return projectDir;
		}
	}
}