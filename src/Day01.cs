using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_dotnet
{
    public class Day01
    {
        public static string TestFileName = "Day01_test";
        public static string ProductionFileName = "Day01";
        
        public static int Part1(IEnumerable<string> input) =>
            input
                .Select(Measurement.FromString)
                .Aggregate((counter: Counter.Initial, prevMeasure: Measurement.Initial, index: 0),
                    (data, currMeasure) =>
                    {
                        var (counter, prevMeasure, index) = data;
                        return (
                            counter.IncreaseIfTrue(index > 0 && currMeasure.Value > prevMeasure.Value),
                            currMeasure,
                            index + 1
                        );
                    })
                .counter.Value;

        public static int Part2(IEnumerable<string> input) =>
            input
                .Select(int.Parse)
                .Aggregate((counter: Counter.Initial, prevMeasure: ThreeMeasurement.Initial, index: 0),
                    (data, currMeasure) =>
                    {
                        var (counter, prevMeasure, index) = data;
                        return (
                            counter.IncreaseIfTrue(index > 2 && prevMeasure.Slide(currMeasure).Sum > prevMeasure.Sum),
                            prevMeasure.Slide(currMeasure),
                            index + 1
                        );
                    })
                .counter.Value;


        internal class Measurement
        {
            public int Value { get; }

            private Measurement(int value)
            {
                Value = value;
            }

            public static Measurement Initial => new(0);
            public static Measurement FromString(string value) => new(int.Parse(value));
        }

        internal class ThreeMeasurement
        {
            private int First { get; }
            private int Second { get; }
            private int Third { get; }

            private ThreeMeasurement(int first, int second, int third)
            {
                First = first;
                Second = second;
                Third = third;
            }

            public static ThreeMeasurement Initial => new(0, 0, 0);

            public int Sum => First + Second + Third;
            public ThreeMeasurement Slide(int value) => new(Second, Third, value);
        }

        internal class Counter
        {
            public int Value { get; }

            private Counter(int value)
            {
                Value = value;
            }

            public Counter IncreaseIfTrue(bool check) => check ? new Counter(Value + 1) : this;

            public static Counter Initial => new(0);
        }
    }
}