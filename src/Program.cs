using System;
using NUnit.Framework;
using static advent_of_code_dotnet.Utils;
using static advent_of_code_dotnet.Day02;

// var testInput = ReadInput(TestFileName);
// Assert.AreEqual(5, Part2(testInput));

var input = ReadInput(ProductionFileName);
Console.WriteLine(Part1(input));
Console.WriteLine(Part2(input));