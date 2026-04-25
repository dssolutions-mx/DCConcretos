# Golden trace runbook (Phase 1b)

Follow [`09-validation-protocol.md`](../09-validation-protocol.md) on a **staging** SQL instance and Windows VM.

## Steps

1. Restore company database backup to staging; create login per `staging-sql-login-template.sql`.
2. Deploy legacy `DCConcretosWF.exe` + DLLs with **sanitized** config pointing at staging (no `sa`).
3. Apply `extended-events-batch-trace.sql` (edit database / login names); start session.
4. Run **one** minimal successful batch (e.g. Compras) from the legacy app.
5. Stop session; export XE to a secure path under `evidence/` (gitignored).
6. Run `golden-batch-before-after.sql` before/after (extend table list after ILSpy).
7. Fill `evidence/golden-batch-record-TEMPLATE.md` (copy to a local filename) including **contador** UI verification.

## Definition of done

- Extended Events file path recorded (not committed if sensitive).
- Before/after diff attached or summarized.
- Contador sign-off line completed.
