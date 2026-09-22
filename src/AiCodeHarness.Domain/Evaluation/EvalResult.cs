namespace AiCodeHarness.Domain.Evaluation;

public sealed record EvalResult(
    string CaseId,
    bool Passed,
    double Precision,
    double Recall,
    IReadOnlyCollection<string> MissingRules,
    IReadOnlyCollection<string> UnexpectedRules);
