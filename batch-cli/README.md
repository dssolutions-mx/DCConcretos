# DC Concretos — CLI ligero (Python)

Aplicación **mínima** para desarrollar y probar desde **Mac** (o Linux) lo que realmente les importa: **conexión al SQL Server** donde vive CONTPAQi Comercial y, más adelante, **cargas en lote** sin depender de una app gráfica de Windows.

No reemplaza por sí sola las reglas de negocio de CONTPAQi: las **escrituras** a la base siguen requiriendo el mismo cuidado (SDK, trazas en copia, contador) que con .NET.

## Por qué Python puede ser “más simple”

| Tema | App Windows (.NET WPF) | Este CLI (Python) |
|------|------------------------|-------------------|
| Interfaz | Ventanas, menús | Comandos en terminal (`python -m ...`) |
| Probar en Mac | Hay que compilar para Windows o usar CI | Instalas dependencias y corres aquí |
| Tamaño del proyecto | Varios proyectos .csproj | Unas pocas carpetas |
| Conexión a SQL remoto | Sí | Sí (misma red / VPN / túnel) |

## Requisitos en Mac

1. **Python 3.11+** (`python3 --version`).
2. **Microsoft ODBC Driver 18 for SQL Server** (recomendado en Mac con Homebrew):

   ```bash
   brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
   brew install msodbcsql18
   ```

3. Red que permita llegar al **puerto 1433** (o el que use su instancia) del servidor SQL (VPN corporativa, firewall abierto a tu IP, etc.).

## Instalación (entorno virtual)

Desde la carpeta `batch-cli/`:

```bash
python3 -m venv .venv
source .venv/bin/activate
pip install -e ".[sql]"
```

## Configuración (sin meter secretos al git)

Copia el ejemplo y edita **solo en tu máquina**:

```bash
cp env.example .env
# Edita .env con host, base de datos, usuario y contraseña
```

Variables soportadas (ver `env.example`).

## Comandos

```bash
# Comprueba que llegas al SQL Server y a la base (solo lectura: SELECT 1)
python -m dcconcretos_cli ping

# Reserva para futuro: simular carga de compras (hoy no escribe nada)
python -m dcconcretos_cli compras --dry-run ./ejemplo.xlsx
```

## Relación con el ERP “remoto” y Contabilidad

- Si el **ERP** ya tiene la verdad de negocio, a largo plazo lo más simple suele ser que **las cargas nazcan en el ERP** y solo lo que falta se sincronice hacia Comercial (API, cola, tablas de interfaz) en lugar de mantener dos sitios donde se “suben” cosas.
- Este CLI es útil cuando **sí o sí** hay que hablar con la base de Comercial desde fuera del ERP y quieren **iterar rápido** (Mac, tests, scripts).

## Tests

```bash
pip install -e ".[dev]"
pytest -q
```

Los tests **no** conectan a su servidor real (usan mocks).
