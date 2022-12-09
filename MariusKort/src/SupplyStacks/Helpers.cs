using System.Text.RegularExpressions;

namespace SupplyStacks
{
    internal static class Helpers
    {
        public static IEnumerable<Instruction> GenerateInstructions(IEnumerable<string> instructionsInput)
        {
            return instructionsInput
                .Select(s => Regex.Match(s, "^[^0-9]*([0-9]+)[^0-9]*([0-9]+)[^0-9]*([0-9]+)$"))
                .Where(m => m.Groups.Count > 3)
                .Select(m => new Instruction
                {
                    Amount = byte.TryParse(m.Groups[1].Value, out var amount) ? amount : byte.MinValue,
                    From = byte.TryParse(m.Groups[2].Value, out var from) ? from : byte.MinValue,
                    To = byte.TryParse(m.Groups[3].Value, out var to) ? to : byte.MinValue,
                });
        }

        public static IEnumerable<Stack<char>> GenerateStacks(string[] crates, string stacksInput)
        {
            return Regex.Matches(stacksInput, "[0-9]")
                .Select(m => crates
                    .SelectMany(c => c.Skip(m.Index).Take(1))
                    .Where(c => char.IsLetter(c))
                    .Reverse())
                .Select(x => new Stack<char>(x));
        }

        public static void ValidateInstruction(IList<Stack<char>> stacks, Instruction instruction)
        {
            if (stacks.Count < instruction.From)
            {
                throw new InvalidOperationException("Invalid stack to pick from");
            }
            if (stacks.Count < instruction.To)
            {
                throw new InvalidOperationException("Invalid stack to put on");
            }
            if (stacks[instruction.From - 1].Count < instruction.Amount)
            {
                throw new InvalidOperationException("not enough crates to pick");
            }
        }

        public static void ApplyInstruction(IList<Stack<char>> stacks, Instruction instruction)
        {
            ValidateInstruction(stacks, instruction);
            for (byte i = 0; i < instruction.Amount; i++)
            {
                var pickedUpCrate = stacks[instruction.From - 1].Pop();
                stacks[instruction.To - 1].Push(pickedUpCrate);
            }
        }

        public static void ApplyInstructionExtended(IList<Stack<char>> stacks, Instruction instruction)
        {
            ValidateInstruction(stacks, instruction);
            Enumerable.Range(1, instruction.Amount)
                .Select(_ => stacks[instruction.From - 1].Pop())
                .Reverse()
                .ToList()
                .ForEach(c => stacks[instruction.To - 1].Push(c));
        }
    }
}
