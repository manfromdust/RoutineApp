using System;
using System.Collections.Generic;
using System.Text;

using Expedition178.GameMechanics;
using Expedition178.Game.Parameters;

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

        public Character(string name, CharacterType charType, AttackType attackType)
        {
            this.name = name;
            this.AttackType = attackType;
            this.charType = charType;

            Random random = new Random();

            switch (charType)
            {
                case CharacterType.Tank:
                    Attack = random.Next(Parameters.MinTankAttack, Parameters.MaxTankAttack + 1);
                    MaxLife = random.Next(Parameters.MinTankLife, Parameters.MaxTankLife + 1);
                    life = MaxLife;
                    Speed = random.Next(Parameters.MinTankSpeed, Parameters.MaxTankSpeed + 1);
                    break;
                case CharacterType.Dps:
                    Attack = random.Next(Parameters.MinDpsAttack, Parameters.MaxDpsAttack + 1);
                    MaxLife = random.Next(Parameters.MinDpsLife, Parameters.MaxDpsLife + 1);
                    life = MaxLife;
                    Speed = random.Next(Parameters.MinDpsSpeed, Parameters.MaxDpsSpeed + 1);
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
