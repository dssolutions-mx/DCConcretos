using DcConcretos.Application.Conceptos;

namespace DcConcretos.Tests;

public class ConceptoCodesParserTests
{
    [Fact]
    public void ParseFromConceptosFile_parses_shipped_format()
    {
        const string raw = """
'C001',
'C002',
'SP001',
""";
        var codes = ConceptoCodesParser.ParseFromConceptosFile(raw);
        Assert.Equal(new[] { "C001", "C002", "SP001" }, codes);
    }

    [Fact]
    public void ParseFromConceptosFile_rejects_invalid_token()
    {
        const string raw = "'C001'; DROP TABLE admDocumentos;--";
        Assert.Throws<FormatException>(() => ConceptoCodesParser.ParseFromConceptosFile(raw));
    }
}
