# Rebuild specification (Phase 2 blueprint)

## Goals

Rebuild **DCConcretosWF**-equivalent functionality as a **maintainable** .NET application integrated with **CONTPAQi Comercial**, preserving business outcomes validated in **`09-validation-protocol.md`**.

## Target stack (recommended)

- **Runtime:** .NET 8+ (or .NET Framework 4.8 only if Comercial SDK constraints require — confirm with distributor).
- **Writes:** **CONTPAQi Comercial SDK** (or **ARSoftware.Contpaqi.Comercial.Sdk**-aligned patterns) — **avoid undocumented raw INSERT** except where distributor explicitly approves.
- **Reads / reporting:** T-SQL or EF raw SQL **read-only** against `adm*` tables where appropriate.
- **Excel:** ClosedXML or OpenXML SDK (parity with legacy) with **schema-validated** templates.

## Functional requirements (FR)

1. **Supplier account statement** — parity with `FrmEstadoDeCuentaProveedor`: filters, Excel export, optional “send to Comercial” behavior once mechanism is known from trace/decompile.
2. **Batch: Compras** — parity with `FrmCargaCompras` including file discovery via `rutaArchivoCompras`.
3. **Batch: Diesel** — parity with `FrmCargaDiesel` (`rutaArchivoDiesel`).
4. **Batch: Remisiones** — parity with `FrmCargaRemisiones` (`GetFoliosRemisiones` semantics).
5. **Console / diagnostics** — parity or improvement over `FrmConsola` (structured logs).
6. **Configuration** — replace plaintext secrets with **Windows DPAPI**, **Credential Manager**, or **Azure Key Vault** pattern as appropriate; support same logical settings as legacy keys in `01-environment-inventory.md`.

## Non-functional requirements (NFR)

- **Security:** no `sa`; least-privilege SQL; audit trail (who/when/what batch).
- **Observability:** structured logging, optional Application Insights.
- **Testability:** integration tests against **restored DB snapshot** (masked data).
- **Upgrade safety:** Comercial version pinned in README; migration checklist per Comercial release.

## Acceptance tests (must pass before production)

1. **Schema validation script** `scripts/validate-information-schema.sql` returns **no MISSING** rows on target Comercial version.
2. **Concept mapping query** (`05-concept-mapping.md`) returns **all** configured codes with non-null `CIDCONCEPTODOCUMENTO`.
3. **Golden batch parity** — reproduce **one** traced scenario from `09`: identical document keys / totals within tolerance defined with contador.
4. **Excel export parity** — same column set and plant/proyecto categorization rules as `REPORTE DE ESTADO DE CUENTA.sql` for a fixed fixture dataset.
5. **Failure modes** — invalid file → user-visible Spanish error; no partial commit without explicit transaction strategy.

## Explicit out of scope (initial release)

- CONTPAQi **Contabilidad** poliza authoring (unless distributor maps requirement).
- Cloud multi-tenant SaaS unless requested.
- Rewriting Comercial internal schema — integrate, do not fork.

## Dependencies

- Access to **staging Comercial** + **legal OK** to decompile/reimplement behavior.
- **CONTPAQi distributor** contact for SDK licensing/version matrix.
