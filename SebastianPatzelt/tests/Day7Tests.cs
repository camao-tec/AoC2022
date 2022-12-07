using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace tests
{
    public class Day7Tests
    {
        private const string input = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";

        [Theory]
        [InlineData("a", 94853)]
        [InlineData("d", 24933642)]
        public void Parse_GivenInput_Correct(string dir, long expectedSize)
        {
            var root = input.Split(Environment.NewLine).Parse1();
            Assert.Equal(expectedSize, root.Children[dir].Size);
        }

        [Fact]
        public void Parse_GivenInput_Sum()
        {
            var root = input.Split(Environment.NewLine).Parse1();
            var results = DirParser.ProcessDeletionCandidates(root, 100000, 30000, (long)Math.Pow(2, 10));
            Assert.Equal(95437, results.Item1);
        }
    }
}