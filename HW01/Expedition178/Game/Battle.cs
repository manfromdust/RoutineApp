using Expedition178.Characters;
using Expedition178.GameMechanics;
using Expedition178.Game.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expedition178.Game
{
    public class Battle : IBattle
    {
        public Monster[] Monsters { get; init; }

        public Battle(int wave)
        {
            Monsters = CharacterGenerator.GenerateMonsters(wave);
        }

        private bool IsAdventurerFaster(Adventurer adventurer, Monster creature)
        {
            return adventurer.Speed > creature.Speed;
        }

        private void HealAdventurers(Adventurer[] adventurers)
        {
            foreach (var adventurer in adventurers)
            {
                adventurer.Heal();
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
                second.TakeDamage(first.Attack, first.AttackType);
                if (second.IsAlive())
                {
                    first.TakeDamage(second.Attack, second.AttackType);
                }
            }

            return (first.IsAlive()) ? first : second;
        }
    }
}
