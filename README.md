# DC Concretos — Comercial integration (replacement desktop)

This repository contains:

- **Phase 1 documentation** under [`docs/dcconcretos-rebuild/`](docs/dcconcretos-rebuild/) (CONTPAQi Comercial analysis, validation scripts, rebuild spec).
- **New .NET 8 WPF** solution [`DcConcretos.sln`](DcConcretos.sln): read-only **estado de cuenta proveedor** report + Excel export; batch write paths are **SDK stubs** until Phase 2 integration.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) on Windows.
- Network access to SQL Server hosting the CONTPAQi Comercial company database (staging first).

## Configuration

1. Copy `src/DcConcretos.Desktop/appsettings.json` to `appsettings.Local.json` next to it **or** use `dotnet user-secrets set "SqlReporting:ConnectionString" "..."` in the Desktop project.
2. Set `SqlReporting:ConnectionString` to a **least-privilege** login (not `sa`).

## Build & run

```powershell
dotnet restore DcConcretos.sln
dotnet build DcConcretos.sln -c Release
dotnet run --project src/DcConcretos.Desktop -c Release
```

## Tests

```powershell
dotnet test DcConcretos.sln -c Release
```

CI runs on **windows-latest** (see [`.github/workflows/build.yml`](.github/workflows/build.yml)).

## Legacy artifacts

Original deployment files (`REPORTE DE ESTADO DE CUENTA.sql`, `ConceptosEstadoCuentaProveedor.txt`, configs) remain in the repo root / `OLD*` folders for reference.
