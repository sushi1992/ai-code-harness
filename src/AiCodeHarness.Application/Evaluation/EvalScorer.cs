using AiCodeHarness.Domain.Evaluation;
using AiCodeHarness.Domain.Models;

namespace AiCodeHarness.Application.Evaluation;

public sealed class EvalScorer
{
    public EvalResult Score(
        CodeReviewEvalCase testCase,
        IReadOnlyCollection<CodeReviewFinding> actualFindings)
    {
        var expectedRules = testCase.ExpectedFindings
            .Select(x => x.Rule)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var actualRules = actualFindings
            .Select(x => x.Rule)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var truePositives = expectedRules.Intersect(actualRules, StringComparer.OrdinalIgnoreCase).Count();
        var falsePositives = actualRules.Except(expectedRules, StringComparer.OrdinalIgnoreCase).Count();
        var falseNegatives = expectedRules.Except(actualRules, StringComparer.OrdinalIgnoreCase).Count();

        var precision = truePositives + falsePositives == 0
            ? 1.0
            : (double)truePositives / (truePositives + falsePositives);

        var recall = truePositives + falseNegatives == 0
            ? 1.0
            : (double)truePositives / (truePositives + falseNegatives);

        var missing = expectedRules.Except(actualRules, StringComparer.OrdinalIgnoreCase).ToArray();
        var unexpected = actualRules.Except(expectedRules, StringComparer.OrdinalIgnoreCase).ToArray();

        return new EvalResult(
            testCase.Id,
            missing.Length == 0 && unexpected.Length == 0,
            precision,
            recall,
            missing,
            unexpected);
    }
}
