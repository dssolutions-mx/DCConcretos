from __future__ import annotations

from .config import SqlConfig


def ping_sql(cfg: SqlConfig) -> str:
    """Ejecuta SELECT 1 en la base configurada. Requiere pyodbc."""
    try:
        import pyodbc  # type: ignore[import-untyped]
    except ImportError as e:
        raise RuntimeError(
            "Instala pyodbc: pip install 'dcconcretos-cli[sql]'"
        ) from e

    conn = pyodbc.connect(cfg.odbc_connection_string(), timeout=15)
    try:
        cur = conn.cursor()
        cur.execute("SELECT 1 AS ok")
        row = cur.fetchone()
        return f"Conexión OK (resultado: {row[0] if row else 'vacío'})"
    finally:
        conn.close()
