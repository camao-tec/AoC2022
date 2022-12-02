using Xunit;

namespace tests
{
    public class Day2Tests
    {
        [Theory]
        [InlineData("A", "Y", 8)]
        [InlineData("B", "X", 1)]
        [InlineData("C", "Z", 6)]
        [InlineData("C", "Y", 2)]
        [InlineData("A", "Z", 3)]
        [InlineData("A", "X", 4)]
        [InlineData("B", "Y", 5)]
        public void Shape_Fight_CorrectPoints(string opponent, string me, int expectedPoints)
        {
            Assert.Equal(expectedPoints, me.ToShape().Fight(opponent.ToShape()));
        }
        
        [Theory]
        [InlineData("A", "Y", 4)]
        [InlineData("B", "X", 1)]
        [InlineData("C", "Z", 7)]
        [InlineData("C", "Y", 6)]
        [InlineData("A", "Z", 8)]
        public void Shape_FightWithPredictedResult_CorrectPoints(string opponent, string result, int expectedPoints)
        {
            var requiredResult = result.ToShape().ToRequiredResult();
            var me = opponent.ToShape().FindOpponentForResult(requiredResult);
            Assert.Equal(expectedPoints, me.Fight(opponent.ToShape()));
        }
    }
}