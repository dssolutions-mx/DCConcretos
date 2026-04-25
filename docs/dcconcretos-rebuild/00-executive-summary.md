# DCConcretosWF — Executive summary

## What this is

**DCConcretosWF** is a third-party **Windows desktop tool** (.NET Framework 4.5.2, WinForms) that works alongside **CONTPAQi Comercial** (SQL Server company database). It is **not** part of CONTPAQi’s official product line; it was custom-built for **DC Concretos** and the original developer is no longer available.

## What it appears to do (evidence-based)

- **Supplier account statement (“estado de cuenta proveedor”)** — UI form `FrmEstadoDeCuentaProveedor`, export to Excel (ClosedXML), optional action **“Enviar a Comercial”** tied to Comercial credentials in config.
- **Batch loading flows** — separate forms discovered in the binary: **`FrmCargaCompras`** (purchases), **`FrmCargaDiesel`**, **`FrmCargaRemisiones`**, plus a console form **`FrmConsola`**. Menu item **`cargasBatchToolStripMenuItem`** indicates explicit batch operations.
- **Data access** — `System.Data.SqlClient` appears in companion **`Modelo.dll`** (no Contpaqi/ARSoftware assembly names found in string scans). **`usaContpaqi=1`** in config suggests an intended integration path with Comercial; **confirm at runtime** whether the SDK is invoked or only SQL + UI automation.

## Main risks

- **Vendor abandonment** — no source, unknown full write behavior until traced on a **database copy**.
- **Security** — historical configs used **`sa`** and plaintext passwords (must be rotated and replaced for any future work).
- **CONTPAQi upgrades** — schema and business rules in Comercial can change; a replacement must target a **pinned Comercial version** or use the **supported SDK** abstraction.

## Recommendation

1. **Short term:** Operate legacy EXE only on a controlled PC; **rotate credentials**; use **`01`–`09`** in this folder to capture environment and (on staging) one **golden traced batch**.
2. **Medium term:** **Rebuild** a maintainable app using **`10-rebuild-specification.md`** — default architecture: **Comercial SDK (or ARSoftware-aligned patterns) for writes**, SQL optional for reporting; **never** replicate document creation with undocumented raw SQL unless explicitly accepted as risk.

## Phase 2 scope (high level)

Implement parity for: estado de cuenta proveedor reporting/export, batch cargas (compras / diesel / remisiones per current menus), configuration model equivalent to `.exe.config`, audit logging, and non-`sa` SQL access — with acceptance tests tied to **`09-validation-protocol.md`**.
