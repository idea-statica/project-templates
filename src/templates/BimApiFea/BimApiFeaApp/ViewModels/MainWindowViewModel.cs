using BimApiFeaApp.Models;
using CheckbotClient;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeaApi;
using IdeaStatiCa.Plugin;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BimApiFeaApp.ViewModels
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
				throw new Exception("BimApiFeaApp.MainWindowViewModel : Logger can not be null");
			}

			ProjectDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FeaProjectExample"); ;

			Logger = logger;
			if (Directory.Exists(configuration.IdeaStatiCaDir))
			{
				// Checkbot exists
				IdeaStatiCaSetup = configuration.IdeaStatiCaDir;
				StatusMessage = "Ok";
				Logger.LogInformation($"BimApiFeaApp.MainWindowViewModel is starting. Checkbot is found '{IdeaStatiCaSetup}'");
			}
			else
			{
				// checkbot doesn;t exist
				IdeaStatiCaSetup = string.Empty;
				Logger.LogInformation($"BimApiFeaApp.BimApiFeaApp failed. Checkbot is not found '{IdeaStatiCaSetup}'");
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
		public string IdeaStatiCaSetup
		{
			get { return ideaStatiCaUri; }
			set
			{
				ideaStatiCaUri = value;
				OnPropertyChanged(nameof(IdeaStatiCaSetup));
			}
		}

		string projectDir;
		public string ProjectDir
		{
			get { return projectDir; }
			set
			{
				projectDir = value;
				OnPropertyChanged(nameof(ProjectDir));
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
				OnPropertyChanged(nameof(IdeaStatiCaSetup));
			}
		}

		private bool CanOpenCheckbot()
		{
			return !string.IsNullOrEmpty(IdeaStatiCaSetup);
		}


		private Task OpenCheckbot()
		{
			Logger.LogInformation("MainWindowViewModel.OpenCheckbot is starting");
			try
			{
				if (!Directory.Exists(ProjectDir))
				{
					Directory.CreateDirectory(ProjectDir);
				}

				var feaApp = new FeaApp();
				feaApp.ProjectDir = ProjectDir;

				var checkBotPath = Path.Combine(IdeaStatiCaSetup, "IdeaCheckbot.exe");

				FeaApi = feaApp;
				CheckbotTask = Task.Run(() => CheckBotRunner.Run(checkBotPath, FeaApi, Logger));
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
