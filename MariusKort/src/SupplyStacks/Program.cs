using SupplyStacks;
using System.Text.RegularExpressions;

var input = await File.ReadAllLinesAsync("input.txt");

var cratesInput = input
    .TakeWhile(x => Regex.IsMatch(x, "^[A-Z\\[\\]\\s]+$"))
    .ToArray();

var stacksInput = input
    .Skip(cratesInput.Length)
    .Take(1)
    .FirstOrDefault();

var instructionsInput = input
    .Skip(cratesInput.Length + 1)
    .Where(s => !string.IsNullOrWhiteSpace(s));

if (string.IsNullOrEmpty(stacksInput))
{
    Console.WriteLine("Unable to generate stacks from input file");
    return;
}

var instructions = Helpers.GenerateInstructions(instructionsInput).ToList();

// part 1
var stacks = Helpers.GenerateStacks(cratesInput, stacksInput).ToList();
instructions.ForEach(i => Helpers.ApplyInstruction(stacks, i));

Console.WriteLine($"Result 1: {new string(stacks.Select(s => s.Pop()).ToArray())}");

// part 2
var stacks2 = Helpers.GenerateStacks(cratesInput, stacksInput).ToList();
instructions.ForEach(i => Helpers.ApplyInstructionExtended(stacks2, i));

Console.WriteLine($"Result 2: {new string(stacks2.Select(s => s.Pop()).ToArray())}");