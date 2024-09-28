using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using E2P.AppSettingsModels;
using E2P.Models;
using E2P.Models.SearchFilters;
using E2P.Persistence;
using E2P.Services;
using E2P.ViewModels;
using E2P.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace E2P
{
    public partial class App : Application
    {
        private IConfigurationRoot? _configurationRoot;
        private ServiceCollection _serviceCollection = new ServiceCollection();
        public IServiceProvider? ServiceProvider { get; set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                _configurationRoot = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                ConfigureServices();
                var dbInit = _serviceCollection.BuildServiceProvider().GetRequiredService<DatabaseInit>();
                dbInit.EnsureDb();
                ServiceProvider = _serviceCollection.BuildServiceProvider();

                if (!IsRunningInEfCoreTooling())
                {
                    BindingPlugins.DataValidators.RemoveAt(0);

                    desktop.MainWindow = new MainWindow
                    {
                        DataContext = new MainWindowViewModel(_serviceCollection.BuildServiceProvider().GetRequiredService<CompanyService>()),
                    };
                }
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void ConfigureServices()
        {
            _serviceCollection.ConfigureWritable<ApplicationSettings>(_configurationRoot.GetSection("ApplicationSettings"));
            _serviceCollection.AddSingleton<DatabaseInit>();
            _serviceCollection.AddDbContext<ApplicationDbContext>();

            // scoped
            _serviceCollection.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            _serviceCollection.AddScoped<CompanyService>();

            // transient
            _serviceCollection.AddTransient<MainWindowViewModel>();

            // singleton
            _serviceCollection.AddSingleton<MainWindow>();
            //services.AddScoped<IService<ExcelFile, ExcelFileSearchFilters>, ExcelFileService>();
            //services.AddScoped<IService<PDFFile, PDFFileSearchFilters>, PDFFileService>();
        }

        private bool IsRunningInEfCoreTooling()
        {
            var args = Environment.GetCommandLineArgs();
            bool isMigrationTools = args.Any(arg => arg.Contains("database") || arg.Contains("migrations"));

            return isMigrationTools;
        }
    }
}
