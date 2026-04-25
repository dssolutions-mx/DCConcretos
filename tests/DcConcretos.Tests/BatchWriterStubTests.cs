using DcConcretos.Integrations.Comercial.Batch;

namespace DcConcretos.Tests;

public class BatchWriterStubTests
{
    [Fact]
    public async Task NotImplemented_returns_message()
    {
        var w = new NotImplementedComercialBatchWriter();
        await using var s = new MemoryStream();
        var r = await w.SubmitComprasAsync(s);
        Assert.False(r.Success);
        Assert.Contains("SDK", r.Message ?? "");
    }
}
