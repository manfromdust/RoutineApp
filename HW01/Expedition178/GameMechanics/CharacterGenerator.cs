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

            for (int i = 0; i < Parameters.MaxAdventurers; i++)
            {
                adventurers[i] = new Adventurer(names[i],
                                                (CharacterType) generator.GetNext(0, Enum.GetValues<CharacterType>().Length),
                                                (AttackType) generator.GetNext(0, Enum.GetValues<AttackType>().Length),
                                                generator);
            }
            return adventurers;
        }

        public static Monster[] GenerateMonsters(int wave, IRandomGenerator generator)
        {
            var monsters = new Monster[Parameters.MaxMonsters];
            var names = NameGenerator.GenerateName(Parameters.MaxMonsters);

            for (int i = 0; i < Parameters.MaxMonsters; i++)
            {
                monsters[i] = new Monster(names[i],
                                          (CharacterType) generator.GetNext(0, Enum.GetValues<CharacterType>().Length),
                                          (AttackType) generator.GetNext(0, Enum.GetValues<AttackType>().Length),
                                          (MonsterType) generator.GetNext(0, Enum.GetValues<MonsterType>().Length),
                                          wave,
                                          generator);
            }
            return monsters;
        }
    }
}
