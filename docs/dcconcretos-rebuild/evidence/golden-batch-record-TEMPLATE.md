# Golden batch record (template)

Fill after running `09-validation-protocol.md` on a **staging** database copy.

| Field | Value |
|-------|-------|
| Date/time (local) | |
| Operator Windows user | |
| Form / feature | (e.g. `FrmCargaCompras` minimal file) |
| Input file hash (SHA-256) | |
| SQL session id / XE file path | |
| Tables with new rows | |
| New `CIDDOCUMENTO` keys (sample) | |
| Contador sign-off (name / date) | |

## Notes

Attach anonymized XE excerpt path and before/after row counts from `scripts/golden-batch-before-after.sql`.
