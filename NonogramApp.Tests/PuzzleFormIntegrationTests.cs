using Xunit;
using NonogramApp.Views;
using System.Reflection;

public class PuzzleFormIntegrationTests
{
    [Fact]
    public void GenerateClues_MatchesSolution()
    {
        // Arrange
        var form = new PuzzleForm(5);
        // Use reflection to call private methods for test purposes
        var randomizeSolution = typeof(PuzzleForm).GetMethod("RandomizeSolution", BindingFlags.NonPublic | BindingFlags.Instance);
        var generateClues = typeof(PuzzleForm).GetMethod("GenerateClues", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        randomizeSolution.Invoke(form, null);
        generateClues.Invoke(form, null);

        // Assert
        // For each row, the clues should match the solution's runs
        var solutionField = typeof(PuzzleForm).GetField("solution", BindingFlags.NonPublic | BindingFlags.Instance);
        var rowCluesField = typeof(PuzzleForm).GetField("rowClues", BindingFlags.NonPublic | BindingFlags.Instance);

        int[][] solution = (int[][])solutionField.GetValue(form);
        var rowClues = (List<List<int>>)rowCluesField.GetValue(form);

        for (int i = 0; i < solution.Length; i++)
        {
            var expected = PuzzleForm.GetRuns(solution[i]);
            Assert.Equal(expected, rowClues[i]);
        }
    }
}