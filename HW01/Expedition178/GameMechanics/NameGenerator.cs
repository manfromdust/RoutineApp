using System;
using System.Collections.Generic;
using System.Text;

namespace Expedition178.GameMechanics
{
    public static class NameGenerator
    {
        private static readonly string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                           "..", "..", "..", "Data", "names.txt");
        private static readonly string[] names = File.ReadAllLines(path);

        public static string[] GenerateName(int count)
        {
            var random = new Random();
            var indexes = new HashSet<int>();
            string[] result = new string[count];

            for (int i = 0; i < count; i++)
            {
                int index;
                do
                {
                    index = random.Next(names.Length);
                } while (!indexes.Contains(index));
                
                indexes.Add(index);
                result[i] = names[index];
            }
            return result;
        }
    }
}
