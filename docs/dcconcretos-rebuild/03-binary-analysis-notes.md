# Binary analysis notes (DCConcretosWF + Modelo)

**Methods used:** `file(1)`, `strings(1)`, SHA-256 inventory, directory listing. **Recommended follow-up on Windows:** ILSpy / dnSpyEx / dotPeek for IL-level decompilation (full method bodies, embedded resources, exact SQL text).

## Assembly inventory (deployment)

| Assembly | Role (inferred) |
|----------|-----------------|
| `DCConcretosWF.exe` | WinForms host; menus and forms |
| `Modelo.dll` | Data layer — `SqlConnection`, `SqlCommand`, domain getters/setters for documentos/conceptos/proveedor |
| `ClosedXML.dll` | Excel generation / manipulation |
| `DocumentFormat.OpenXml.dll` | Office Open XML primitives |
| `ExcelNumberFormat.dll` | Number format helpers for spreadsheets |
| `Newtonsoft.Json.dll` | JSON serialization (API or config payloads — confirm in decompiler) |

**Not observed in strings:** `Contpaqi*`, `ARSoftware*`, explicit `admDocumentos` / `SELECT` text inside `DCConcretosWF.exe` (SQL may live in `Modelo.dll` resources, embedded as UTF-16, or loaded from files at runtime).

## WinForms / namespaces (from `DCConcretosWF.exe` strings)

- `DCConcretosWF.Formularios.FrmEstadoDeCuentaProveedor`
- `DCConcretosWF.Formularios.FrmCargaCompras`
- `DCConcretosWF.Formularios.FrmCargaDiesel`
- `DCConcretosWF.Formularios.FrmCargaRemisiones`
- `DCConcretosWF.Formularios.FrmConsola`
- `DCConcretosWF.Reportes.Excel.ReporteEstadoCuentaExcel` + `GenerarReporteExcel`

## Menu / handler names (behavior map)

| String token | Likely UX / behavior |
|--------------|----------------------|
| `estadoDeCuentaProveedorToolStripMenuItem_Click` | Opens supplier statement UI |
| `btnEnviarAComercial_Click` | Pushes data into Comercial workflow (mechanism TBD) |
| `comprasToolStripMenuItem_Click` | Opens purchase batch UI |
| `cargasBatchToolStripMenuItem` | Parent menu for batch loads |
| `btnPegarExcel_Click` | Clipboard → grid / sheet ingestion |
| `btnExportar_Click` | Export current view/report |

## Modelo.dll — data layer hints

Notable identifiers: `GetEstadoCuentaProveedor`, `GetConceptos`, `GetConceptosByCodigo`, `GetConceptosByID`, `GetDocumento`, `GetClientes`, `GetFoliosRemisiones`, `codigosConceptos`, `rutaArchivoCompras`, `rutaArchivoDiesel`, `rutaArchivoInterfaz`, `rutaReportesExcel`.

**Interpretation:** batch flows likely read **Excel or interface files** from configured paths and map rows into Comercial document structures via SQL (and possibly SDK if `usaContpaqi` triggers a different code path not visible as plain strings).

## Correction to prior “purchases only” narrative

Strings show **multiple batch surfaces** (`FrmCargaCompras`, `FrmCargaDiesel`, `FrmCargaRemisiones`). If operators only used **Compras** in practice, that is a **usage** pattern—not proof the EXE only contains purchase logic.

## Open questions (require decompiler or runtime trace)

1. Exact **T-SQL** text for each form’s read/write paths (including dynamic SQL).
2. Whether **`usaContpaqi`** invokes **SDK**, **COM**, **file-based interfaz**, or **SQL only**.
3. Schema of **`rutaArchivoInterfaz`** file (CSV/XML/custom).
4. Error handling and transaction boundaries (single `SqlTransaction` vs per-row).
5. Whether **`btnEnviarAComercial`** requires Comercial executable running on same machine.

## ILSpy export checklist (Windows)

1. Open `DCConcretosWF.exe` + `Modelo.dll` in ILSpy.
2. Export C# project to a throwaway folder; search for `SqlCommand`, `CommandText`, `ExecuteNonQuery`, `Transaction`.
3. List **every** `FROM` / `INTO` / `JOIN` table reference.
4. Capture screenshots of form designer resource usage for labels (Spanish prompts).
