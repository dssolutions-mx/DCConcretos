# Document concept code mapping

## Source list (`ConceptosEstadoCuentaProveedor.txt`)

Codes shipped for SQL `IN (...)` substitution (includes a likely typo duplicate `C0015` vs `C015`):

`C001` … `C015`, `C0015`, `SP001` … `SP005`, `C0022025`, `C0032025`, `C0082025`, `C0092025`, `C0112025`, `C0132025`

## Purpose

These **`CCODIGOCONCEPTO`** values restrict **which document concepts** participate in **supplier account-statement** style reporting (`REPORTE DE ESTADO DE CUENTA.sql`). They do **not** by themselves prove “only purchases” globally — they define **which Comercial document types** this report treats as in-scope for **proveedor** balances.

## Run on company database (copy) — resolve to rows

Execute on a **restored backup** of the Comercial company database (replace database name if different):

```sql
USE [adCONCRETOS_NUEVA]; -- change if needed

DECLARE @codes TABLE (codigo VARCHAR(30));
INSERT INTO @codes (codigo) VALUES
('C001'),('C002'),('C003'),('C004'),('C005'),('C006'),('C007'),('C008'),('C009'),
('C010'),('C011'),('C012'),('C013'),('C014'),('C015'),('C0015'),
('SP001'),('SP002'),('SP003'),('SP004'),('SP005'),
('C0022025'),('C0032025'),('C0082025'),('C0092025'),('C0112025'),('C0132025');

SELECT
  c.CIDCONCEPTODOCUMENTO,
  c.CCODIGOCONCEPTO,
  c.CNOMBRECONCEPTO
  -- Add version-specific columns here after INFORMATION_SCHEMA check, e.g. naturaleza / flags
FROM admConceptos AS c
INNER JOIN @codes AS x ON x.codigo = c.CCODIGOCONCEPTO
ORDER BY c.CCODIGOCONCEPTO;
```

## Populate this table after you run the query

| CCODIGOCONCEPTO | CIDCONCEPTODOCUMENTO | CNOMBRECONCEPTO (from DB) | Purchase vs sale? (accounting) | Notes |
|-----------------|----------------------|---------------------------|--------------------------------|-------|
| *(pending)* | | | | Fill after query |

## `C0015` vs `C015`

Treat as **data quality** item: confirm whether both exist in `admConceptos`. If only `C015` exists, fix the text file or app config source to avoid silent omission.
