namespace AOC22.Misc;

public class DayAttribute : Attribute
{
    public int Index { get; }

    public DayAttribute(int index)
    {
        Index = index;
    }
}