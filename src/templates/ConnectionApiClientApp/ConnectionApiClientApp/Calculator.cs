using IdeaStatiCa.Api.Common;
using IdeaStatiCa.Api.Connection.Model;
using IdeaStatiCa.ConnectionApi;
using IdeaStatiCa.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectionApiClientApp
{
	public class Calculator : IDisposable
	{
		private bool disposedValue;
		private readonly IApiServiceFactory<IConnectionApiClient> _clientFactory;
		private readonly IPluginLogger _logger;


		public Calculator(IPluginLogger logger, IApiServiceFactory<IConnectionApiClient> clientFactory)
		{
			_clientFactory = clientFactory;
			_logger = logger;
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
			using (var conClient = await _clientFactory.CreateApiClient())
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
					AnalysisType = ConAnalysisTypeEnum.Stress_Strain,
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
