using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  class Card
  {
    public int ManaCost { get; set; }
    public int HP { get; set; }
    public int Attack { get; set; }
    public PlayerAction StartAction { get; set; }
    public PlayerAction UseAction { get; set; }
    public PlayerAction DieAction { get; set; }
    public PlayerAction AttackProcessAction { get; set; }

    public Card(int mana, int hp, int attack, PlayerAction startAction = null, PlayerAction useAction = null, PlayerAction dieAction = null, PlayerAction attackProcessAction = null)
    {
      ManaCost = mana;
      HP = hp;
      Attack = attack;

      StartAction = startAction;
      UseAction = useAction;
      DieAction = dieAction;
      AttackProcessAction = attackProcessAction;
    }

    public override string ToString()
    {
      return String.Format("Mana cost: {0}, HP: {1}, Attack: {2}", ManaCost, HP, Attack);
    }
  }
}
