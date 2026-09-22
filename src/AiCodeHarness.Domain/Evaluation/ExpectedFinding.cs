using AiCodeHarness.Domain.Models;

namespace AiCodeHarness.Domain.Evaluation;

public sealed record ExpectedFinding(
    string Rule,
    Severity Severity);
