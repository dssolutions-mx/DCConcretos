using System.Text.RegularExpressions;

namespace DcConcretos.Application.Conceptos;

public static partial class ConceptoCodesParser
{
    private static readonly Regex ValidCode = MyRegex();

    public static IReadOnlyList<string> ParseFromConceptosFile(string rawText)
    {
        var list = new List<string>();
        foreach (var line in rawText.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries))
        {
            var t = line.Trim().TrimEnd(',').Trim();
            if (t.Length < 2) continue;
            if (t.StartsWith('\'') && t.EndsWith('\''))
                t = t[1..^1];
            t = t.Trim();
            if (t.Length == 0) continue;
            if (!ValidCode.IsMatch(t))
                throw new FormatException($"Código de concepto no permitido: '{t}'");
            list.Add(t);
        }
        return list;
    }

    [GeneratedRegex("^[A-Za-z0-9]+$", RegexOptions.CultureInvariant)]
    private static partial Regex MyRegex();
}
