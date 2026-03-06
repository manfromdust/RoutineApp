using System;
using System.Collections.Generic;
using System.Text;

using Expedition178.GameMechanics;

namespace Expedition178.Characters
{
    public class Adventurer : Character
    {
        private int level = 1;
        private int experience = 0;

        public Adventurer(string name, CharacterType charType, AttackType attackType) : base(name, charType, attackType) { }

        public override string ToString()
        {
            return $"{base.ToString()}, Level: {level}, Experience {experience}.";
        }
    }
}
