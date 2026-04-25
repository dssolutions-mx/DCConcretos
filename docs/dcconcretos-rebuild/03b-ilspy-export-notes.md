# ILSpy / dnSpyEx export notes (Phase 1b)

**Purpose:** Close open questions listed in [`03-binary-analysis-notes.md`](./03-binary-analysis-notes.md) with IL-level evidence from `DCConcretosWF.exe` and `Modelo.dll`.

## Prerequisites (Windows staging VM)

- ILSpy 8+ or dnSpyEx, same folder as production deployment: `DCConcretosWF.exe`, `Modelo.dll`, and all satellite DLLs.
- Copy assemblies to a **non-production** path before export.

## Export procedure

1. Open **ILSpy** → *File* → *Open* → select `DCConcretosWF.exe` and `Modelo.dll`.
2. Right-click assembly → **Save Code…** → choose output folder under `docs/dcconcretos-rebuild/evidence/ilspy-export/` (local machine only; folder is gitignored).
3. Open generated `.csproj` in Visual Studio or search in VS Code across export root.

## Search checklist (record hits in your secure notes)

| Search term | What to capture |
|-------------|-----------------|
| `CommandText` | Every assignment; copy full SQL or builder pattern. |
| `ExecuteNonQuery` / `ExecuteScalar` / `ExecuteReader` | Call sites → form or service name. |
| `SqlTransaction` / `BeginTransaction` | Transaction boundaries per batch. |
| `INSERT` / `UPDATE` / `DELETE` | Table names; map to read vs write. |
| `Contpaqi` / `ARSoftware` / `SDK` / `Interop` | Confirms or denies SDK path for `usaContpaqi`. |
| `rutaArchivo` | All path keys and file format assumptions. |
| `FrmCarga` | Handlers tying UI → `Modelo` calls. |

## Outputs to merge back into Phase 1 docs

1. Append resolved questions to this file (section below).
2. Update [`07-batch-input-spec.md`](./07-batch-input-spec.md) with exact column layouts once known.
3. Update [`04-data-dictionary-comercial.md`](./04-data-dictionary-comercial.md) write-path table list from INSERT targets.

## Resolved questions (fill after export)

| # | Question (from 03) | Answer / evidence pointer |
|---|----------------------|---------------------------|
| 1 | Exact T-SQL per form | |
| 2 | Meaning of `usaContpaqi` | |
| 3 | `rutaArchivoInterfaz` schema | |
| 4 | Transaction strategy | |
| 5 | `btnEnviarAComercial` requirements | |
