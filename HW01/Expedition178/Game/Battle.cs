using Expedition178.Characters;
using Expedition178.GameMechanics;
using Expedition178.Game.Parameters;
using Expedition178.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;

namespace Expedition178.Game
{
    public class Battle : IBattle
    {
        public Monster[] Monsters { get; init; }

        public Battle(int wave, IRandomGenerator generator)
        {
            Monsters = CharacterGenerator.GenerateMonsters(wave, generator);
        }

        private bool IsAdventurerFaster(Adventurer adventurer, Monster creature)
        {
            return adventurer.Speed >= creature.Speed;
        }

        private void HealAdventurers(Adventurer[] adventurers)
        {
            foreach (var adventurer in adventurers)
            {
                adventurer.Heal(adventurer.MaxLife);
            }
        }

        public Character[] PerformBattle(Adventurer[] player, Monster[] enemy)
        {
            int playerIndex = 0;
            int enemyIndex = 0;

            while (playerIndex < player.Length && enemyIndex < enemy.Length)
            {
                var winner = PerformRound(player[playerIndex], enemy[enemyIndex]);

                if (winner is Adventurer)
                {
                    enemyIndex++;
                    player[playerIndex].GainExperience(Parameters.Parameters.WonRound);
                }
                else
                {
                    playerIndex++;
                }
            }

            int experienceMultiplier = (playerIndex < enemyIndex) ? Parameters.Parameters.WonMultiplier : 1;

            foreach (var adventurer in player)
            {
                adventurer.GainExperience(experienceMultiplier);
            }

            HealAdventurers(player);

            return (playerIndex < enemyIndex) ? player : enemy;
        }

        public Character PerformRound(Adventurer adventurer, Monster creature)
        {
            Character first;
            Character second;

            if (IsAdventurerFaster(adventurer, creature))
            {
                first = adventurer;
                second = creature;
            }
            else
            {
                first = creature;
                second = adventurer;
            }

            while (first.IsAlive() && second.IsAlive())
            {
                second.TakeDamage(first.Attack, first.AttackType, first.name);
                if (second.IsAlive())
                {
                    first.TakeDamage(second.Attack, second.AttackType, second.name);
                }
            }

            return (first.IsAlive()) ? first : second;
        }
    }
}
