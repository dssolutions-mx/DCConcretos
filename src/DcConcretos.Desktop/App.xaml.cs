using System.Windows;
using DcConcretos.Application.Reporting;
using DcConcretos.Infrastructure.Configuration;
using DcConcretos.Infrastructure.Reporting;
using DcConcretos.Integrations.Comercial.Batch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DcConcretos.Desktop;

// Base class must be fully qualified: sibling namespace DcConcretos.Application shadows System.Windows.Application.
public partial class App : System.Windows.Application
{
    public static IHost AppHost { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        AppHost = Host.CreateDefaultBuilder(e.Args)
            .ConfigureAppConfiguration((_, cfg) =>
            {
                cfg.SetBasePath(AppContext.BaseDirectory);
                cfg.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                cfg.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
                cfg.AddUserSecrets(typeof(App).Assembly, optional: true);
            })
            .ConfigureServices((ctx, services) =>
            {
                services.Configure<SqlReportingOptions>(ctx.Configuration.GetSection(SqlReportingOptions.SectionName));
                services.AddSingleton<IEstadoCuentaProveedorReportingService, SqlEstadoCuentaProveedorReportingService>();
                services.AddSingleton<IEstadoCuentaExcelExporter, ClosedXmlEstadoCuentaExcelExporter>();
                services.AddSingleton<IComercialBatchWriter, NotImplementedComercialBatchWriter>();
                services.AddTransient<MainWindow>();
            })
            .Build();

        AppHost.Start();

        var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (AppHost is not null)
            await AppHost.StopAsync();
        AppHost?.Dispose();
        base.OnExit(e);
    }
}
