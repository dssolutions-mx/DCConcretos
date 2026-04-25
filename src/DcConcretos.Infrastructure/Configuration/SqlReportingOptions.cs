namespace DcConcretos.Infrastructure.Configuration;

public sealed class SqlReportingOptions
{
    public const string SectionName = "SqlReporting";

    public string ConnectionString { get; set; } = string.Empty;
}
