using System;
using System.Collections.Generic;
using System.Text;

using Expedition178.GameMechanics;
using Expedition178.Interfaces;

namespace Expedition178.Characters
{
    public class Monster : Character
    {
        private MonsterType MonsterType { get; init; }
        private Dictionary<AttackType, float> damage = new();
        public Monster(string name, CharacterType charType, AttackType attackType,
                       MonsterType monType, int wave, IRandomGenerator generator) :
                       base(name, charType, attackType, generator)
        {
            // Scale monster stats based on wave number
            Attack += (wave - 1) * generator.GetNext(5, 10);
            MaxLife += (wave - 1) * generator.GetNext(20, 32);
            life = MaxLife;
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

        public override void TakeDamage(int damage, AttackType attackType, string enemyName)
        {
            int modifiedDamage = (int)(damage * this.damage[attackType]);
            base.TakeDamage(modifiedDamage, attackType, enemyName);
        }
    }
}
