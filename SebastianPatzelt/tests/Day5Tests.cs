using System.Collections.Generic;
using Xunit;

namespace tests
{
    public class Day5Tests
    {
        [Theory]
        [InlineData("move 1 from 2 to 1")]
        public void Parse_input_Correct_Instruction(string instructionCode)
        {
            var instruction = instructionCode.ToInstruction();

            Assert.Equal(2, instruction.Start);
            Assert.Equal(1, instruction.End);
            Assert.Equal(1, instruction.Amount);
        }

        [Theory]
        [InlineData("move 1 from 2 to 1")]
        public void Move_WithGivenInstruction_ResultsIn(string instructionCode)
        {
            var instruction = instructionCode.ToInstruction();
            var stacks = TestData;
            instruction.Move(stacks);
            Assert.Equal("D", stacks[0].Peek());
            Assert.Equal("C", stacks[1].Peek());
            Assert.Equal("P", stacks[2].Peek());

            instruction = "move 3 from 1 to 3".ToInstruction();
            instruction.Move(stacks);

            Assert.Empty(stacks[0]);
            Assert.Equal(4, stacks[2].Count);
            Assert.Equal("Z", stacks[2].Peek());

        }
        
        [Theory]
        [InlineData("move 1 from 2 to 1")]
        public void MoveMulti_WithGivenInstruction_ResultsIn(string instructionCode)
        {
            var instruction = instructionCode.ToInstruction();
            var stacks = TestData;
            instruction.MoveMulti(stacks);
            Assert.Equal("D", stacks[0].Peek());
            Assert.Equal("C", stacks[1].Peek());
            Assert.Equal("P", stacks[2].Peek());

            instruction = "move 3 from 1 to 3".ToInstruction();
            instruction.MoveMulti(stacks);

            Assert.Empty(stacks[0]);
            Assert.Equal(4, stacks[2].Count);
            Assert.Equal("D", stacks[2].Peek());

        }

        private Stack<string>[] TestData
        {
            get
            {
                var stack1 = new Stack<string>();
                var stack2 = new Stack<string>();
                var stack3 = new Stack<string>();

                stack1.Push("Z");
                stack1.Push("N");

                stack2.Push("M");
                stack2.Push("C");
                stack2.Push("D");

                stack3.Push("P");

                return new[] { stack1, stack2, stack3 };
            }
        }
    }
}