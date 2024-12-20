using IdeaStatiCa.Api.Common;
using IdeaStatiCa.PluginLogger;
using IdeaStatiCa.RcsApi;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RcsApiClientConsoleApp
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			// initialize logger
			string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
			var logfileName = Path.ChangeExtension(assemblyName, ".log");
			SerilogFacade.Initialize(logfileName);

			var logger = LoggerProvider.GetLogger(assemblyName);
			{
				try
				{
					// Get the directory of the executing assembly
					string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

					//Getting the directory path to the sample file in example project.
					string samplePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

					//filepath to existing .ideaRcs project
					string rcsFilePath = Path.Combine(samplePath, "Projects\\Project1.IdeaRcs");

					IApiServiceFactory<IRcsApiClient> clientFactory = null;

					try
					{
						// to attach the client to a running instance of IdeaStatiCa.ConnectionRestApi which is listening on http://localhost:5000
						// clientFactory = new ConnectionApiServiceAttacher("http://localhost:5000");

						// to run the private service IdeaStatiCa.ConnectionRestApi
						string ideaStatiCaPath = "C:\\Program Files\\IDEA StatiCa\\StatiCa 24.1"; // path to the IdeaStatiCa.ConnectionRestApi.exe
						Debug.Assert(File.Exists(Path.Combine(ideaStatiCaPath, "IdeaStatiCa.RcsRestApi.exe")), $"IdeaStatiCa.RcsRestApi.exe was not found in '{ideaStatiCaPath}'");

						clientFactory = new RcsApiServiceRunner(ideaStatiCaPath);

						using (var calculator = new Calculator(logger, clientFactory))
						{
							logger.LogInformation("Starting CBFEM calculation");
							var res = await calculator.CalculateAsync(rcsFilePath, CancellationToken.None);
							logger.LogInformation($"CBFEM calculation finished ");

							// Convert the result to a JSON string
							string jsonString = JsonConvert.SerializeObject(res, Formatting.Indented);
							Console.WriteLine(jsonString);
						}
					}
					finally
					{
						if(clientFactory is IDisposable disp)
						{
							disp.Dispose();
						}
					}
				}
				catch (Exception ex)
				{
					logger.LogWarning("An error occurred", ex);
				}
			}
		}
	}
}
