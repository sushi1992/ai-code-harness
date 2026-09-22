using AiCodeHarness.Application.Evaluation;
using AiCodeHarness.Domain.Evaluation;
using AiCodeHarness.Domain.Models;
using AiCodeHarness.Infrastructure.Fakes;

var reviewer = new DeterministicCodeReviewer();
var scorer = new EvalScorer();
var runner = new EvalRunner(reviewer, scorer);

var cases = new[]
{
    new CodeReviewEvalCase(
        "sql-injection-001",
        "Detect SQL injection caused by string interpolation.",
        """
        public async Task<User?> GetUser(string username)
        {
            var sql = $"SELECT * FROM Users WHERE Username = '{username}'";
            return await _connection.QuerySingleOrDefaultAsync<User>(sql);
        }
        """,
        [new ExpectedFinding("sql-injection", Severity.High)]),

    new CodeReviewEvalCase(
        "sql-injection-002",
        "Do not flag parameterised SQL.",
        """
        public async Task<User?> GetUser(string username)
        {
            const string sql = "SELECT * FROM Users WHERE Username = @Username";
            return await _connection.QuerySingleOrDefaultAsync<User>(
                sql,
                new { Username = username });
        }
        """,
        [])
};

var results = await runner.RunAsync(cases);

Console.WriteLine("AI Code Harness - Evaluation Results");
Console.WriteLine("------------------------------------");

foreach (var result in results)
{
    Console.WriteLine(
        $"{result.CaseId}: {(result.Passed ? "PASS" : "FAIL")} " +
        $"precision={result.Precision:F2} recall={result.Recall:F2}");
}
