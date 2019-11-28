using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
    struct Card
    {
        public int ManaCost {get; set;}
        public int HP {get; set;}
        public int Attack {get; set;}
        public Action StartAction {get; set;}
        public Action UseAction {get; set;}
        public Action DieAction {get; set;}
        public Action AttackProcessAction {get; set;}

        public Card(int mana, int hp, int attack)
        {
            ManaCost = mana;
            HP = hp;
            Attack = attack;
            StartAction = null;
            UseAction = null;
            DieAction = null;
            AttackProcessAction = null;
        }

        public override string ToString()
        {
            return String.Format("Mana cost: {0}, HP: {1}, Attack: {2}", ManaCost, HP, Attack);
        }
    }
}
