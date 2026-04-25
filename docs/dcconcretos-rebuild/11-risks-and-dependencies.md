# Risks and dependencies

## Technical risks

| Risk | Impact | Mitigation |
|------|--------|------------|
| Comercial **DB schema drift** across versions | Silent query failure or corrupt writes | Pin Comercial version; run `scripts/validate-information-schema.sql` on upgrade |
| Unknown **write paths** in legacy EXE | Rebuild misses edge case | Complete ILSpy + **golden trace** (`09`) before coding writes |
| **`usaContpaqi` path** unclear | Mis-implemented “Enviar a Comercial” | Runtime trace + distributor guidance |
| Excel paste surface (`btnPegarExcel`) | Injection / malformed sheet crashes | Validate schema; size limits; try/catch with user-safe messages |

## Security / compliance

- Historical **`sa`** usage — credential exposure; **rotate** all secrets found in old configs; assume compromise if folder was shared.
- **PII / financial data** in traces — `.xel` and Profiler outputs must be **stored encrypted** and **redacted** before attaching to tickets.

## Legal / commercial

- **Ownership** of `DCConcretosWF.exe` and `Modelo.dll` — client should confirm license to reverse engineer / replace (contract with vanished vendor).
- **CONTPAQi SDK** terms — confirm with distributor before shipping a replacement that bundles SDK binaries.

## Organizational dependencies

- **Named contador** for UAT sign-off (`09` golden record).
- **IT** for staging SQL instance + VM + firewall rules.
- **Operator** who knows current file templates and daily workflow.

## Open technical dependencies

- Exact meaning of **`rutaArchivoInterfaz`**.
- Whether **`btnEnviarAComercial`** requires local Comercial UI session.
