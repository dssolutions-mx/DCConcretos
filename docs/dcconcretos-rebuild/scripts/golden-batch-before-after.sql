-- Template: snapshot high-signal keys before/after ONE legacy batch run on STAGING.
-- Edit table list after ILSpy identifies actual write targets.

-- BEFORE RUN
SELECT 'BEFORE' AS phase, GETDATE() AS ts;
SELECT COUNT(*) AS admDocumentos_count FROM dbo.admDocumentos;
SELECT COUNT(*) AS admMovimientos_count FROM dbo.admMovimientos;

-- TODO: add MAX(CIDDOCUMENTO), TOP 5 latest rows, etc.

-- (operator runs DCConcretosWF batch here)

-- AFTER RUN
SELECT 'AFTER' AS phase, GETDATE() AS ts;
SELECT COUNT(*) AS admDocumentos_count FROM dbo.admDocumentos;
SELECT COUNT(*) AS admMovimientos_count FROM dbo.admMovimientos;

-- TODO: SELECT rows WHERE CIDDOCUMENTO > @before_max_id
