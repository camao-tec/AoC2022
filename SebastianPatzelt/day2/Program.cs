using System.Linq;
using System.IO;

File.ReadAllLines("input.txt").Select(x =>
{
    var shapes = x.Split(' ');
    var opponent = shapes.First().ToShape();
    var mySign = shapes.Last().ToShape();
    return new { Opponent = opponent, Me = mySign };
});

public static class Helper
{
    public static Shape ToShape(this string s)
    {
        return s switch
        {
            "A" => new Rock(),
            "B" => new Paper(),
            "C" => new Sciccors()
        };
}
public class Rock : Shape
{
    public Rock(): base("A", "Y", 1) { } 
}

public class Paper: Shape
{
    public Paper(): base("B", "Z", 2) { } 
}

public class Sciccors: Shape
{
    public Sciccors(): base("C", "X", 3) { } 
}

public class Shape
{
    public Shape(string id, string losesAgainst, int value)
    {
        Id = id;
        LosesAgainst = losesAgainst;
        Value = value;
    }

    public string Id { get; set; }
    public string LosesAgainst { get; set; }
    public int Value { get; set; }
    public int Fight(Shape opponent)
    {
        return 0;
    }
}