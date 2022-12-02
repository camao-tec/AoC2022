using System;
public class Shape
{
    public enum RequiredResult
    {
        Loose = 0,
        Draw = 1,
        Win = 2
    }

    public Shape(string id, Type losesAgainst, int value)
    {
        Id = id;
        LosesAgainst = losesAgainst;
        Value = value;
    }

    public Shape FindOpponentForResult(RequiredResult requiredResult)
    {
        if(requiredResult == RequiredResult.Loose)
        {
            switch (this)
            {
                case Paper: return new Rock();
                case Rock: return new Sciccors();
                case Sciccors: return new Paper();
            }
        } else if (requiredResult == RequiredResult.Draw) {
            switch (this)
            {
                case Paper: return new Paper();
                case Rock: return new Rock();
                case Sciccors: return new Sciccors();
            }
        }
        else if (requiredResult == RequiredResult.Win)
        {
            switch (this)
            {
                case Paper: return new Sciccors();
                case Rock: return new Paper();
                case Sciccors: return new Rock();
            }
        }
        return null;
    }

    public RequiredResult ToRequiredResult()
    {
        return Id switch
        {
            "A" => RequiredResult.Loose,
            "B" => RequiredResult.Draw,
            "C" => RequiredResult.Win,
        };
    }

    public string Id { get; set; }
    public Type LosesAgainst { get; set; }
    public int Value { get; set; }

    public int Fight(Shape opponent)
    {
        int points = 0;
        if (opponent.Id == Id)
        {
            points = 3;
        }
        else if (opponent.LosesAgainst.Equals(GetType()))
        {
            points = 6;
        }
        else if (LosesAgainst.Equals(opponent.GetType()))
        {
            points = 0;
        }
        return points + Value;
    }

    public override string ToString()
    {
        return $"{GetType().Name} {Id} {ToRequiredResult().ToString()}";
    }
}