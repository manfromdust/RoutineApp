using System;
using System.Collections.Generic;
using System.Text;

using Expedition178.GameMechanics;
using Expedition178.Game.Parameters;
using Expedition178.Interfaces;

namespace Expedition178.Characters
{
    public abstract class Character
    {
        public readonly string name;
        public int Attack { get; protected set; }
        public int MaxLife { get; protected set; }
        protected int life;
        public int Speed { get; protected set; }
        protected readonly CharacterType charType;
        public AttackType AttackType { get; protected set; }

        public Character(string name, CharacterType charType, AttackType attackType, IRandomGenerator generator)
        {
            this.name = name;
            this.AttackType = attackType;
            this.charType = charType;

            switch (charType)
            {
                case CharacterType.Tank:
                    Attack = generator.GetNext(Parameters.MinTankAttack, Parameters.MaxTankAttack);
                    MaxLife = generator.GetNext(Parameters.MinTankLife, Parameters.MaxTankLife);
                    life = MaxLife;
                    Speed = generator.GetNext(Parameters.MinTankSpeed, Parameters.MaxTankSpeed);
                    break;
                case CharacterType.Dps:
                    Attack = generator.GetNext(Parameters.MinDpsAttack, Parameters.MaxDpsAttack);
                    MaxLife = generator.GetNext(Parameters.MinDpsLife, Parameters.MaxDpsLife);
                    life = MaxLife;
                    Speed = generator.GetNext(Parameters.MinDpsSpeed, Parameters.MaxDpsSpeed);
                    break;
            }
        }

        public bool IsAlive()
        {
            return life > 0;
        }

        public virtual void TakeDamage(int damage, AttackType attackType, string enemyName)
        {
            life -= damage;
            if (life < 0) life = 0;
            Console.WriteLine($"{enemyName} has dealt {damage} damage to {this.name}.");
            Console.WriteLine($"{this.name} now has {this.life} out of {this.MaxLife}.");
        }

        public override string ToString()
        {
            return $"{name} ({charType}, {AttackType}) - Attack: {Attack}, Life: {life}/{MaxLife}, Speed: {Speed}";
        }
    }
}
