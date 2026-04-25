#Requires -Version 5.1
<#
.SYNOPSIS
  Runs validate-information-schema.sql via sqlcmd when DC_SQL_* env vars are set.

.EXAMPLE
  $env:DC_SQL_SERVER = "localhost\SQLEXPRESS"
  $env:DC_SQL_DATABASE = "adCONCRETOS_NUEVA_STAGING"
  $env:DC_SQL_USER = "dcconc_readtrace"
  $env:DC_SQL_PASSWORD = "***"
  .\tools\Run-SchemaValidation.ps1

  Output: paste into docs/dcconcretos-rebuild/evidence/schema-validation-results-TEMPLATE.md (copy, redact).
#>
$ErrorActionPreference = "Stop"
$root = (Resolve-Path "$PSScriptRoot\..").Path

$server = $env:DC_SQL_SERVER
$database = $env:DC_SQL_DATABASE
$user = $env:DC_SQL_USER
$password = $env:DC_SQL_PASSWORD

if (-not $server -or -not $database -or -not $user -or -not $password) {
  Write-Host "Set DC_SQL_SERVER, DC_SQL_DATABASE, DC_SQL_USER, DC_SQL_PASSWORD then re-run."
  Write-Host "Script path: $PSScriptRoot\..\docs\dcconcretos-rebuild\scripts\validate-information-schema.sql"
  exit 0
}

$sqlFile = Join-Path $root "docs\dcconcretos-rebuild\scripts\validate-information-schema.sql"
if (-not (Test-Path $sqlFile)) { throw "Missing $sqlFile" }

& sqlcmd -S $server -d $database -U $user -P $password -b -i $sqlFile
