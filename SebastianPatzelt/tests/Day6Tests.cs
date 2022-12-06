using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace tests
{
    public class Day6Tests
    {
        [Theory]
        [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
        [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
        [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
        [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
        [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
        public void Parse_InputWithPackageLengthFour_ReturnsCorrectStart(string signal, int expectedposition)
        {
            Assert.Equal(expectedposition, signal.FindIndexOfStartPackage(4));
        }

        [Theory]
        [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
        [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
        [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
        [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
        [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
        public void Parse_InputWithPackageLengthFourteen_ReturnsCorrectStart(string signal, int expectedposition)
        {
            Assert.Equal(expectedposition, signal.FindIndexOfStartPackage(14));
        }
    }
}