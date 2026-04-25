namespace DcConcretos.Integrations.Comercial.Batch;

public interface IComercialBatchWriter
{
    Task<BatchWriteResult> SubmitComprasAsync(Stream source, CancellationToken cancellationToken = default);

    Task<BatchWriteResult> SubmitDieselAsync(Stream source, CancellationToken cancellationToken = default);

    Task<BatchWriteResult> SubmitRemisionesAsync(Stream source, CancellationToken cancellationToken = default);
}

public sealed record BatchWriteResult(bool Success, string? Message);
