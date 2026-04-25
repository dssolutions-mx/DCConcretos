# DCConcretos → CONTPAQi Comercial — Phase 1 documentation bundle

This folder implements **Phase 1** of the rebuild plan: **inventory, binary surface analysis, SQL/concept artifacts, validation protocol templates, and Phase 2 specification**.

## Contents

- `00-executive-summary.md` … `11-risks-and-dependencies.md` — narrative handoff
- `appendix-diagrams/` — Mermaid sources (`.mmd`) for architecture and batch flows
- `scripts/` — SQL templates for schema validation, staging login, Extended Events trace — see [`scripts/README.md`](scripts/README.md) and [`scripts/RUNBOOK-GOLDEN-TRACE.md`](scripts/RUNBOOK-GOLDEN-TRACE.md)
- `artifacts/` — copies of `REPORTE DE ESTADO DE CUENTA.sql` and `ConceptosEstadoCuentaProveedor.txt` plus a **sanitized** example config (no real secrets)
- `evidence/` — gitignored staging outputs (templates only committed); see [`evidence/README.md`](evidence/README.md)

## Repo location

This bundle is versioned under the **DCConcretos** repository at `docs/dcconcretos-rebuild/` (synced from the Cursor project that authored Phase 1).

## Canonical binary bundle (analyzed)

Path used for hashes/strings: deployment folder containing `DCConcretosWF.exe` (see `01-environment-inventory.md`).

## Next actions (client)

1. Run **`scripts/validate-information-schema.sql`** and **`05-concept-mapping.md`** query on a **DB copy**.
2. Execute **`09-validation-protocol.md`** on staging and attach golden results.
3. Begin Phase 2 using **`10-rebuild-specification.md`** and **`PHASE2-KICKOFF.md`**.
