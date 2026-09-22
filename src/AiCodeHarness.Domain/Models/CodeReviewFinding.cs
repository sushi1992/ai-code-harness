namespace AiCodeHarness.Domain.Models;

public sealed record CodeReviewFinding(
    string Rule,
    string Category,
    Severity Severity,
    string Message);
