using System;
using System.Linq;
using Xunit;

namespace tests
{

    public class Day8Tests
    {
        private const string input = @"30373
25512
65332
33549
35390";

        [Theory]
        [InlineData(1, 1, true, false, true, false)]
        [InlineData(1, 2, false, true, true, false)]
        [InlineData(1, 3, false, false, false, false)]
        public void Parse_GivenInput_Correct(int rowIndex, int colIndex, bool visibleLeft, bool visibleRight, bool visibleTop, bool visibleBottom)
        {
            var grid = GridParser.Parse(input.Split(Environment.NewLine));
            Assert.Equal(visibleLeft, grid[rowIndex][colIndex].Visibility.Left);
            Assert.Equal(visibleRight, grid[rowIndex][colIndex].Visibility.Right);
            Assert.Equal(visibleTop, grid[rowIndex][colIndex].Visibility.Top);
            Assert.Equal(visibleBottom, grid[rowIndex][colIndex].Visibility.Bottom);
        }

        [Theory]
        [InlineData(1, 2, 1)]
        [InlineData(3, 2, 2)]
        public void GetTopScore_GivenInput_IsExpectedValue(int rowIndex, int colIndex, int expectedScore)
        {
            var root = input.Split(Environment.NewLine).Select((x, row) => x.Select((y, col) => new Cell(row, col, int.Parse(y.ToString()))).ToArray()).ToArray();
            var cellToInspect = root[rowIndex][colIndex];
            var neighbors = GridHelper.GetNeigbours(cellToInspect, root);
            var top = neighbors.Where(n => n.Row < rowIndex && n.Col == cellToInspect.Col).Reverse();
            var score = GridParser.GetScore(cellToInspect, top, true);
            Assert.Equal(expectedScore, score);
        }
        
        [Theory]
        [InlineData(1, 2, 2)]
        public void GetRightScore_GivenInput_IsExpectedValue(int rowIndex, int colIndex, int expectedScore)
        {
            var root = input.Split(Environment.NewLine).Select((x, row) => x.Select((y, col) => new Cell(row, col, int.Parse(y.ToString()))).ToArray()).ToArray();
            var cellToInspect = root[rowIndex][colIndex];
            var neighbors = GridHelper.GetNeigbours(cellToInspect, root);
            var right = neighbors.Where(n => n.Row == rowIndex && n.Col > cellToInspect.Col);
            var score = GridParser.GetScore(cellToInspect, right, false);
            Assert.Equal(expectedScore, score);
        }
        
        [Theory]
        [InlineData(3, 2, 8)]
        //[InlineData(1, 2, 4)]
        public void GeScore_GivenInput_IsExpectedValue(int rowIndex, int colIndex, int expectedScore)
        {
            var grid = GridParser.Parse(input.Split(Environment.NewLine));
            Assert.Equal(expectedScore, grid[rowIndex][colIndex].Visibility.Score);
        }

        [Fact]
        public void Parse_GivenInput_Sum()
        {
            var grid = GridParser.Parse(input.Split(Environment.NewLine));
            var t1 = grid.SelectMany(x => x).Count(x => x.Visibility != null && x.Visibility.Visible);
            var dimension = grid.Length;
            var umfang = 2 * dimension + 2 * (dimension - 2);
            Assert.Equal(21, umfang + t1);
        }
    }
}