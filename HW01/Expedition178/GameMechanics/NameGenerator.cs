using System;
using System.Collections.Generic;
using System.Text;

namespace Expedition178.GameMechanics
{
    internal static class NameGenerator
    {
        private static readonly string path = "names.txt";

        public static string GenerateName()
        {
            var names = File.ReadAllLines(path);
            var random = new Random();
            return names[random.Next(names.Length)];
        }
    }
}
