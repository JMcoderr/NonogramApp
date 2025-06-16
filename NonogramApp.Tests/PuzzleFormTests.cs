using Xunit;
using System.Collections.Generic;
using NonogramApp.Views;

public class PuzzleFormTests
{
    [Fact]
    public void GetRuns_ReturnsCorrectRuns_ForFilledAndEmptyCells()
    {
        // Arrange
        int[] line = { 1, 1, 0, 1, 0, 1, 1, 1, 0 };
        // Act
        var runs = PuzzleForm.GetRuns(line);
        // Assert
        Assert.Equal(new List<int> { 2, 1, 3 }, runs);
    }
}