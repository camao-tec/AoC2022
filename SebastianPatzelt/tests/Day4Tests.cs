using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace tests
{
    public class Day4Tests
    {
        [Theory]
        [InlineData("2-4,6-8", false)]
        [InlineData("2-3,4-5", false)]
        [InlineData("6-6,4-6", true)]
        [InlineData("2-8,3-7", true)]
        [InlineData("5-7,7-9", true)]
        [InlineData("2-6,4-8", true)]
        public void Parse_input_AnyInCommon(string pairs, bool expected)
        {
            var result = pairs.ToRanges().HasAnySectionInCommon();
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData("2-4,6-8", false)]
        [InlineData("2-3,4-5", false)]
        [InlineData("6-6,4-6", true)]
        [InlineData("2-8,3-7", true)]
        [InlineData("5-7,7-9", false)]
        [InlineData("2-6,4-8", false)]
        public void Parse_input_FullOverlap(string pairs, bool expected)
        {
            var result = pairs.ToRanges().HasFullSectionInCommon();
            Assert.Equal(expected, result);
        }
    }
}