# CONTPAQi Comercial — data touch list (Phase 1)

This document lists **tables and columns referenced by shipped artifacts** and **UI/domain hints** from binaries. It is **not** a full Comercial data dictionary.

Legend: **R** = read in shipped SQL, **W** = write suspected from app (needs trace), **?** = unknown until decompile/trace.

## Tables from `REPORTE DE ESTADO DE CUENTA.sql`

| Table | Usage | Columns referenced (non-exhaustive) |
|-------|-------|-------------------------------------|
| `admDocumentos` **R** | Document header filter + join for proyecto | `CIDDOCUMENTO`, `CFECHA`, `CSERIEDOCUMENTO`, `CFOLIO`, `CIDCONCEPTODOCUMENTO`, `CIDCLIENTEPROVEEDOR`, `CFECHAVENCIMIENTO`, `CTIPOCAMBIO`, `CPENDIENTE`, `CTOTAL`, `CIDMONEDA`, `CIDPROYECTO` |
| `admConceptos` **R** | Filter by concept code list `{2}` | `CIDCONCEPTODOCUMENTO`, `CCODIGOCONCEPTO`, `CNOMBRECONCEPTO` |
| `admClientes` **R** | Supplier / counterparty display | `CIDCLIENTEPROVEEDOR`, `CRAZONSOCIAL` |
| `admMovimientos` **R** | Line-level net amounts | `CIDDOCUMENTO`, `CIDPRODUCTO`, `CNETO` |
| `admProductos` **R** | Product names/codes for categorization | `CIDPRODUCTO`, `CNOMBREPRODUCTO`, `CCODIGOPRODUCTO` |
| `admProyectos` **R** | Override category label for specific projects | `CIDPROYECTO`, `CNOMBREPROYECTO` |

## Temporary tables (report only)

`#documentos`, `#documentos2`, `#detalleProducto`, `#detalleProducto2`, `#detalleProducto3`, `#documentos3`, `#SinCategoria`, `#categoriasAgregar` — all **session temp tables** inside the report batch.

## Hardcoded business constants in SQL (must be preserved or parameterized in rebuild)

- **Project IDs:** `CIDPROYECTO IN (8, 9)` — special-case mapping to project name as `Categoria`.
- **Plant / “Hoja” rules:** `CNOMBRECONCEPTO LIKE '%1-%'` … `'%4-%'`, `'%JIANXING%'`, `'%5%'` — operational labeling for spreadsheets.

## Binary-derived entities (`Modelo.dll` / `DCConcretosWF.exe` strings)

Domain properties include: `CIDDocumento`, `IDDocumento`, `SerieDocumento`, `CIDConceptoDocumento`, `CodigoConcepto`, `NombreConcepto`, `Proveedor`, `IdClienteProveedor`, `CodigoClienteProveedor`, `ListadoDocumentos`, `FechaMovimiento`, `NoMovimiento`, `UnidadMedida`, `ProveedorCategoria`, `GetFoliosRemisiones`.

**Interpretation:** batch modules likely create or match **documents** and **movements** consistent with Comercial’s document model — exact tables for **writes** must be confirmed by trace (`09`).

## Client validation SQL (run on DB copy)

See `scripts/validate-information-schema.sql` to compare **live** `INFORMATION_SCHEMA.COLUMNS` against this list for your Comercial version.
