using System;
using System.Collections.Generic;
using System.Text;

using Expedition178.GameMechanics;

namespace Expedition178.Characters
{
    internal abstract class Character
    {
        public string Name { protected get; init; }
        protected int Attack { get; set; }
        protected int Life { get; set; }
        protected int Speed { get; set; }
        protected CharacterType Type { get; init; }
        protected AttackType AttackType { get; init; }

        public Character(string name, int attack)
        {
            Name = name;
        }
    }
}
