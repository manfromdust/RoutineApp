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

        public Character[] PerformBattle(Adventurer[] player, Monster[] enemy)
        {
            int playerIndex = 0;
            int enemyIndex = 0;

            while (playerIndex < player.Length && enemyIndex < enemy.Length)
            {
                var winner = PerformRound(player[playerIndex], enemy[enemyIndex]);

                if (winner is Adventurer)
                {
                    if (!enemy[enemyIndex].IsAlive())
                    {
                        enemyIndex++;
                    }
                }
                else
                {
                    if (!player[playerIndex].IsAlive())
                    {
                        playerIndex++;
                    }
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

        }
}
