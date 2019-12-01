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

    // TODO: implement HPMax !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
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

    static public Card DimonCard()
    {
      return new Card(-5, 1, -1);
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;

      Card card2 = (Card)obj;

      return card2.ManaCost == this.ManaCost &&
        card2.HP == this.HP &&
        card2.Attack == this.Attack &&
        (card2.StartAction == this.StartAction || card2.StartAction.Equals(this.StartAction)) &&
        (card2.DieAction == this.DieAction || card2.DieAction.Equals(this.DieAction)) &&
        (card2.UseAction == this.UseAction || card2.UseAction.Equals(this.UseAction)) &&
        (card2.AttackProcessAction == this.AttackProcessAction || card2.AttackProcessAction.Equals(this.AttackProcessAction));
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}