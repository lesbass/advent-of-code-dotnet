using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_dotnet
{
    public class Day02
    {
        public static string TestFileName = "Day02_test";
        public static string ProductionFileName = "Day02";

        public static int Part1(IEnumerable<string> input) =>
            ExecuteCommands(SimpleSubmarine.Initial, ParseCommands(input));

        public static int Part2(IEnumerable<string> input) => 
            ExecuteCommands(ProSubmarine.Initial, ParseCommands(input));


        private static IEnumerable<Command> ParseCommands(IEnumerable<string> input) => input
            .Select(Command.FromRawData);

        private static Submarine ExecuteCommand(Submarine submarine, Command command) => submarine.Execute(command);

        private static int ExecuteCommands(Submarine submarine, IEnumerable<Command> commands) => commands
            .Aggregate(submarine, ExecuteCommand)
            .GetSignature();


        internal enum DirectionEnum
        {
            Forward,
            Up,
            Down
        }

        internal class Command
        {
            public DirectionEnum Direction { get; }
            public int Value { get; }

            private Command(DirectionEnum direction, int value)
            {
                Direction = direction;
                Value = value;
            }

            public static Command FromRawData(string raw)
            {
                var rawSplit = raw.Split(' ');
                return new Command(Enum.Parse<DirectionEnum>(rawSplit[0], true), int.Parse(rawSplit[1]));
            }
        }

        internal abstract class Submarine
        {
            protected int X { get; }
            protected int Y { get; }

            protected Submarine(int x, int y)
            {
                X = x;
                Y = y;
            }

            public Submarine Execute(Command command) => command.Direction == DirectionEnum.Forward
                ? HandleHorizontalCommand(command.Value)
                : HandleVerticalCommand(command.Direction, command.Value);

            protected abstract Submarine HandleHorizontalCommand(int value);
            protected abstract Submarine HandleVerticalCommand(DirectionEnum direction, int value);

            public int GetSignature() => X * Y;
        }

        internal class SimpleSubmarine : Submarine
        {
            private SimpleSubmarine(int x, int y) : base(x, y)
            {
            }

            protected override Submarine HandleHorizontalCommand(int value) => new SimpleSubmarine(X + value, Y);

            protected override Submarine HandleVerticalCommand(DirectionEnum direction, int value) =>
                new SimpleSubmarine(X, Y + (direction == DirectionEnum.Up ? -1 * value : value));

            public static SimpleSubmarine Initial => new(0, 0);
        }

        internal class ProSubmarine : Submarine
        {
            private int Aim { get; }

            private ProSubmarine(int x, int y, int aim) : base(x, y)
            {
                Aim = aim;
            }

            protected override Submarine HandleHorizontalCommand(int value) =>
                new ProSubmarine(X + value, Y + Aim * value, Aim);

            protected override Submarine HandleVerticalCommand(DirectionEnum direction, int value) =>
                new ProSubmarine(X, Y, Aim + (direction == DirectionEnum.Up ? -1 * value : value));

            public static ProSubmarine Initial => new(0, 0, 0);
        }
    }
}