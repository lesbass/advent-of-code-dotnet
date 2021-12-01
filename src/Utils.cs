using System.IO;
using System.Text;

namespace advent_of_code_dotnet
{
    public static class Utils
    {
        /// <summary>
        /// Reads lines from the given input txt file.
        /// </summary>
        public static string[] ReadInput(string name) => File.ReadAllLines(Path.Combine( "inputs", $"{name}.txt"), Encoding.UTF8);
    }
}