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
                Random random = new Random();
                level++;
                experience -= 100;
                maxLife += random.Next(20, 31);
                Attack += random.Next(5, 11);
                Console.WriteLine($"{name} leveled up to level {level}!");
            }
        }

        public void GainExperience(int multiplier)
        {
            Random random = new Random();
            experience += multiplier * random.Next(20, 51);
            CheckLevelUp();
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Level: {level}, Experience {experience}.";
        }
    }
}
