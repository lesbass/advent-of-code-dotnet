using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_dotnet
{
    public class Day03
    {
        public static string TestFileName = "Day03_test";
        public static string ProductionFileName = "Day03";

        public static int Part1(IEnumerable<string> input) => GetGammaRate(input) * GetEpsilonRate(input);

        public static int Part2(IEnumerable<string> input) =>
            GetOxygenGeneratorRating(input) * GetCo2ScrubberRating(input);

        private static int ExtractNumberAtPosition(string input, int index) => int.Parse(input[index].ToString());

        private static int GetAverageValueAtPosition(IEnumerable<string> input, int index) => (int)Math.Round(input
            .Select(it => ExtractNumberAtPosition(it, index)).Average(), 0, MidpointRounding.AwayFromZero);

        private static int GetAverageValueAtPositionInverted(IEnumerable<string> input, int index) => (int)Math.Round(
            input
                .Select(it => ExtractNumberAtPosition(it, index)).Average(), 0, MidpointRounding.AwayFromZero) ^ 1;

        private static int GetIntFromBinaryString(IEnumerable<int> input) =>
            Convert.ToInt32(string.Join("", input.Select(x => x.ToString())), 2);

        private static string FindOrNext(List<string> input, Func<List<string>, string> block) =>
            input.Count == 1 ? input.First() : block(input);

        private static string FindRating(IEnumerable<string> input, string prev, Func<List<string>, string> block) =>
            FindOrNext(input.Where(it => it.StartsWith(prev)).ToList(), block);

        private static string FindOxygenGeneratorRating(IEnumerable<string> input, string prev = "") =>
            FindRating(input, prev, it => FindOxygenGeneratorRating(it,
                prev + GetAverageValueAtPosition(it, prev.Length)
            ));

        private static string FindCo2ScrubberRating(IEnumerable<string> input, string prev = "") =>
            FindRating(input, prev, it => FindCo2ScrubberRating(it,
                prev + GetAverageValueAtPositionInverted(it, prev.Length)
            ));

        private static int GetGammaRate(IEnumerable<string> input) =>
            GetIntFromBinaryString(Enumerable.Range(0, input.First().Length)
                .Select(it => GetAverageValueAtPosition(input, it)));

        private static int GetEpsilonRate(IEnumerable<string> input) =>
            GetIntFromBinaryString(Enumerable.Range(0, input.First().Length)
                .Select(it => GetAverageValueAtPositionInverted(input, it)));

        private static int GetOxygenGeneratorRating(IEnumerable<string> input) => Convert.ToInt32(FindOxygenGeneratorRating(input), 2);

        private static int GetCo2ScrubberRating(IEnumerable<string> input) => Convert.ToInt32(FindCo2ScrubberRating(input), 2);
    }
}