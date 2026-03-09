using Expedition178.Characters;
using Expedition178.Game;
using Expedition178.GameMechanics;
using Expedition178.Interfaces;

namespace Expedition178.Tests
{
    public class BattleTests
    {
        public class FakeRandomGenerator : IRandomGenerator
        {
            public int GetNext(int min, int max)
            {
                return 1;
            }
        }

        [Fact]
        public void EqualsStatsPlayerWins()
        {
            var random = new FakeRandomGenerator();
            var battle = new Battle(1, random);
            var adventurer = new Adventurer("Test01", CharacterType.Dps, AttackType.Fire, random);
            var monster = new Monster("Test02", CharacterType.Tank, AttackType.Dark, MonsterType.Radiant, 1, random);

            var expected = adventurer;

            Assert.Equal(expected, battle.PerformRound(adventurer, monster));
        }

        [Fact]
        public void MonsterBetterStatsWins()
        {
            var random = new FakeRandomGenerator();
            var battle = new Battle(1, random);
            var adventurer = new Adventurer("Test01", CharacterType.Dps, AttackType.Fire, random);
            var monster = new Monster("Test02", CharacterType.Tank, AttackType.Dark, MonsterType.Radiant, 2, random);

            var expected = monster;

            Assert.Equal(expected, battle.PerformRound(adventurer, monster));
        }

        [Fact]
        public void EqualStatsPlayerAfterLevelUpWins()
        {
            var random = new FakeRandomGenerator();
            var battle = new Battle(1, random);
            var adventurer = new Adventurer("Test01", CharacterType.Dps, AttackType.Fire, random);
            var monster = new Monster("Test02", CharacterType.Tank, AttackType.Dark, MonsterType.Radiant, 1, random);
            adventurer.GainExperience(100);

            var expected = adventurer;

            Assert.Equal(expected, battle.PerformRound(adventurer, monster));
        }

        [Fact]
        public void NobodyWinningReturnsNull()
        {
            var random = new FakeRandomGenerator();
            var battle = new Battle(1, random);
            var adventurer = new Adventurer("Test01", CharacterType.Dps, AttackType.Light, random);
            var monster = new Monster("Test02", CharacterType.Tank, AttackType.Dark, MonsterType.Radiant, 3, random);

            adventurer.GainExperience(400);
            adventurer.Heal(100);

            Character? expected = null;
            Assert.Equal(expected, battle.PerformRound(adventurer, monster));
        }

        [Fact]
        public void OnePlayerAgainstMultipleMonstersOfEqualStats()
        {
            var random = new FakeRandomGenerator();
            var battle = new Battle(1, random);
            var adventurer = new Adventurer("Test01", CharacterType.Dps, AttackType.Fire, random);
            var monster = new Monster("Test02", CharacterType.Dps, AttackType.Dark, MonsterType.Radiant, 1, random);
            var monster2 = new Monster("Test03", CharacterType.Dps, AttackType.Dark, MonsterType.Radiant, 1, random);
            var monster3 = new Monster("Test03", CharacterType.Tank, AttackType.Dark, MonsterType.Radiant, 1, random);
            Monster[] monsters = [monster, monster2, monster3];
            Adventurer[] adventurers = [adventurer];

            var expected = adventurers;

            Assert.Equal(expected, battle.PerformBattle(adventurers, monsters));
        }

        [Fact]
        public void NormalGameEqualStatsPlayerWins()
        {
            var random = new FakeRandomGenerator();
            var battle = new Battle(1, random);
            Adventurer[] adventurers = [new Adventurer("Test01", CharacterType.Dps, AttackType.Fire, random),
                new Adventurer("Test02", CharacterType.Dps, AttackType.Light, random),
                new Adventurer("Test03", CharacterType.Tank, AttackType.Frost, random)
            ];
            Monster[] monsters = [new Monster("Test04", CharacterType.Dps, AttackType.Dark, MonsterType.Natural, 1, random),
                new Monster("Test05", CharacterType.Dps, AttackType.Dark, MonsterType.Shadow, 1, random),
                new Monster("Test06", CharacterType.Tank, AttackType.Dark, MonsterType.Natural, 1, random)
            ];

            var expected = adventurers;

            Assert.Equal(expected, battle.PerformBattle(adventurers, monsters));
        }

        [Fact]
        public void NormalGameButImmuneMonstersMonstersWins()
        {
            var random = new FakeRandomGenerator();
            var battle = new Battle(1, random);
            Adventurer[] adventurers = [new Adventurer("Test01", CharacterType.Dps, AttackType.Light, random),
                new Adventurer("Test02", CharacterType.Dps, AttackType.Light, random),
                new Adventurer("Test03", CharacterType.Tank, AttackType.Dark, random)
            ];
            Monster[] monsters = [new Monster("Test04", CharacterType.Dps, AttackType.Dark, MonsterType.Radiant, 1, random),
                new Monster("Test05", CharacterType.Dps, AttackType.Dark, MonsterType.Radiant, 1, random),
                new Monster("Test06", CharacterType.Tank, AttackType.Dark, MonsterType.Shadow, 1, random)
            ];

            var expected = monsters;

            Assert.Equal(expected, battle.PerformBattle(adventurers, monsters));
        }
    }
}
