from __future__ import annotations

import os
from dataclasses import dataclass

from dotenv import load_dotenv


@dataclass(frozen=True)
class SqlConfig:
    server: str
    database: str
    user: str
    password: str
    odbc_extra: str

    @staticmethod
    def from_env() -> SqlConfig:
        load_dotenv(override=False)
        server = os.environ.get("DC_SQLSERVER", "").strip()
        database = os.environ.get("DC_DATABASE", "").strip()
        user = os.environ.get("DC_SQLUSER", "").strip()
        password = os.environ.get("DC_SQLPASSWORD", "").strip()
        extra = os.environ.get("DC_ODBC_EXTRA", "TrustServerCertificate=yes").strip()
        missing = [k for k, v in [
            ("DC_SQLSERVER", server),
            ("DC_DATABASE", database),
            ("DC_SQLUSER", user),
            ("DC_SQLPASSWORD", password),
        ] if not v]
        if missing:
            raise ValueError(
                "Faltan variables de entorno: " + ", ".join(missing) + ". "
                "Copia env.example a .env y completa los valores."
            )
        return SqlConfig(server=server, database=database, user=user, password=password, odbc_extra=extra)

    def odbc_connection_string(self) -> str:
        # Driver 18 es el actual en Mac/Homebrew msodbcsql18
        return (
            "Driver={ODBC Driver 18 for SQL Server};"
            f"Server={self.server};Database={self.database};"
            f"Uid={self.user};Pwd={self.password};"
            f"{self.odbc_extra}"
        )
