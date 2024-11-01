using IdeaStatiCa.PluginLogger;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectionApiClientApp
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

					// Example usage of the directory
					string ideaConFile = Path.Combine(assemblyDirectory, "Projects", "test1.ideaCon");

					using (var calculator = new Calculator(logger))
					{
						logger.LogInformation("Starting CBFEM calculation");
						var res = await calculator.CalculateAsync(ideaConFile, CancellationToken.None);
						logger.LogInformation($"CBFEM calculation finished ");
						// Convert the result to a JSON string
						string jsonString = JsonConvert.SerializeObject(res, Formatting.Indented);
						Console.WriteLine(jsonString);

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
