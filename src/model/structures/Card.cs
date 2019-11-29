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
    public CardAction StartAction { get; set; }
    public CardAction UseAction { get; set; }
    public CardAction DieAction { get; set; }
    public CardAction AttackProcessAction { get; set; }

    public Card(int mana, int hp, int attack, CardAction startAction = null, CardAction useAction = null, CardAction dieAction = null, CardAction attackProcessAction = null)
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
