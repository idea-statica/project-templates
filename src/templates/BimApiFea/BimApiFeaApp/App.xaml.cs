using BimApiFeaApp.Models;
using BimApiFeaApp.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using IdeaStatiCa.Plugin;
using IdeaStatiCa.PluginLogger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace BimApiFeaApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly IServiceProvider serviceProvider;

		public App()
		{
			SerilogFacade.Initialize();
			IServiceCollection services = new ServiceCollection();

			services.AddSingleton<Microsoft.Extensions.Configuration.IConfiguration>(sp =>
			{
				return new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();
			});

			services.AddSingleton<CheckbotConfiguration>(sp =>
			{
				var configuration = sp.GetService<Microsoft.Extensions.Configuration.IConfiguration>();

				CheckbotConfiguration checkBotConfig = new CheckbotConfiguration();
				var configSection = configuration.GetSection("CheckbotConfiguration");
				checkBotConfig.IdeaStatiCaDir = configSection["IdeaStatiCa"];
				return checkBotConfig;
			});

			services.AddSingleton<IPluginLogger>(sp =>
			{
				// initialize logger the main application logger
				SerilogFacade.Initialize();
				return LoggerProvider.GetLogger("BimApiFeaApp");
			});

			services.AddTransient<MainWindow>(serviceProvider => new MainWindow
			{
				DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>()
			});

			services.AddSingleton<MainWindowViewModel>();

			services.AddTransient<Func<Type, ObservableObject>>(serviceProvider => viewModelType => (ObservableObject)serviceProvider.GetRequiredService(viewModelType));

			serviceProvider = services.BuildServiceProvider();
		}


		protected override void OnStartup(StartupEventArgs e)
		{
			var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
			mainWindow.Show();
			base.OnStartup(e);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
		}
	}
}
