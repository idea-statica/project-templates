﻿namespace FeaApi
{
	public interface IFeaApi
	{
		/// <summary>
		/// Get directory of FEA model on a disk
		/// </summary>
		/// <returns></returns>
		string GetProjectDir();

		IFeaGeometryApi Geometry { get; }
		IFeaLoadsApi Loads { get; }

		IFeaResultsApi Results { get; }
	}

	public class FeaApp : IFeaApi
	{
		public FeaApp()
		{
		}

		public IFeaGeometryApi Geometry { get; } = new FeaGeometryApi();

		public IFeaLoadsApi Loads { get; } = new FeaLoadsApi();
		public IFeaResultsApi Results { get; } = new FeaResultsApi();

		public string ProjectDir { get; set; }

		/// <inheritdoc cref="IFeaApi.GetProjectDir"/>
		public string GetProjectDir()
		{
			return ProjectDir;
		}
	}
}