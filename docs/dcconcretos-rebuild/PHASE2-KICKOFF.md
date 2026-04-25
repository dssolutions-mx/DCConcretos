# Phase 2 kickoff — rebuild execution checklist

Phase 2 is **software implementation**. Phase 1 delivers **evidence and specifications** in this folder.

## Entry criteria (before coding)

- [ ] Staging Comercial database restored and reachable from dev network
- [ ] `scripts/validate-information-schema.sql` — **no MISSING** columns
- [ ] `05-concept-mapping.md` table filled from live `admConceptos`
- [ ] At least **one** golden trace captured per `09-validation-protocol.md` (or explicit waiver signed by stakeholder acknowledging risk)

## Engineering tasks (ordered)

1. Export ILSpy projects for `DCConcretosWF.exe` and `Modelo.dll`; commit **read-only** reference export to private secure repo (not public if secrets embedded).
2. Implement configuration module matching logical keys in `01-environment-inventory.md` without plaintext `sa`.
3. Implement reporting path for estado de cuenta proveedor per `08-sql-inventory.md` (consider porting SQL to stored procedure with parameters instead of string replace).
4. Implement batch modules (`FrmCarga*` parity) using **SDK-first** write strategy per `10-rebuild-specification.md`.
5. Automated tests + contador UAT per acceptance tests in `10`.

## Exit criteria

- All acceptance tests in **`10-rebuild-specification.md`** pass on staging
- Production cutover plan approved (freeze window, rollback, credential rotation)
