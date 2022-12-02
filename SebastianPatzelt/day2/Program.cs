using System.Linq;
using System.IO;
using System;

Console.WriteLine(File.ReadAllLines("input.txt").Select(x =>
{
    var shapes = x.Split(' ');
    var opponent = shapes.First().ToShape();
    var mySign = shapes.Last().ToShape();
    return new { Opponent = opponent, Me = mySign };
})
.Select(x =>
{
    var requiredResult = x.Me.ToRequiredResult();
    var me = x.Opponent.FindOpponentForResult(requiredResult);
    return me.Fight(x.Opponent);
}).Sum());
