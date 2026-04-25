# User journeys and UI map (string-derived)

**Source:** `strings DCConcretosWF.exe` (2026-04-24). **Decompiler required** for exact control layout, tab order, and validation messages.

## Shell / navigation

- **`FrmConsola`** — `FrmConsola_Load` suggests a console-style host or log window for batch operations.
- **Menu: `cargasBatchToolStripMenuItem`** — entry point label for batch loads (Spanish copy not captured from binary).
- **`comprasToolStripMenuItem_Click`** — opens **`FrmCargaCompras`** (purchase batch).
- **`estadoDeCuentaProveedorToolStripMenuItem_Click`** — opens **`FrmEstadoDeCuentaProveedor`**.

## Form: supplier statement (`FrmEstadoDeCuentaProveedor`)

**Observed control / member names:**

- `cmbCliente`, `lblCliente`
- `cmbConcepto`, concept columns `colConcepto`
- `btnExportar`, `btnEnviarAComercial`, `btnPegarExcel`
- Data: `GetEstadoCuentaProveedor`, `CargarConceptos`, `MostrarConceptos`, `GetClientes`

**Journey (inferred):**

1. Choose **cliente/proveedor** and **concepto** filters.
2. Optionally **paste Excel** (`btnPegarExcel_Click`).
3. **Export** (`btnExportar_Click`) via `GenerarReporteExcel` / `ReporteEstadoCuentaExcel`.
4. Optionally **send to Comercial** (`btnEnviarAComercial_Click`) using `NombreUsuarioComercial` / `PasswordUsuarioComercial` / `NombreEmpresaComercial` settings.

## Form: purchase batch (`FrmCargaCompras`)

- `FrmCargaCompras_Load`
- `rutaArchivoCompras` — suggests **file-based** input path in config or user selection.

**Journey (inferred):** user selects or configures compras file → app validates → writes to Comercial via SQL/SDK (TBD).

## Form: diesel batch (`FrmCargaDiesel`)

- `FrmCargaDiesel_Load`
- `rutaArchivoDiesel`

## Form: remisiones batch (`FrmCargaRemisiones`)

- `FrmCargaRemisiones_Load`
- `colRemision`, `GetFoliosRemisiones`

## Error messages

**Pending:** capture **verbatim** Spanish strings from decompiled resources (`*.resources`) or by exercising UI on staging.

## Screenshots checklist (client UAT)

For each form above: full window, dropdown opened, error dialog example, successful completion state.
