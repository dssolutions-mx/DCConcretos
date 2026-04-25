# Evidence (staging traces — do not commit raw outputs)

This folder holds **optional** artifacts from Phase 1b: Extended Events exports, ILSpy notes with secrets redacted, schema validation CSVs, and golden batch sign-off scans.

**Git:** By default everything here is **ignored** except `README.md` and files matching `*TEMPLATE*.md`. Copy a template to a new filename locally if you need it versioned (redacted only).

## What to add after running on staging

| Artifact | Source |
|----------|--------|
| `golden-batch-record-TEMPLATE.md` | Copy to `golden-batch-record.md` (local only) after `09-validation-protocol.md` |
| `schema-validation-results-TEMPLATE.md` | Copy after `scripts/validate-information-schema.sql` |
| `ilspy-export/` | ILSpy “Save Code” export (keep in secure share, not public git) |
