const byte INITAL_REGISTER_VALUE = 1;
const byte INITIAL_CYCLE_OFFSET = 20;
const byte CYCLE_OFFSET = 40;
const byte SPRITE_WIDTH = 3;

var input = await File.ReadAllLinesAsync("input.txt");

var instructions = input
    .Select(x => x.Split(' '))
    .Select(x => new Instruction(
        command: x.First(),
        param: x.Length > 1 && short.TryParse(x.Last(), out var param) ? param : (short)0,
        cycles: (byte)x.Length)
    );

var instructionQueue = new Queue<Instruction>(instructions);

Instruction? pendingInstruction = null;

int regX = INITAL_REGISTER_VALUE;
var cycle = 0;
var signal = 0;

while (instructionQueue.Count > 0)
{
    // draw sprite at begin of cycle
    var drawIndex = cycle - (cycle / CYCLE_OFFSET * CYCLE_OFFSET);
    var pos = drawIndex - regX + (SPRITE_WIDTH / 2);
    Console.Write(pos >= 0 && pos < SPRITE_WIDTH ? '#' : '.');

    cycle++;

    // part 1 offset
    if ((cycle + INITIAL_CYCLE_OFFSET) % CYCLE_OFFSET == 0)
    {
        signal += cycle * regX;
    }

    pendingInstruction = pendingInstruction != null && pendingInstruction.Cycles > 0
        ? pendingInstruction
        : instructionQueue.Dequeue();

    pendingInstruction.Cycles--;

    if (pendingInstruction.Cycles <= 0)
    {
        switch (pendingInstruction.Command)
        {
            case "addx":
                regX += pendingInstruction.Param;
                break;
            default:
                break;
        }
        pendingInstruction = null;
    }

    if (cycle % CYCLE_OFFSET == 0)
    {
        Console.WriteLine();
    }
}

Console.WriteLine();
Console.WriteLine($"signal strength: {signal}");

record Instruction
{
    public Instruction(string command, short param, byte cycles = 1)
    {
        Command = command;
        Param = param;
        Cycles = cycles;
    }
    public string Command { get; init; }
    public short Param { get; init; }
    public byte Cycles { get; set; }
}