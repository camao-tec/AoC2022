public static class Helper
{
    public static Shape ToShape(this string s)
    {
        return s switch
        {
            "A" => new Rock(),
            "X" => new Rock(),
            "B" => new Paper(),
            "Y" => new Paper(),
            "C" => new Sciccors(),
            "Z" => new Sciccors()
        };
    }
}
