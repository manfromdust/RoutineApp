using System;
using System.Collections.Generic;
using System.Text;

using Expedition178.Interfaces;

namespace Expedition178.GameMechanics
{
    public class RandomGenerator : IRandomGenerator
    {
        public int GetNext(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max + 1);
        }
    }
}
