using AiCodeHarness.Application.Abstractions;
using AiCodeHarness.Domain.Evaluation;

namespace AiCodeHarness.Application.Evaluation;

public sealed class EvalRunner(
    ILlmCodeReviewer reviewer,
    EvalScorer scorer)
{
    public async Task<IReadOnlyCollection<EvalResult>> RunAsync(
        IEnumerable<CodeReviewEvalCase> cases,
        CancellationToken cancellationToken = default)
    {
        var results = new List<EvalResult>();

        foreach (var testCase in cases)
        {
            var findings = await reviewer.ReviewAsync(testCase.Code, cancellationToken);
            results.Add(scorer.Score(testCase, findings));
        }

        return results;
    }
}
