from __future__ import annotations

import argparse
import sys

from . import __version__
from .config import SqlConfig
from .sql_ping import ping_sql


def main(argv: list[str] | None = None) -> int:
    parser = argparse.ArgumentParser(
        prog="dcconcretos",
        description="CLI ligero: conexión SQL Server (Comercial) y futuras cargas en lote.",
    )
    parser.add_argument("--version", action="version", version=f"%(prog)s {__version__}")
    sub = parser.add_subparsers(dest="cmd", required=True)

    p_ping = sub.add_parser("ping", help="Prueba conexión a SQL Server (SELECT 1)")
    p_ping.set_defaults(_fn=_cmd_ping)

    p_compras = sub.add_parser("compras", help="Carga de compras (por ahora solo --dry-run)")
    p_compras.add_argument("archivo", nargs="?", help="Ruta a Excel/CSV (futuro)")
    p_compras.add_argument(
        "--dry-run",
        action="store_true",
        help="No escribe en la base; solo valida configuración y archivo si existe",
    )
    p_compras.set_defaults(_fn=_cmd_compras)

    args = parser.parse_args(argv)
    return int(args._fn(args))


def _cmd_ping(_args: argparse.Namespace) -> int:
    try:
        cfg = SqlConfig.from_env()
        msg = ping_sql(cfg)
        print(msg)
        return 0
    except Exception as e:  # noqa: BLE001 — CLI amigable
        print(f"Error: {e}", file=sys.stderr)
        return 1


def _cmd_compras(args: argparse.Namespace) -> int:
    try:
        SqlConfig.from_env()
    except Exception as e:
        print(f"Error: {e}", file=sys.stderr)
        return 1

    if not args.dry_run:
        print(
            "Por seguridad, la primera versión solo permite --dry-run. "
            "Las escrituras reales irán detrás del mismo diseño que el .NET (SDK / trazas).",
            file=sys.stderr,
        )
        return 2

    path = args.archivo
    if path:
        from pathlib import Path

        p = Path(path)
        if not p.is_file():
            print(f"No existe el archivo: {path}", file=sys.stderr)
            return 1
        print(f"[dry-run] Archivo encontrado: {p.resolve()} ({p.stat().st_size} bytes)")
    else:
        print("[dry-run] Sin archivo; solo configuración cargada.")

    print("[dry-run] Listo. Aquí iría la lógica de compras cuando esté definida (ILSpy / SDK).")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
