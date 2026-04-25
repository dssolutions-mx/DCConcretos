-- Template: Extended Events session to capture SQL from DCConcretosWF / Modelo layer.
-- EDIT: database name, login name, file path before executing on STAGING.

IF EXISTS (SELECT * FROM sys.server_event_sessions WHERE name = N'dcconc_app_sql')
  DROP EVENT SESSION dcconc_app_sql ON SERVER;
GO

CREATE EVENT SESSION dcconc_app_sql ON SERVER
ADD EVENT sqlserver.sql_batch_completed(
  ACTION (sqlserver.sql_text, sqlserver.session_id, sqlserver.username, sqlserver.database_name)
  WHERE (
    sqlserver.database_name = N'adCONCRETOS_NUEVA' -- TODO: staging DB name
    AND sqlserver.username = N'dcconc_readtrace' -- TODO: actual SQL login used by app
  )
),
ADD EVENT sqlserver.rpc_completed(
  ACTION (sqlserver.sql_text, sqlserver.session_id, sqlserver.username, sqlserver.database_name)
  WHERE (
    sqlserver.database_name = N'adCONCRETOS_NUEVA'
    AND sqlserver.username = N'dcconc_readtrace'
  )
)
ADD TARGET package0.event_file
(
  SET filename = N'C:\Temp\dcconc_app_sql.xel' -- TODO: writable path on SQL host
);
GO

ALTER EVENT SESSION dcconc_app_sql ON SERVER STATE = START;
GO

-- After reproducing one batch:
-- ALTER EVENT SESSION dcconc_app_sql ON SERVER STATE = STOP;
-- Inspect .xel with SSMS: Management → Extended Events → Open → Merge files
