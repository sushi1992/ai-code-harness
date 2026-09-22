using AiCodeHarness.Application.Abstractions;
using AiCodeHarness.Domain.Models;

namespace AiCodeHarness.Infrastructure.Fakes;

public sealed class DeterministicCodeReviewer : ILlmCodeReviewer
{
    public Task<IReadOnlyCollection<CodeReviewFinding>> ReviewAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<CodeReviewFinding> findings =
            code.Contains("SELECT * FROM Users WHERE Username = '", StringComparison.Ordinal)
                ? [new CodeReviewFinding(
                    Rule: "sql-injection",
                    Category: "security",
                    Severity: Severity.High,
                    Message: "User-controlled input is interpolated into SQL.")]
                : [];

        return Task.FromResult(findings);
    }
}
