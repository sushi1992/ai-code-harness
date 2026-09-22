using AiCodeHarness.Application.Evaluation;
using AiCodeHarness.Domain.Evaluation;
using AiCodeHarness.Domain.Models;

namespace AiCodeHarness.Tests;

[TestClass]
public sealed class EvalScorerTests
{
    [TestMethod]
    public void Score_WhenExpectedRuleIsFound_Passes()
    {
        var testCase = new CodeReviewEvalCase(
            "case-1",
            "example",
            "code",
            [new ExpectedFinding("sql-injection", Severity.High)]);

        var actual = new[]
        {
            new CodeReviewFinding(
                "sql-injection",
                "security",
                Severity.High,
                "message")
        };

        var result = new EvalScorer().Score(testCase, actual);

        Assert.IsTrue(result.Passed);
        Assert.AreEqual(1.0, result.Precision);
        Assert.AreEqual(1.0, result.Recall);
    }

    [TestMethod]
    public void Score_WhenUnexpectedRuleIsFound_Fails()
    {
        var testCase = new CodeReviewEvalCase(
            "case-2",
            "example",
            "code",
            []);

        var actual = new[]
        {
            new CodeReviewFinding(
                "sql-injection",
                "security",
                Severity.High,
                "message")
        };

        var result = new EvalScorer().Score(testCase, actual);

        Assert.IsFalse(result.Passed);
        Assert.AreEqual(0.0, result.Precision);
        Assert.AreEqual(1.0, result.Recall);
    }
}
