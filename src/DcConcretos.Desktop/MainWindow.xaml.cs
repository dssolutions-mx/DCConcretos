using System.IO;
using System.Windows;
using DcConcretos.Application.Conceptos;
using DcConcretos.Application.Reporting;
using DcConcretos.Integrations.Comercial.Batch;
using Microsoft.Win32;

namespace DcConcretos.Desktop;

public partial class MainWindow : Window
{
    private readonly IEstadoCuentaProveedorReportingService _reporting;
    private readonly IEstadoCuentaExcelExporter _excel;
    private readonly IComercialBatchWriter _batchWriter;
    private System.Data.DataTable? _lastResult;

    public MainWindow(
        IEstadoCuentaProveedorReportingService reporting,
        IEstadoCuentaExcelExporter excel,
        IComercialBatchWriter batchWriter)
    {
        InitializeComponent();
        _reporting = reporting;
        _excel = excel;
        _batchWriter = batchWriter;
        DpDesde.SelectedDate = DateTime.Today.AddMonths(-1);
        DpHasta.SelectedDate = DateTime.Today;
        Loaded += MainWindow_OnLoaded;
    }

    private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Assets", "ConceptosEstadoCuentaProveedor.txt");
            if (File.Exists(path))
                TxtConceptos.Text = await File.ReadAllTextAsync(path).ConfigureAwait(true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Conceptos", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private async void BtnRun_OnClick(object sender, RoutedEventArgs e)
    {
        BtnRun.IsEnabled = false;
        try
        {
            if (DpDesde.SelectedDate is null || DpHasta.SelectedDate is null)
            {
                MessageBox.Show(this, "Seleccione fechas desde / hasta.", "Validación", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var desde = DateOnly.FromDateTime(DpDesde.SelectedDate.Value);
            var hasta = DateOnly.FromDateTime(DpHasta.SelectedDate.Value);
            if (hasta < desde)
            {
                MessageBox.Show(this, "La fecha hasta debe ser mayor o igual que desde.", "Validación",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var codes = ConceptoCodesParser.ParseFromConceptosFile(TxtConceptos.Text);
            var req = new EstadoCuentaReportRequest(desde, hasta, codes);
            _lastResult = await _reporting.LoadReportAsync(req).ConfigureAwait(true);
            GridResultados.ItemsSource = _lastResult.DefaultView;
            BtnExport.IsEnabled = _lastResult.Rows.Count > 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            BtnRun.IsEnabled = true;
        }
    }

    private async void BtnExport_OnClick(object sender, RoutedEventArgs e)
    {
        if (_lastResult is null || _lastResult.Rows.Count == 0) return;
        var dlg = new SaveFileDialog
        {
            Filter = "Excel|*.xlsx",
            FileName = $"EstadoCuenta_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
        };
        if (dlg.ShowDialog(this) != true) return;
        try
        {
            await _excel.ExportAsync(_lastResult, dlg.FileName).ConfigureAwait(true);
            MessageBox.Show(this, "Exportación completada.", "Excel", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void MenuCompras_OnClick(object sender, RoutedEventArgs e) =>
        await ShowBatchStubAsync("Compras", _batchWriter.SubmitComprasAsync).ConfigureAwait(true);

    private async void MenuDiesel_OnClick(object sender, RoutedEventArgs e) =>
        await ShowBatchStubAsync("Diesel", _batchWriter.SubmitDieselAsync).ConfigureAwait(true);

    private async void MenuRemisiones_OnClick(object sender, RoutedEventArgs e) =>
        await ShowBatchStubAsync("Remisiones", _batchWriter.SubmitRemisionesAsync).ConfigureAwait(true);

    private static async Task ShowBatchStubAsync(string name, Func<Stream, CancellationToken, Task<BatchWriteResult>> fn)
    {
        await using var empty = new MemoryStream();
        var r = await fn(empty, default).ConfigureAwait(true);
        MessageBox.Show($"{name}: {(r.Success ? "OK" : r.Message)}", "Carga batch", MessageBoxButton.OK,
            r.Success ? MessageBoxImage.Information : MessageBoxImage.Information);
    }
}
