using AiCodeHarness.Domain.Models;

namespace AiCodeHarness.Application.Abstractions;

public interface ILlmCodeReviewer
{
    Task<IReadOnlyCollection<CodeReviewFinding>> ReviewAsync(
        string code,
        CancellationToken cancellationToken = default);
}
