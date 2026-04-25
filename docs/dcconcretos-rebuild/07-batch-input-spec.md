# Batch input specification (inferred + placeholders)

**Status:** Partial — exact column layouts are **not** visible in ASCII strings alone. Complete this document after **ILSpy resource inspection** or by **recording a template file** from a successful operator run on staging.

## Known configuration keys (string evidence)

| Key / member | Meaning |
|--------------|---------|
| `rutaArchivoCompras` | Path or pattern for **Compras** batch source file |
| `rutaArchivoDiesel` | Path for **Diesel** batch source file |
| `rutaArchivoInterfaz` | Unknown format — likely **intercambio / interfaz** file for Comercial or middleware |
| `rutaReportesExcel` | Output folder for generated Excel reports |

## Excel clipboard path (`FrmEstadoDeCuentaProveedor`)

- Handler: `btnPegarExcel_Click`
- Stack: **ClosedXML** + **DocumentFormat.OpenXml**

**Hypothesis:** pasted grid maps to document lines similar to `admMovimientos` / product lines — confirm by decompiling paste handler.

## Synthetic example tables (fill after discovery)

### Compras file (unknown columns)

| Column name (TBD) | Type | Required | Notes |
|-------------------|------|----------|-------|
| … | … | … | Capture from Modelo mapping code |

### Diesel file (unknown columns)

| Column name (TBD) | Type | Required | Notes |
|-------------------|------|----------|-------|
| … | … | … | |

### Remisiones file (unknown columns)

| Column name (TBD) | Type | Required | Notes |
|-------------------|------|----------|-------|
| … | … | … | `GetFoliosRemisiones` suggests folio/series logic |

## Validation rules (TBD)

Document after decompile:

- Required fields per row
- Duplicate detection (folio/serie)
- Currency / tipo de cambio handling
- Interaction with `admConceptos` for allowed document types
