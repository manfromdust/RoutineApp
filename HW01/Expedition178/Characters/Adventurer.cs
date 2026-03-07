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

        private void CheckLevelUp()
        {
            if (experience >= 100)
            {
                level++;
                experience -= 100;
                Console.WriteLine($"{name} leveled up to level {level}!");
            }
        }

        public void GainExperience()
        {
            Random random = new Random();
            experience += random.Next(40, 81);
            CheckLevelUp();
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Level: {level}, Experience {experience}.";
        }
    }
}
