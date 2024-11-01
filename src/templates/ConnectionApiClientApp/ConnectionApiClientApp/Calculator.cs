using IdeaStatiCa.ConnectionApi;
using IdeaStatiCa.ConnectionApi.Model;
using IdeaStatiCa.Plugin;

namespace ConnectionApiClientApp
{
	public class Calculator : IDisposable
	{
		private bool disposedValue;
		private readonly ConnectionApiClientFactory _clientFactory;
		private readonly IPluginLogger _logger;


		public Calculator(IPluginLogger logger)
		{
			_logger = logger;

			// attaach to the existing service
			_clientFactory = new ConnectionApiClientFactory("http://localhost:5000");
		}

		/// <summary>
		/// Calculate the ideaCon project and return the result
		/// </summary>
		/// <param name="ideaConFile">The path to the a ideacon project to calculte</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns>Results</returns>
		public async Task<List<ConResultSummary>> CalculateAsync(string ideaConFile, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Creating CreateConnectionApiClient");
			// create ConnectionApiClient
			using (var conClient = await _clientFactory.CreateConnectionApiClient())
			{
				// open the project and get its id
				var projData = await conClient.Project.OpenProjectAsync(ideaConFile, cancellationToken);

				if(!projData.Connections.Any())
				{
					_logger.LogInformation($"'ideaConFile' does not contain any connections.");
					return null;
				}

				// request to run plastic CBFEM for all connections in the project
				ConCalculationParameter conCalcParam = new ConCalculationParameter()
				{
					AnalysisType = ConAnalysisTypeEnum.StressStrain,
					ConnectionIds = projData.Connections.Select(c => c.Id).ToList()
				};

				_logger.LogInformation("Starting calculation");
				var cbfemResult = await conClient.Calculation.CalculateAsync(projData.ProjectId, conCalcParam, 0, cancellationToken);
				_logger.LogInformation("Calculation finished");
				return cbfemResult;
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}


		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
