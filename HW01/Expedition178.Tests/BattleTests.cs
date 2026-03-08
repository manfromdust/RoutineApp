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
    }
}
