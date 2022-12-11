using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;

var visits = new List<(int, int)>();
(int, int) head = (0, 0);
(int, int) tail = (0, 0);
var current = (head, tail);
var commandos = File.ReadAllLines("input.txt").Select(x => new { Direction = x.Split(' ')[0], Steps = int.Parse(x.Split(' ')[1]) }).ToList();
foreach (var command in commandos)
{
    current = RopeService.Move(current.Item1, current.Item2, command.Direction, command.Steps, visits);
}
Console.WriteLine(visits.Distinct().ToList().Count);
Console.ReadLine();

public static class RopeService
{
    public static (int, int) Correction((int, int) head, (int, int) tail)
    {
        if (tail.Item2 == head.Item2)
        {
            if (tail.Item1 < head.Item1)
            {
                tail.Item1++;
            }
            else
            {
                tail.Item1--;
            }
        }
        else if (tail.Item2 < head.Item2)
        {
            tail.Item2++;
            var distance = tail.Distance(head);
            if (distance == Math.Sqrt(2))
            {
                tail.Item1 = head.Item1;
            }
        }
        else if (tail.Item2 > head.Item2)
        {
            tail.Item2--;
            var distance = tail.Distance(head);
            if (distance == Math.Sqrt(2))
            {
                tail.Item1 = head.Item1;
            }
        }
        return tail;
    }

    public static ((int, int), (int, int)) Move((int, int) head, (int, int) tail, string direction, int amount, List<(int, int)> visits)
    {
        for (int i = 0; i < amount; i++)
        {
            switch (direction)
            {
                case "R":
                    head.Item1++;
                    break;
                case "L":
                    head.Item1--;
                    break;
                case "U":
                    head.Item2++;
                    break;
                case "D":
                    head.Item2--;
                    break;
            }
            if (head.Distance(tail) >= 2)
            {
                tail = Correction(head, tail);
                visits.Add(tail);
            }
        }
        return (head, tail);
    }
}

public static class DistanceService
{
    public static double Distance(this (int, int) point1, (int, int) point2)
    {
        return Math.Sqrt((Math.Pow(point1.Item1 - point2.Item1, 2) + Math.Pow(point1.Item2 - point2.Item2, 2)));
    }
}