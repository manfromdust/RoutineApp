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
        public int attack;
        protected int life;
        public int Speed { get; protected set; }
        protected readonly CharacterType charType;
        protected readonly AttackType attackType;

        public Character(string name, CharacterType charType, AttackType attackType)
        {
            this.name = name;
            this.attackType = attackType;
            this.charType = charType;

            Random random = new Random();

            switch (charType)
            {
                case CharacterType.Tank:
                    attack = random.Next(Parameters.MinTankAttack, Parameters.MaxTankAttack + 1);
                    life = random.Next(Parameters.MinTankLife, Parameters.MaxTankLife + 1);
                    Speed = random.Next(Parameters.MinTankSpeed, Parameters.MaxTankSpeed + 1);
                    break;
                case CharacterType.Dps:
                    attack = random.Next(Parameters.MinDpsAttack, Parameters.MaxDpsAttack + 1);
                    life = random.Next(Parameters.MinDpsLife, Parameters.MaxDpsLife + 1);
                    Speed = random.Next(Parameters.MinDpsSpeed, Parameters.MaxDpsSpeed + 1);
                    break;
            }
        }

        public bool IsAlive()
        {
            return life > 0;
        }

        public virtual void TakeDamage(int damage, AttackType attackType)
        {
            life -= damage;
            if (life < 0) life = 0;
        }

        public override string ToString()
        {
            return $"{name} ({charType}, {attackType}) - Attack: {attack}, Life: {life}, Speed: {Speed}";
        }
    }
}
