using System;
using System.Collections.Generic;
using System.Text;
using Expedition178.Characters;
using Expedition178.Game.Parameters;
using Expedition178.Interfaces;

namespace Expedition178.GameMechanics
{
    public static class CharacterGenerator
    {
        public static Adventurer[] GenerateAdventurers(IRandomGenerator generator)
        {
            var adventurers = new Adventurer[Parameters.MaxAdventurers];
            var names = NameGenerator.GenerateName(Parameters.AdventuresChoiseCount);
            Random random = new Random();

            for (int i = 0; i < Parameters.MaxAdventurers; i++)
            {
                adventurers[i] = new Adventurer(names[i],
                                                (CharacterType)random.Next(Enum.GetValues<CharacterType>().Length),
                                                (AttackType)random.Next(Enum.GetValues<AttackType>().Length),
                                                generator);
            }
            return adventurers;
        }

        public static Monster[] GenerateMonsters(int wave, IRandomGenerator generator)
        {
            var monsters = new Monster[Parameters.MaxMonsters];
            var names = NameGenerator.GenerateName(Parameters.MaxMonsters);
            Random random = new Random();
            for (int i = 0; i < Parameters.MaxMonsters; i++)
            {
                monsters[i] = new Monster(names[i],
                                          (CharacterType)random.Next(Enum.GetValues<CharacterType>().Length),
                                          (AttackType)random.Next(Enum.GetValues<AttackType>().Length),
                                          (MonsterType)random.Next(Enum.GetValues<MonsterType>().Length),
                                          wave,
                                          generator);
            }
            return monsters;
        }
    }
}
