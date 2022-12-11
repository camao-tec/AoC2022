using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace tests
{

    public class Day9Tests
    {
        [Fact]
        public void Day9Parser_Testinput_Correct()
        {
            var input = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";
            var visits = new List<(int, int)>();
            (int, int) head = (0, 0);
            (int, int) tail = (0, 0);
            var current = (head, tail);
            visits.Add(tail);
            var commandos = input.Split(Environment.NewLine).Select(x => new { Direction = x.Split(' ')[0], Steps = int.Parse(x.Split(' ')[1]) }).ToList();
            foreach (var command in commandos)
            {
                if (command.Direction == "R" && command.Steps == 4)
                {
                    var o = "bama";
                }
                current = RopeService.Move(current.Item1, current.Item2, command.Direction, command.Steps, visits);
            }
            var t = visits.Distinct().ToList().Count;
            Assert.Equal(12, t);
        }

        [Fact]
        public void Move_Righ_TailFollows()
        {
            var visits = new List<(int, int)>();
            (int, int) head = (0, 0);
            (int, int) tail = (0, 0);
            visits.Add(tail);
            for (int i = 0; i < 4; i++)
            {
                head.Item1++;
                if (head.Distance(tail) >= 2)
                {
                    tail = RopeService.Correction(head, tail);
                    visits.Add(tail);
                }
            }
            Assert.True(head.Item1 == 4);
            Assert.Contains(visits, x => x.Item2 == 0 && x.Item1 == 3);
            Assert.DoesNotContain(visits, x => x.Item2 == 0 && x.Item1 == 4);
        }

        [Fact]
        public void Move_Up_TailFollows()
        {
            var visits = new List<(int, int)>();
            (int, int) head = (4, 0);
            (int, int) tail = (3, 0);
            for (int i = 0; i < 4; i++)
            {
                head.Item2++;
                if (head.Distance(tail) >= 2)
                {
                    tail = RopeService.Correction(head, tail);
                    visits.Add(tail);
                }
            }
            Assert.True(head.Item1 == 4 && head.Item2 == 4);
            Assert.Contains(visits, x => x.Item2 == 3 && x.Item1 == 4);
        }

        [Fact]
        public void Move_Left_TailFollows()
        {
            var visits = new List<(int, int)>();
            (int, int) head = (4, 4);
            (int, int) tail = (4, 3);
            var result = RopeService.Move(head, tail, "L", 3, visits);
            Assert.True(result.Item1.Item1 == 1 && result.Item1.Item2 == 4);
            Assert.Contains(visits, x => x.Item2 == 4 && x.Item1 == 4);
            Assert.Contains(visits, x => x.Item2 == 4 && x.Item1 == 3);
        }

        [Fact]
        public void Move_Down_TailFollows()
        {
            var visits = new List<(int, int)>();
            (int, int) head = (1, 4);
            (int, int) tail = (2, 4);
            var result = RopeService.Move(head, tail, "D", 1, visits);
            Assert.True(result.Item1.Item1 == 1 && result.Item1.Item2 == 3);
            Assert.Empty(visits);
        }

        [Fact]
        public void Move_Down1_TailFollows()
        {
            var visits = new List<(int, int)>();
            (int, int) head = (1, 4);
            (int, int) tail = (2, 4);
            var result = RopeService.Move(head, tail, "D", 2, visits);
            Assert.True(result.Item1.Item1 == 1 && result.Item1.Item2 == 2);
            Assert.True(result.Item2.Item1 == 1 && result.Item2.Item2 == 3);
            Assert.True(visits.Count == 1);
        }
    }
}