using System;
using System.Collections.Generic;
using System.Text;

using Expedition178.GameMechanics;
using Expedition178.Interfaces;

namespace Expedition178.Characters
{
    public class Adventurer : Character
    {
        private int level = 1;
        private int experience = 0;

        public Adventurer(string name, CharacterType charType, AttackType attackType, IRandomGenerator generator) :
            base(name, charType, attackType, generator) { }

        private void CheckLevelUp()
        {
            while (experience >= 100)
            {
                level++;
                experience -= 100;
                MaxLife += generator.GetNext(20, 30);
                Attack += generator.GetNext(5, 10);
                Console.WriteLine($"{name} leveled up to level {level}!");
            }
        }

        public void GainExperience(int multiplier)
        {
            experience += multiplier * generator.GetNext(20, 50);
            CheckLevelUp();
        }

        public void Heal(int amount)
        {
            life += amount;
            if (life > MaxLife) life = MaxLife;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Level: {level}, Experience {experience}.";
        }
    }
}
