# Environment and deployment inventory

**Inventory date:** 2026-04-24  
**Source bundle path (reference):** `/Users/juanj/Downloads/DCConcretos`  
**Documentation root (this repo):** `docs/dcconcretos-rebuild/`

Secrets in this document are **redacted**. Replace `«REDACTED»` with client-managed values only in secure stores, not in git.

## SQL Server (from `DCConcretosWF.exe.config`)

| Item | Value (redacted where sensitive) |
|------|----------------------------------|
| Provider | `System.Data.SqlClient` |
| Data Source | `«LAN_HOST»\comercial` (example pattern: named instance `\comercial`) |
| Initial Catalog | `adCONCRETOS_NUEVA` (CONTPAQi Comercial company DB naming pattern `ad` + name) |
| SQL auth | **Was** `sa` + password in plaintext config — **must not** be reused for rebuild |

**Not captured here (fill on client network):** SQL Server **version/edition**, Windows Server hostname, firewall rules, backup schedule.

## CONTPAQi Comercial

| Item | Status |
|------|--------|
| Product | CONTPAQi **Comercial** (Comercial Premium schema suggested by `adm*` tables in shipped SQL) |
| Exact build / Help→About | **Pending** — record from a workstation with Comercial installed next to this tool |
| ADD / other add-ons | **Pending** |

## Client runtime (.NET)

| Item | Value |
|------|--------|
| Target framework | **.NET Framework 4.5.2** (`supportedRuntime` sku in config) |
| `DCConcretosWF.exe` | PE32 **.NET** GUI assembly (verified `file(1)`) |
| Deployment layout | EXE + `DCConcretosWF.exe.config` + DLLs in same folder |

## Deployment folder listing (primary bundle)

Files observed in the live bundle directory alongside the executable:

- `DCConcretosWF.exe`
- `DCConcretosWF.exe.config`
- `ClosedXML.dll`
- `DocumentFormat.OpenXml.dll`
- `ExcelNumberFormat.dll`
- `Modelo.dll` (data access / domain strings reference `SqlConnection`, `SqlCommand`, `System.Data.SqlClient`)
- `Newtonsoft.Json.dll`
- `ConceptosEstadoCuentaProveedor.txt` (concept code list for SQL `IN (...)`)
- `REPORTE DE ESTADO DE CUENTA.sql` (report SQL template)
- Historical copies under `OLD 11112024/`, `OLD 16102024/`, `Old/`, `Old 18102024/`
- `Old/Newtonsoft.Json.xml` — XML doc file for Json.NET (not required at runtime)

## SHA-256 fingerprints (reproducibility)

Computed on 2026-04-24 for the primary bundle path:

| File | SHA-256 |
|------|---------|
| `DCConcretosWF.exe` | `23ff02d4c3e9c617f7ed6751dfdc2607c3ba3a5a2cae201a6398a7482d97d6c5` |
| `DCConcretosWF.exe.config` | `951f65983d397b3cab421878b6d05bc71e5cc392d823641f2a870065e621f052` |
| `ConceptosEstadoCuentaProveedor.txt` | `1fc67bf17ebf0a963afa00da514ba7fe0a4cd3b1dbe5e5f5050701be01481989` |
| `REPORTE DE ESTADO DE CUENTA.sql` | `1a1b8661a961fdce1b3d6a983c807ba60c5f322fc2791af9fa544f47bdafae62` |
| `ClosedXML.dll` | `b3985354e01d4faa597385421e618e556838f85b9577bd9cc75561f0ed7e4993` |
| `DocumentFormat.OpenXml.dll` | `211c0231d9bae955404e9ac05a7fe03d784c3365ef599c07012b42d00edb2fd5` |
| `ExcelNumberFormat.dll` | `27680288672ca8f9f8fcca0118d32f70d007394055f17e65239499c95b803b0d` |
| `Modelo.dll` | `23cda227f4be7e689658349f13000f3adba3aea99e830c57602c8920a7c33b2e` |
| `Newtonsoft.Json.dll` | `e1e27af7b07eeedf5ce71a9255f0422816a6fc5849a483c6714e1b472044fa9d` |

## `appSettings` keys (semantic)

| Key | Purpose (inferred) |
|-----|--------------------|
| `usuario` / `password` | Application-level credentials (possibly Comercial UI user or app gate) |
| `usuarioContpaqi` / `passwordContpaqi` | Comercial-related identity when `usaContpaqi` is active |
| `usaContpaqi` | `1` = use Contpaqi integration branch (exact mechanism: see `03-binary-analysis-notes.md`) |
| `nombreBDEmpresa` | Company database name aligned with SQL catalog |

## Operational (pending client input)

- **Which PC(s)** run the EXE  
- **Which Windows user** runs it (for SQL trace filtering)  
- **Whether** Excel paste (`btnPegarExcel`) is primary input vs file paths `rutaArchivoCompras`, `rutaArchivoDiesel`, `rutaReportesExcel`, `rutaArchivoInterfaz`
