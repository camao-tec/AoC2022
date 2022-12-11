using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace tests
{
    public class Day11Tests
    {
        [Fact]
        public void Parse_TestInput_Correct()
        {
            var input =
@"Monkey 0:
    Starting items: 79, 98
    Operation: new = old * 19
    Test: divisible by 23
      If true: throw to monkey 2
      If false: throw to monkey 3";
            var lines = input.Split(Environment.NewLine);

            var monkeys = MonkeyParser.Parse(lines);
            Assert.Equal(Monkey.Operations.Multiplication, monkeys.Last().Operation);
            Assert.Equal(19, monkeys.Last().OperationNumber);
            Assert.Equal("0", monkeys.Last().Name);
            Assert.Equal(23, monkeys.Last().TestDivisor);
            Assert.Equal(3, monkeys.Last().CheckFalseThrowTo);
            Assert.Equal(2, monkeys.Last().CheckTrueThrowTo);
        }

        [Fact]
        public void Process_TestInput_Correct()
        {
            var input =
@"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";
            var lines = input.Split(Environment.NewLine);

            var monkeys = MonkeyParser.Parse(lines);

            for (int i = 0; i < 20; i++)
            {
                MonkeyParser.Inspect(monkeys);
            }

            Assert.Empty(monkeys[3].Items);
            Assert.Empty(monkeys[2].Items);

            var monkeyBusiness = monkeys.OrderByDescending(x => x.InspectionCount).Take(2);
            Assert.Equal(10605, monkeyBusiness.First().InspectionCount * monkeyBusiness.Last().InspectionCount);
        }
    }
}