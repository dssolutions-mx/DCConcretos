namespace DcConcretos.Application.Reporting;

public sealed record EstadoCuentaReportRequest(
    DateOnly FechaDesde,
    DateOnly FechaHasta,
    IReadOnlyList<string> CodigosConcepto);
