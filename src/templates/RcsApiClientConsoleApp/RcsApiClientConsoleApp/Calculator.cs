using IdeaStatiCa.Api.Common;
using IdeaStatiCa.Api.RCS.Model;
using IdeaStatiCa.Plugin;
using IdeaStatiCa.RcsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RcsApiClientConsoleApp
{
	public class Calculator : IDisposable
	{
		private bool disposedValue;
		private readonly IApiServiceFactory<IRcsApiClient> _clientFactory;
		private readonly IPluginLogger _logger;


		public Calculator(IPluginLogger logger, IApiServiceFactory<IRcsApiClient> clientFactory)
		{
			_clientFactory = clientFactory;
			_logger = logger;
		}

		/// <summary>
		/// Calculate the ideaCon project and return the result
		/// </summary>
		/// <param name="rcsFile">The path to the the rcs project to calculte</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns>Results</returns>
		public async Task<List<RcsSectionResultOverview>> CalculateAsync(string rcsFile, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Creating CreateConnectionApiClient");

			// create ConnectionApiClient
			using (var rcsClient = await _clientFactory.CreateApiClient())
			{
				// open the project and get its id
				var projData = await rcsClient.Project.OpenProjectAsync(rcsFile, cancellationToken);

				if(!projData.Sections.Any())
				{
					_logger.LogInformation($"'{rcsFile}' does not contain any sections.");
					return null;
				}

				RcsCalculationParameters rcsCalcParam = new RcsCalculationParameters()
				{
					Sections = projData.Sections.Select(s => s.Id).ToList()
				};

				_logger.LogInformation("Starting calculation");
				var rcsSectResults = await rcsClient.Calculation.CalculateAsync(projData.ProjectId, rcsCalcParam, 0, cancellationToken);
				_logger.LogInformation("Calculation finished");
				return rcsSectResults;
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
