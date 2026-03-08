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
        private Dictionary<AttackType, DmgModifiers> damage = new();

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
                    damage[AttackType.Fire] = DmgModifiers.Weakness;
                    damage[AttackType.Frost] = DmgModifiers.Weakness;
                    damage[AttackType.Physical] = DmgModifiers.None;
                    damage[AttackType.Light] = DmgModifiers.Resistance;
                    damage[AttackType.Dark] = DmgModifiers.None;
                    break;
                case MonsterType.Radiant:
                    damage[AttackType.Fire] = DmgModifiers.None;
                    damage[AttackType.Frost] = DmgModifiers.None;
                    damage[AttackType.Physical] = DmgModifiers.None;
                    damage[AttackType.Light] = DmgModifiers.Immunity;
                    damage[AttackType.Dark] = DmgModifiers.Weakness;
                    break;
                case MonsterType.Shadow:
                    damage[AttackType.Fire] = DmgModifiers.None;
                    damage[AttackType.Frost] = DmgModifiers.None;
                    damage[AttackType.Physical] = DmgModifiers.Resistance;
                    damage[AttackType.Light] = DmgModifiers.Weakness;
                    damage[AttackType.Dark] = DmgModifiers.Immunity;
                    break;
            }
        }

        public override void TakeDamage(int damage, AttackType attackType, string enemyName)
        {
            int modifiedDamage = 0;
            switch (this.damage[attackType])
            {
                case DmgModifiers.None:
                    modifiedDamage = damage;
                    break;
                case DmgModifiers.Weakness:
                    modifiedDamage = (int) Math.Ceiling(damage * ((double)DmgModifiers.Weakness / 10.0));
                    break;
                case DmgModifiers.Resistance:
                    modifiedDamage = (int) Math.Floor(damage * ((double)DmgModifiers.Weakness / 10.0));
                    break;
                case DmgModifiers.Immunity:
                    modifiedDamage = 0;
                    break;
            }

            base.TakeDamage(modifiedDamage, attackType, enemyName);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Type: {MonsterType}.";
        }
    }
}
