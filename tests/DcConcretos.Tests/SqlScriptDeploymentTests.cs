namespace DcConcretos.Tests;

public class SqlScriptDeploymentTests
{
    [Fact]
    public void Reporte_sql_exists_next_to_Infrastructure_build_output()
    {
        var dir = Path.GetDirectoryName(typeof(SqlScriptDeploymentTests).Assembly.Location)!;
        var sql = Path.Combine(dir, "Sql", "ReporteEstadoCuentaProveedor.sql");
        Assert.True(File.Exists(sql), $"Expected {sql}");
        var text = File.ReadAllText(sql);
        Assert.Contains("@fechaDesde", text);
        Assert.Contains("STRING_SPLIT", text);
    }
}
