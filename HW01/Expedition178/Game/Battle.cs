using Expedition178.Characters;
using Expedition178.GameMechanics;
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
            return adventurer.Attack > creature.Attack;
        }

        public Character[] PerformBattle(Adventurer[] player, Monster[] enemy)
        {
            
        }

        public Character PerformRound(Adventurer adventurer, Monster creature)
        {

        }
}
