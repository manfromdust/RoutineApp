using System;
using System.Collections.Generic;
using System.Text;

using Expedition178.GameMechanics;

namespace Expedition178.Characters
{
    internal class Monster : Character
    {
        public Monster(string name, CharacterType charType, AttackType attackType) : base(name, charType, attackType) { }
    }
}
