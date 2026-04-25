# SQL inventory (Phase 1)

## External file: `REPORTE DE ESTADO DE CUENTA.sql`

- **Location:** shipped beside EXE (also duplicated under `OLD*/` and `Old*/` folders).
- **Purpose:** Supplier-oriented **account statement** style report with **product categorization** (cemento, grava, arena, etc.) and **plant (“Hoja”)** labeling derived from concept name patterns.
- **Parameters:**
  - `{0}` — **date from** (`CONVERT(DATE,A.CFECHA) BETWEEN '{0}' AND '{1}'`)
  - `{1}` — **date to**
  - `{2}` — **comma-separated list** of `CCODIGOCONCEPTO` literals; typically built from `ConceptosEstadoCuentaProveedor.txt`

### Important SQL characteristics

- Filters **`CPENDIENTE > 0`** (pending balance) on `admDocumentos`.
- Uses **`CIDCLIENTEPROVEEDOR`** join path (proveedor-side reporting).
- **Business overrides:** `CIDPROYECTO IN (8,9)` remaps category from `admProyectos.CNOMBREPROYECTO`.
- **Plant mapping** uses `CNOMBRECONCEPTO LIKE` patterns (`%1-%` … `%4-%`, `%JIANXING%`, `%5%` duplicated in source).

## Embedded SQL in binaries

- **Not extracted** as plain ASCII from `DCConcretosWF.exe` in this pass.
- **`Modelo.dll`** contains `SqlCommand`, `sqlQuery` member names but not full query text in `strings` output — **decompile `Modelo.dll`** for `CommandText` assignments.

## Action items for Phase 2 engineer

1. ILSpy search across solution export: `CommandText`, `StringBuilder`, `SELECT`, `INSERT`, `UPDATE`.
2. Map each SQL batch to calling form (`FrmCarga*` / `FrmEstado*`).
3. Mark each statement **read vs write** and expected isolation level.
