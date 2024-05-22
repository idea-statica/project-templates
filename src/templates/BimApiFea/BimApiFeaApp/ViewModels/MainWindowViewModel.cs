using BimApiClientApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeaApi;
using IdeaStatiCa.Plugin;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BimApiClientApp.ViewModels
{
	/// <summary>
	/// TODO - add a description of this view model
	/// </summary>
	public class MainWindowViewModel : ObservableObject
	{
		private readonly IPluginLogger Logger;
		private string ideaStatiCaUri;
		private string statusMessage;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration">It provides a location of IDEA StatiCa setup</param>
		/// <param name="logger">Logger</param>
		public MainWindowViewModel(CheckbotConfiguration configuration, IPluginLogger logger)
		{
			if(string.IsNullOrEmpty(configuration?.IdeaStatiCaDir))
			{
				// IDEA StatiCa configuration is missing 
			}

			if(logger == null)
			{
				// logger is required
				throw new Exception("BimApiClientApp.MainWindowViewModel : Logger can not be null");
			}

			Logger = logger;
			if (Directory.Exists(configuration.IdeaStatiCaDir))
			{
				// Checkbot exists
				IdeaStatiCa = configuration.IdeaStatiCaDir;
				StatusMessage = "Ok";
				Logger.LogInformation($"BimApiClientApp.MainWindowViewModel is starting. Checkbot is found '{IdeaStatiCa}'");
			}
			else
			{
				// checkbot doesn;t exist
				IdeaStatiCa = string.Empty;
				Logger.LogInformation($"BimApiClientApp.BimApiClientApp failed. Checkbot is not found '{IdeaStatiCa}'");
				StatusMessage = $"IDEA StatiCa is not in available at'{configuration?.IdeaStatiCaDir}'";
			}

			OpenCheckbotCmdAsync = new AsyncRelayCommand(OpenCheckbot, CanOpenCheckbot);
		}

		/// <summary>
		/// Command fro opening IDEA Statica Checkbt
		/// </summary>
		public IAsyncRelayCommand OpenCheckbotCmdAsync
		{
			get;
			private set;
		}

		/// <summary>
		/// Location of IDEA Statica to show
		/// </summary>
		public string IdeaStatiCa
		{
			get { return ideaStatiCaUri; }
			set
			{
				ideaStatiCaUri = value;
				OnPropertyChanged(nameof(IdeaStatiCa));
			}
		}

		public IFeaApi FeaApi { get; private set; }

		private Task CheckbotTask { get; set; }

		/// <summary>
		/// A status messege text to show
		/// </summary>
		public string StatusMessage
		{
			get { return statusMessage; }
			set
			{
				statusMessage = value;
				OnPropertyChanged(nameof(IdeaStatiCa));
			}
		}

		private bool CanOpenCheckbot()
		{
			return !string.IsNullOrEmpty(IdeaStatiCa);
		}


		private Task OpenCheckbot()
		{
			Logger.LogInformation("MainWindowViewModel.OpenCheckbot is starting");
			try
			{
				FeaApi = new FeaApp();

				CheckbotTask = Task.Run(() => BimApiFeaClient.CheckBotRunner.Run(Path.Combine(IdeaStatiCa, "IdeaCheckbot.exe"), FeaApi, Logger));
			}
			catch (Exception ex)
			{
				Logger.LogWarning("MainWindowViewModel.OpenCheckbot failed", ex);
				StatusMessage = ex.Message;
			}
			return Task.CompletedTask;
		}
	}
}
