# AI Code Harness

Production-shaped .NET evaluation harness for LLM-assisted code review, regression testing, reliability, and quality gating.

## Why this exists

LLM features are easy to demo and much harder to operate reliably. This project focuses on the engineering around the model: repeatable evaluation cases, structured findings, deterministic scoring, regression detection, quality gates, latency/cost tracking, and eventually tool/agent evaluation.

The goal is not to ask "did the model sound good?" but to answer questions such as:

- Did a prompt or model change improve precision or recall?
- Did false positives increase?
- Is a critical security rule detected consistently across repeated runs?
- Did p95 latency or estimated cost regress?
- Can hostile code/comments manipulate the reviewer?
- Should a result block CI or only warn?

## Current architecture

```text
AiCodeHarness.Cli
        |
        v
AiCodeHarness.Application
        |
        v
AiCodeHarness.Domain
        ^
        |
AiCodeHarness.Infrastructure
```

### Projects

- **Domain** - evaluation cases, expected findings, review findings, scores.
- **Application** - reviewer abstraction, evaluation runner, deterministic scoring.
- **Infrastructure** - model/provider integrations. A deterministic fake is included first so the eval framework can be exercised without network calls.
- **CLI** - runs the starter evaluation suite.
- **Tests** - verifies scoring behaviour independently from any LLM.

## Run locally

Requires .NET 8 SDK.

```bash
dotnet restore
dotnet test
dotnet run --project src/AiCodeHarness.Cli
```

Expected starter output:

```text
AI Code Harness - Evaluation Results
------------------------------------
sql-injection-001: PASS precision=1.00 recall=1.00
sql-injection-002: PASS precision=1.00 recall=1.00
```

## Roadmap

### Phase 1 - evaluation core

- [x] Typed evaluation cases
- [x] Expected vs actual findings
- [x] Precision / recall scoring
- [x] Positive and negative controls
- [x] Unit tests for scoring
- [ ] Suite-level aggregate metrics
- [ ] Severity-aware scoring
- [ ] Dataset loading from JSON/YAML

### Phase 2 - real LLM integration

- [ ] Structured JSON-schema output
- [ ] OpenAI implementation of `ILlmCodeReviewer`
- [ ] Model + prompt version metadata
- [ ] Retry/error classification
- [ ] Token, latency and estimated-cost telemetry
- [ ] Persisted evaluation runs

### Phase 3 - reliability

- [ ] Repeated-run consistency metrics
- [ ] Prompt/model regression comparison
- [ ] Configurable PASS/WARN/FAIL quality gates
- [ ] Prompt-injection eval set
- [ ] CI integration

### Phase 4 - agents and tools

- [ ] Tool-call correctness
- [ ] Constraint and policy adherence
- [ ] Failure recovery
- [ ] Simulated scientific-workcell tasks
- [ ] Safety boundary: deterministic orchestration remains authoritative

## Design principle

The model proposes findings; deterministic code evaluates whether those findings satisfy the expected contract and quality policy. The LLM is never treated as its own judge.
