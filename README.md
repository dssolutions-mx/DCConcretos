# DC Concretos — CONTPAQi Comercial (replacement desktop)

This repository replaces the legacy **DCConcretosWF** WinForms tool with a **.NET 8 WPF** application: **read-only** supplier **estado de cuenta** reporting (parameterized SQL + Excel export). **Batch writes** (Compras / Diesel / Remisiones) are **stubs** until CONTPAQi Comercial SDK integration is completed on staging.

Full analysis, scripts, and acceptance criteria live in [`docs/dcconcretos-rebuild/`](docs/dcconcretos-rebuild/) (start with [`PHASE2-KICKOFF.md`](docs/dcconcretos-rebuild/PHASE2-KICKOFF.md)).

---

## Step-by-step: first-time setup (Windows)

Do these in order on a machine that can reach your **SQL Server** (use a **staging** database first, not production).

### Step 1 — Install prerequisites

1. Install **[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)** (x64).
2. Install **[Git for Windows](https://git-scm.com/download/win)** (optional if you already use Git).
3. Install **[SQL Server Management Studio](https://learn.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms)** (or `sqlcmd`) to run validation scripts.

### Step 2 — Clone this repository

```powershell
cd $env:USERPROFILE\source\repos   # or any folder you prefer
git clone https://github.com/dssolutions-mx/DCConcretos.git
cd DCConcretos
```

If the URL above 404s, replace it with the URL shown on your GitHub repository page after publish.

### Step 3 — Restore and verify the build

```powershell
dotnet restore DcConcretos.sln
dotnet build DcConcretos.sln -c Release
dotnet test DcConcretos.sln -c Release --no-build
```

All tests should pass without a database (they check SQL file deployment and parsers).

### Step 4 — Configure the SQL connection (no plaintext in git)

**Do not** commit passwords. Pick **one** of these:

**Option A — User secrets (recommended for developers)**

```powershell
cd src\DcConcretos.Desktop
dotnet user-secrets init   # only if not already initialized
dotnet user-secrets set "SqlReporting:ConnectionString" "Server=YOURSERVER;Database=YOUR_COMPANY_DB;User Id=YOUR_LOGIN;Password=YOUR_PASSWORD;TrustServerCertificate=True"
cd ..\..
```

**Option B — Local config file**

1. Copy `src\DcConcretos.Desktop\appsettings.json` to `src\DcConcretos.Desktop\appsettings.Local.json`.
2. Set `SqlReporting:ConnectionString` to a **least-privilege** SQL login (avoid `sa`).
3. Confirm `appsettings.Local.json` is listed in `.gitignore` (it is not tracked by default only if you add it — **do not** `git add` files containing secrets).

### Step 5 — (Recommended before production) Phase 1b on staging

On a **copy** of the company database and a **staging** PC:

1. Follow [`docs/dcconcretos-rebuild/03b-ilspy-export-notes.md`](docs/dcconcretos-rebuild/03b-ilspy-export-notes.md) to decompile the legacy EXE/DLL and document write paths.
2. Run [`docs/dcconcretos-rebuild/scripts/validate-information-schema.sql`](docs/dcconcretos-rebuild/scripts/validate-information-schema.sql) in SSMS; the `MISSING` result set must be empty.
3. Follow [`docs/dcconcretos-rebuild/scripts/RUNBOOK-GOLDEN-TRACE.md`](docs/dcconcretos-rebuild/scripts/RUNBOOK-GOLDEN-TRACE.md) for one **golden** legacy batch trace.
4. Optionally use [`tools/Run-SchemaValidation.ps1`](tools/Run-SchemaValidation.ps1) with env vars `DC_SQL_SERVER`, `DC_SQL_DATABASE`, `DC_SQL_USER`, `DC_SQL_PASSWORD`.

Entry criteria for serious SDK work are summarized in [`docs/dcconcretos-rebuild/PHASE2-KICKOFF.md`](docs/dcconcretos-rebuild/PHASE2-KICKOFF.md).

### Step 6 — Run the desktop app

```powershell
dotnet run --project src\DcConcretos.Desktop -c Release
```

In the window:

1. Choose **Desde** / **Hasta** dates.
2. Confirm or edit **códigos de concepto** (defaults load from `Assets\ConceptosEstadoCuentaProveedor.txt`).
3. Click **Ejecutar reporte** — results appear in the grid when SQL returns rows.
4. Click **Exportar Excel…** to save an `.xlsx` file.

**Cargas batch** menu items show placeholder messages until the Comercial SDK is integrated.

### Step 7 — Security housekeeping

- If you ever stored **live** `sa` or passwords in a shared `DCConcretosWF.exe.config`, **rotate** those credentials and move to least-privilege logins.
- Never commit `appsettings.Local.json`, user secrets, or Extended Events files that may contain sensitive SQL.

---

## Repository layout (short)

| Path | Purpose |
|------|---------|
| [`docs/dcconcretos-rebuild/`](docs/dcconcretos-rebuild/) | Phase 1 docs, SQL scripts, evidence templates |
| [`src/DcConcretos.Desktop/`](src/DcConcretos.Desktop/) | WPF UI |
| [`src/DcConcretos.Infrastructure/`](src/DcConcretos.Infrastructure/) | SQL + Excel |
| [`src/DcConcretos.Integrations.Comercial/`](src/DcConcretos.Integrations.Comercial/) | Batch writer interfaces (stubs) |
| [`tests/DcConcretos.Tests/`](tests/DcConcretos.Tests/) | Unit tests |
| [`.github/workflows/build.yml`](.github/workflows/build.yml) | CI on `windows-latest` |

**CI troubleshooting (common fixes):**

- **ClosedXML / `XLCellValue`:** cell values must not assign a raw `object`; use `Convert.ToString` (or `XLCellValue.FromObject` where supported).
- **`Application` is a namespace:** the project `DcConcretos.Application` makes the name `Application` ambiguous inside `DcConcretos.Desktop` — inherit from `System.Windows.Application` explicitly in `App.xaml.cs`.
- **Runner OS:** WPF targets `net8.0-windows`; GitHub Actions must use `windows-latest`, not `ubuntu-latest`.

---

## Legacy files on disk

Older copies of SQL/config may exist next to this clone (`OLD*`, `Old*`, `REPORTE*.sql` at repo root). Canonical copies for documentation are under [`docs/dcconcretos-rebuild/artifacts/`](docs/dcconcretos-rebuild/artifacts/). Prefer those paths in documentation and CI.

---

## License / CONTPAQi

CONTPAQi Comercial is third-party software. SDK licensing and supported versions must be confirmed with your **CONTPAQi distributor** before shipping batch writes. See [`docs/dcconcretos-rebuild/11-risks-and-dependencies.md`](docs/dcconcretos-rebuild/11-risks-and-dependencies.md).
