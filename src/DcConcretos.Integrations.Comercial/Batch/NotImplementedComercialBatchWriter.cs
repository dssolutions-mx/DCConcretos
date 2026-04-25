namespace DcConcretos.Integrations.Comercial.Batch;

public sealed class NotImplementedComercialBatchWriter : IComercialBatchWriter
{
    public Task<BatchWriteResult> SubmitComprasAsync(Stream source, CancellationToken cancellationToken = default) =>
        Task.FromResult(new BatchWriteResult(false,
            "Pendiente: integrar CONTPAQi Comercial SDK (Phase 2 — ver 10-rebuild-specification.md)."));

    public Task<BatchWriteResult> SubmitDieselAsync(Stream source, CancellationToken cancellationToken = default) =>
        Task.FromResult(new BatchWriteResult(false,
            "Pendiente: integrar CONTPAQi Comercial SDK (Phase 2 — ver 10-rebuild-specification.md)."));

    public Task<BatchWriteResult> SubmitRemisionesAsync(Stream source, CancellationToken cancellationToken = default) =>
        Task.FromResult(new BatchWriteResult(false,
            "Pendiente: integrar CONTPAQi Comercial SDK (Phase 2 — ver 10-rebuild-specification.md)."));
}
