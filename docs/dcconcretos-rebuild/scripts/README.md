# SQL scripts (Phase 1b / Phase 2)

| Script | Purpose |
|--------|---------|
| `validate-information-schema.sql` | Confirms columns used by estado de cuenta SQL exist (`MISSING` result set must be empty). |
| `extended-events-batch-trace.sql` | Template for capturing legacy `DCConcretosWF.exe` write batches on staging. |
| `staging-sql-login-template.sql` | Least-privilege login pattern (replace `sa`). |
| `golden-batch-before-after.sql` | Row-count / key snapshot before vs after one batch. |

## Running validation from Windows

Use SSMS against a **database copy**, or from PowerShell:

```powershell
.\tools\Run-SchemaValidation.ps1
```

Requires environment variables `DC_SQL_SERVER`, `DC_SQL_DATABASE`, `DC_SQL_USER`, `DC_SQL_PASSWORD` (see script header).

Paste redacted output into `evidence/schema-validation-results-TEMPLATE.md` (copy locally; folder is gitignored except templates).
