using System;
using System.Collections.Generic;
using System.Text;
using Expedition178.Characters;

namespace Expedition178.GameMechanics
{
    public static class CharacterGenerator
    {
        public static Adventurer[] GenerateAdventurers(int count)
        {
            var adventurers = new Adventurer[count];
            var names = NameGenerator.GenerateName(6);
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                adventurers[i] = new Adventurer(names[i],
                                                (CharacterType)random.Next(Enum.GetValues<CharacterType>().Length),
                                                (AttackType)random.Next(Enum.GetValues<AttackType>().Length));
            }
            return adventurers;
        }

        public static Monster[] GenerateMonsters(int wave)
        {
            var monsters = new Monster[3];
            var names = NameGenerator.GenerateName(3);
            Random random = new Random();
            for (int i = 0; i < 3; i++)
            {
                monsters[i] = new Monster(names[i],
                                          (CharacterType)random.Next(Enum.GetValues<CharacterType>().Length),
                                          (AttackType)random.Next(Enum.GetValues<AttackType>().Length),
                                          (MonsterType)random.Next(Enum.GetValues<MonsterType>().Length),
                                          wave);
            }
            return monsters;
        }
    }
}
