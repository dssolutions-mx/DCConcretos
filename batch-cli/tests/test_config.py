import os

import pytest

from dcconcretos_cli.config import SqlConfig


def test_from_env_missing(monkeypatch: pytest.MonkeyPatch) -> None:
    for k in ("DC_SQLSERVER", "DC_DATABASE", "DC_SQLUSER", "DC_SQLPASSWORD"):
        monkeypatch.delenv(k, raising=False)
    with pytest.raises(ValueError, match="Faltan"):
        SqlConfig.from_env()


def test_from_env_ok(monkeypatch: pytest.MonkeyPatch) -> None:
    monkeypatch.setenv("DC_SQLSERVER", "test\\instance")
    monkeypatch.setenv("DC_DATABASE", "db")
    monkeypatch.setenv("DC_SQLUSER", "u")
    monkeypatch.setenv("DC_SQLPASSWORD", "p")
    c = SqlConfig.from_env()
    assert "ODBC Driver 18" in c.odbc_connection_string()
    assert "test\\instance" in c.odbc_connection_string()
