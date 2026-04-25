using System.Data;

namespace DcConcretos.Application.Reporting;

public interface IEstadoCuentaExcelExporter
{
    Task ExportAsync(DataTable data, string filePath, CancellationToken cancellationToken = default);
}
