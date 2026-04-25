# Performance, packaging, and cutover

## Profiling (after Milestone 1–2)

- Use **Visual Studio Diagnostic Tools** (CPU + memory) while running a large estado de cuenta date range and Excel export.
- If **memory** grows with export size, switch Excel path to **Open XML SDK** streaming writer (keep ClosedXML for smaller exports if preferred).
- For **SQL**: capture actual plan in SSMS; avoid per-row loops in new code; keep `CommandTimeout` explicit (see `SqlEstadoCuentaProveedorReportingService`).

## Connection pooling

- `Microsoft.Data.SqlClient` pools by default; reuse one **connection string** from configuration; avoid creating excessive `SqlConnection` scopes inside inner loops.

## Deployment options

1. **Folder publish** — `dotnet publish src/DcConcretos.Desktop -c Release -r win-x64 --self-contained false` (requires .NET 8 runtime on PCs).
2. **Self-contained** — add `-p:SelfContained=true` for machines without shared runtime (larger footprint).
3. **MSIX** — optional; use when enterprise install / auto-update is required (WinUI 3 packaging docs also apply to WPF with Windows App SDK packaging projects).

## Secrets

- Production: **User Secrets** (dev), **DPAPI-protected config**, or **Windows Credential Manager** — never ship `appsettings.Local.json` with passwords to git.
- Rotate credentials exposed in legacy `DCConcretosWF.exe.config` copies.

## Cutover checklist

See [`docs/dcconcretos-rebuild/PHASE2-KICKOFF.md`](dcconcretos-rebuild/PHASE2-KICKOFF.md) exit criteria: acceptance tests in `10-rebuild-specification.md`, rollback plan, freeze window for legacy EXE.
