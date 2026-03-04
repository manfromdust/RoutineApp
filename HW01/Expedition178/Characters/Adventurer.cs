using System;
using System.Collections.Generic;
using System.Text;

using Expedition178.GameMechanics;

namespace Expedition178.Characters
{
    internal class Adventurer : Character
    {
        private int Level { get; set; }

        public Adventurer(string name, CharacterType charType, AttackType attackType) : base(name, charType, attackType)
        {
            Level = 1;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Level: {Level}";
        }
}
