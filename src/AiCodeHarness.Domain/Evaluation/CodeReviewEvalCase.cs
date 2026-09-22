namespace AiCodeHarness.Domain.Evaluation;

public sealed record CodeReviewEvalCase(
    string Id,
    string Description,
    string Code,
    IReadOnlyCollection<ExpectedFinding> ExpectedFindings);
