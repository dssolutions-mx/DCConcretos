using System.Data;
using DcConcretos.Application.Reporting;
using DcConcretos.Infrastructure.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace DcConcretos.Infrastructure.Reporting;

public sealed class SqlEstadoCuentaProveedorReportingService(IOptions<SqlReportingOptions> options)
    : IEstadoCuentaProveedorReportingService
{
    private readonly string _connectionString = string.IsNullOrWhiteSpace(options.Value.ConnectionString)
        ? throw new InvalidOperationException("SqlReporting:ConnectionString is required (use appsettings.Local.json or user secrets).")
        : options.Value.ConnectionString;

    public async Task<DataTable> LoadReportAsync(EstadoCuentaReportRequest request, CancellationToken cancellationToken = default)
    {
        if (request.CodigosConcepto.Count == 0)
            throw new ArgumentException("Se requiere al menos un código de concepto.", nameof(request));

        var sqlPath = Path.Combine(AppContext.BaseDirectory, "Sql", "ReporteEstadoCuentaProveedor.sql");
        if (!File.Exists(sqlPath))
            throw new FileNotFoundException("No se encontró ReporteEstadoCuentaProveedor.sql", sqlPath);

        var script = await File.ReadAllTextAsync(sqlPath, cancellationToken).ConfigureAwait(false);
        var conceptCodes = string.Join(",", request.CodigosConcepto);

        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var cmd = new SqlCommand(script, conn)
        {
            CommandTimeout = 120
        };
        cmd.Parameters.Add("@fechaDesde", SqlDbType.Date).Value = request.FechaDesde.ToDateTime(TimeOnly.MinValue);
        cmd.Parameters.Add("@fechaHasta", SqlDbType.Date).Value = request.FechaHasta.ToDateTime(TimeOnly.MinValue);
        cmd.Parameters.Add("@conceptCodes", SqlDbType.NVarChar, -1).Value = conceptCodes;

        using var reader = await cmd.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        var table = new DataTable();
        table.Load(reader);
        return table;
    }
}
