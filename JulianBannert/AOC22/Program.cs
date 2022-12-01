using System.Reflection;
using AOC22.Misc;

{
    var dayTypes = Assembly.GetExecutingAssembly().DefinedTypes
        .Where(type => type.GetCustomAttributes().Any(attribute => attribute.GetType().IsAssignableFrom(typeof(DayAttribute))))
        .ToDictionary(typeInfo => typeInfo.GetCustomAttribute<DayAttribute>()!.Index);

    if (args.Length == 0) // Run all days
    {
        for (var i = 1; i < dayTypes.Count +1; i++)
            (Activator.CreateInstance(dayTypes[i]) as IDailyPuzzle)?.Run(InputHelper.ReadInputLines(i));
    }
    else // run single day
    {
        if (!int.TryParse(args[0], out var index)) return;
        if (!dayTypes.TryGetValue(index, out var dayType))
        {
            Console.WriteLine("That day is not implemented.");
            return;
        }
        (Activator.CreateInstance(dayType) as IDailyPuzzle)?.Run(InputHelper.ReadInputLines(index));
    }
}