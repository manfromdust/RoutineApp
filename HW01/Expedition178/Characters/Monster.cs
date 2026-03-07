using System;
using System.Collections.Generic;
using System.Text;

using Expedition178.GameMechanics;

namespace Expedition178.Characters
{
    public class Monster : Character
    {
        private MonsterType MonsterType { get; init; }
        private Dictionary<AttackType, float> damage = new();
        public Monster(string name, CharacterType charType, AttackType attackType, MonsterType monType, int wave) : base(name, charType, attackType)
        {
            // Scale monster stats based on wave number
            attack += wave * 2;
            life += wave * 20;
            Speed += wave;
            MonsterType = monType;

            switch (monType)
            {
                case MonsterType.Natural:
                    damage[AttackType.Fire] = 1.5f;
                    damage[AttackType.Frost] = 1.5f;
                    damage[AttackType.Physical] = 1.0f;
                    damage[AttackType.Light] = 0.5f;
                    damage[AttackType.Dark] = 1.0f;
                    break;
                case MonsterType.Radiant:
                    damage[AttackType.Fire] = 1.0f;
                    damage[AttackType.Frost] = 1.0f;
                    damage[AttackType.Physical] = 1.0f;
                    damage[AttackType.Light] = 0.0f;
                    damage[AttackType.Dark] = 1.5f;
                    break;
                case MonsterType.Shadow:
                    damage[AttackType.Fire] = 1.0f;
                    damage[AttackType.Frost] = 1.0f;
                    damage[AttackType.Physical] = 0.5f;
                    damage[AttackType.Light] = 1.5f;
                    damage[AttackType.Dark] = 0.0f;
                    break;
            }
        }
    }
}
