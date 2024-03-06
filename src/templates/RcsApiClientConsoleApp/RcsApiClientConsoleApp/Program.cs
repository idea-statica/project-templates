using IdeaStatiCa.Plugin.Api.RCS;
using IdeaStatiCa.Plugin.Api.RCS.Model;
using IdeaStatiCa.PluginLogger;
using IdeaStatiCa.PluginsTools.Windows.Utilities;
using IdeaStatiCa.RcsClient.Factory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RcsApiClientConsoleApp
{
	public class Program
	{
		private static IdeaStatiCa.Plugin.IPluginLogger Logger { get; set; }
		static readonly string IdeaStaticaVersion = "23.1";

		public static async Task Main(string[] args)
		{
			// initialize logger 
			SerilogFacade.Initialize();
			Logger = LoggerProvider.GetLogger("RcsApiClientConsoleApp");
			try
			{
				string installationPath = IdeaStatiCaSetupTools.GetIdeaStatiCaInstallDir(IdeaStaticaVersion);

				Console.WriteLine($"Idea Statica found '{installationPath}'");

				Logger.LogInformation($"RcsApiClientConsoleApp is starting '{installationPath}'");
				var rcsClientFactory = new RcsClientFactory(installationPath, Logger);


				//Create the client from the Factory
				using (IRcsApiController client = await rcsClientFactory.CreateRcsApiClient())
				{

					//Getting the directory path to the sample file in example project.
					string samplePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

					//filepath to existing .ideaRcs project
					string rcsFilePath = Path.Combine(samplePath, "Projects\\Project1.IdeaRcs");

					Console.WriteLine($"Openinbg '{rcsFilePath}'");
					//Opens project on the server side to start performing operations
					bool okay = await client.OpenProjectAsync(rcsFilePath, CancellationToken.None);

					// calculate the project
					Console.WriteLine("Starting calculation");
					List<RcsSectionResultOverview> briefResults = await client.CalculateAsync(new RcsCalculationParameters(), CancellationToken.None);

					JToken parsedJson = JToken.Parse(JsonConvert.SerializeObject(briefResults));
					string output = parsedJson.ToString(Newtonsoft.Json.Formatting.Indented);

					//Print brief results to the Console
					Console.WriteLine(output);
				}
			}
			catch (Exception ex)
			{
				Logger.LogError("App failed", ex);
				Console.WriteLine(ex.Message);
			}

			// end console application
			Console.WriteLine("Done. Press any key to exit.");
			Console.ReadKey();

		}
	}
}
