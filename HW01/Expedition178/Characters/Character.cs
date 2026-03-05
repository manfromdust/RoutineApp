using System;
using System.Collections.Generic;
using System.Text;

using Expedition178.GameMechanics;

namespace Expedition178.Characters
{
    internal abstract class Character
    {
        public readonly string name;
        protected int attack;
        protected int life;
        protected int speed;
        protected readonly CharacterType charType;
        protected readonly AttackType attackType;

        public Character(string name, CharacterType charType, AttackType attackType)
        {
            this.name = name;
            this.attackType = attackType;
            this.charType = charType;

            switch (charType)
            {
                case CharacterType.Tank:
                    attack = 15;
                    life = 120;
                    speed = 5;
                    break;
                case CharacterType.Dps:
                    attack = 30;
                    life = 70;
                    speed = 10;
                    break;
            }
        }

        public override string ToString()
        {
            return $"{name} ({charType}, {attackType}) - Attack: {attack}, Life: {life}, Speed: {speed}";
        }
    }
}
