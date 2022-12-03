using Xunit;
using System.Linq;
using System;

namespace tests
{
    public class Day3Tests
    {
        [Theory]
        [InlineData("vJrwpWtwJgWrhcsFMMfFFhFp", 'p', 16)]
        [InlineData("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", 'L', 38)]
        [InlineData("PmmdzqPrVvPwwTWBwg", 'P', 42)]
        [InlineData("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", 'v', 22)]
        [InlineData("ttgJtRGJQctTZtZT", 't', 20)]
        [InlineData("CrZsJsPPZsGzwwsLwLmpwMDw", 's', 19)]
        public void Parse_Testdata_hasCorrectItemType(string rucksack, char expectedItemType, int expectedPriority)
        {
            var item = RucksackParser.ExtractItemTypePriority(rucksack);
            Assert.Equal(expectedItemType, item.PriorityType);
            Assert.Equal(expectedPriority, item.Priority);
        }

        [Fact]
        public void SumPriority_SampleInput_CalculatesCorrect()
        {
            var sampleInput = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";

            var sum = sampleInput.Split(Environment.NewLine).Select(x => x.ExtractItemTypePriority().Priority).Sum();
            Assert.Equal(157, sum);
        }

        [Fact]
        public void SumPriorityPartTwo_SampleInput_CalculatesCorrect()
        {
            var rucksacks = new[] {
                "vJrwpWtwJgWrhcsFMMfFFhFp",
                "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
                "PmmdzqPrVvPwwTWBwg",
                "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
                "ttgJtRGJQctTZtZT",
                "CrZsJsPPZsGzwwsLwLmpwMDw"
            };

            var sum = rucksacks.ExtractItemTypePriority();
            Assert.Equal(70, sum);
        }
    }
}