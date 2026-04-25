# Validation protocol (staging) — SQL trace + golden batch

**Principle:** Never trace write activity against **production** until a **restored copy** of the database is validated.

## Preconditions

1. Restore backup of `adCONCRETOS_NUEVA` (or current name) to **staging SQL instance**.
2. Create dedicated SQL login **`dcconc_readtrace`** (see `scripts/staging-sql-login-template.sql`) — avoid `sa`.
3. Configure **sanitized** `DCConcretosWF.exe.config` on a **staging Windows VM** pointing at staging DB.
4. Identify **Windows user** and **SQL login** the app uses (for Extended Events session predicate).

## Step A — Extended Events session (SQL Server)

Create a session that captures **batch_text** and **rpc_completed** for the tool’s SQL principal.

**Script:** `scripts/extended-events-batch-trace.sql` (edit database name / login name before run).

**Artifacts to save:** `.xel` file or export to table; attach **time window**, **login name**, and **operation** (Compras/Diesel/Remisiones/Estado cuenta).

## Step B — Process Monitor (optional, Windows)

- Filter: **Process Name** is `DCConcretosWF.exe`
- Capture: **File** operations on `rutaArchivo*` paths, temp Excel files, `rutaArchivoInterfaz`.

## Step C — Golden mechanical footprint (one minimal success)

Pick the **smallest** batch the business accepts (e.g. one compra row).

**Before run:**

1. Note `MAX(CIDDOCUMENTO)` (or relevant surrogate) from expected target tables **if** inserts are suspected — table list TBD from decompile; start with:
   - `admDocumentos`
   - `admMovimientos`
   - any interface staging tables discovered in trace.

**After run:**

1. Re-query same aggregates; diff **counts** and **sample primary keys** of new rows.
2. Attach **screenshots** from Comercial UI showing the new document.

### Golden batch record (template)

| Field | Value |
|-------|-------|
| Date/time (local) | |
| Operator Windows user | |
| Form / feature | (e.g. `FrmCargaCompras` minimal file) |
| Input file hash (SHA-256) | |
| SQL session id / XE file path | |
| Tables with new rows | |
| New `CIDDOCUMENTO` keys | |
| Contador sign-off (name/date) | |

## Step D — Accounting validation

With **contador**, compare:

- Totals per document vs source file
- VAT / withholding expectations (if applicable)
- Inventory impact flags in Comercial for the chosen concept

## Current Phase 1 status

- **Trace templates:** provided under `scripts/`.
- **Populated golden record:** **pending execution on client staging** — this repository cannot reach `192.168.x.x` SQL hosts. When the client completes Step C/D, paste results into this section or attach as `09-appendix-golden-run.pdf`.
