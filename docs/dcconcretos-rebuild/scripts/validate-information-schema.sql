-- Run against CONTPAQi Comercial company DB (COPY / STAGING ONLY recommended).
-- Validates that columns referenced by REPORTE DE ESTADO DE CUENTA.sql exist.

DECLARE @cols TABLE (tbl sysname, col sysname);
INSERT INTO @cols VALUES
('admDocumentos','CIDDOCUMENTO'),
('admDocumentos','CFECHA'),
('admDocumentos','CSERIEDOCUMENTO'),
('admDocumentos','CFOLIO'),
('admDocumentos','CIDCONCEPTODOCUMENTO'),
('admDocumentos','CIDCLIENTEPROVEEDOR'),
('admDocumentos','CFECHAVENCIMIENTO'),
('admDocumentos','CTIPOCAMBIO'),
('admDocumentos','CPENDIENTE'),
('admDocumentos','CTOTAL'),
('admDocumentos','CIDMONEDA'),
('admDocumentos','CIDPROYECTO'),
('admConceptos','CIDCONCEPTODOCUMENTO'),
('admConceptos','CCODIGOCONCEPTO'),
('admConceptos','CNOMBRECONCEPTO'),
('admClientes','CIDCLIENTEPROVEEDOR'),
('admClientes','CRAZONSOCIAL'),
('admMovimientos','CIDDOCUMENTO'),
('admMovimientos','CIDPRODUCTO'),
('admMovimientos','CNETO'),
('admProductos','CIDPRODUCTO'),
('admProductos','CNOMBREPRODUCTO'),
('admProductos','CCODIGOPRODUCTO'),
('admProyectos','CIDPROYECTO'),
('admProyectos','CNOMBREPROYECTO');

SELECT c.TABLE_SCHEMA, c.TABLE_NAME, c.COLUMN_NAME, 'OK' AS status
FROM INFORMATION_SCHEMA.COLUMNS AS c
INNER JOIN @cols AS x
  ON x.tbl = c.TABLE_NAME AND x.col = c.COLUMN_NAME
ORDER BY c.TABLE_NAME, c.COLUMN_NAME;

SELECT x.tbl, x.col, 'MISSING' AS status
FROM @cols AS x
WHERE NOT EXISTS (
  SELECT 1
  FROM INFORMATION_SCHEMA.COLUMNS AS c
  WHERE c.TABLE_NAME = x.tbl AND c.COLUMN_NAME = x.col
);
