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
        protected CharacterType CharType { get; init; }
        protected AttackType AttackType { get; init; }

        public Character(string name, CharacterType charType, AttackType attackType)
        {
            Name = name;
            AttackType = attackType;
            CharType = charType;

            switch (charType)
            {
                case CharacterType.Tank:
                    Attack = 15;
                    Life = 120;
                    Speed = 5;
                    break;
                case CharacterType.Dps:
                    Attack = 30;
                    Life = 70;
                    Speed = 10;
                    break;
            }
        }

        public override string ToString()
        {
            return $"{Name} ({CharType}, {AttackType}) - Attack: {Attack}, Life: {Life}, Speed: {Speed}";
        }
    }
}
