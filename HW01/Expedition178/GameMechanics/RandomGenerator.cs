using System;
using System.Collections.Generic;
using System.Text;

using Expedition178.Interfaces;

namespace Expedition178.GameMechanics
{
    public class RandomGenerator : IRandomGenerator
    {
        private Random random = new();

        public int GetNext(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }
}
