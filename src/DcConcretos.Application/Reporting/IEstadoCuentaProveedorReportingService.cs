using System.Data;

namespace DcConcretos.Application.Reporting;

public interface IEstadoCuentaProveedorReportingService
{
    Task<DataTable> LoadReportAsync(EstadoCuentaReportRequest request, CancellationToken cancellationToken = default);
}
