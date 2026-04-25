using System.Data;
using System.Globalization;
using ClosedXML.Excel;
using DcConcretos.Application.Reporting;

namespace DcConcretos.Infrastructure.Reporting;

public sealed class ClosedXmlEstadoCuentaExcelExporter : IEstadoCuentaExcelExporter
{
    public Task ExportAsync(DataTable data, string filePath, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("EstadoCuenta");
        var row = 1;
        for (var c = 0; c < data.Columns.Count; c++)
            ws.Cell(row, c + 1).Value = data.Columns[c].ColumnName;
        row++;
        foreach (DataRow dr in data.Rows)
        {
            for (var c = 0; c < data.Columns.Count; c++)
            {
                var v = dr[c];
                if (v is DBNull)
                    ws.Cell(row, c + 1).Clear();
                else
                {
                    // ClosedXML 0.102+ requires XLCellValue; avoid implicit object assignment (fails CI / strict compile).
                    var text = Convert.ToString(v, CultureInfo.InvariantCulture) ?? string.Empty;
                    ws.Cell(row, c + 1).Value = text;
                }
            }
            row++;
        }
        ws.Columns().AdjustToContents();
        workbook.SaveAs(filePath);
        return Task.CompletedTask;
    }
}
