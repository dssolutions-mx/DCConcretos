-- Template: create a non-sa login for staging trace + least privilege.
-- Adjust permissions after security review; do NOT grant dbo casually.

CREATE LOGIN dcconc_readtrace WITH PASSWORD = N'«STRONG_RANDOM_PASSWORD»';
GO

USE [adCONCRETOS_NUEVA]; -- TODO: staging DB
GO

CREATE USER dcconc_readtrace FOR LOGIN dcconc_readtrace;
GO

-- Minimum for read-only investigation:
ALTER ROLE db_datareader ADD MEMBER dcconc_readtrace;
GO

-- If the legacy app truly requires writes on staging only, add explicit grants per table
-- after identifying required objects from Extended Events / ILSpy (example only):
-- GRANT INSERT ON dbo.adDocumentos TO dcconc_readtrace;
-- GRANT INSERT ON dbo.adMovimientos TO dcconc_readtrace;
