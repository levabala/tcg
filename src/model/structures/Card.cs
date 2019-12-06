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
    public int MaxHP { get; set; }
    public int Attack { get; set; }
    public bool IsSleeping { get; set; }
    public List<Delegate> OnStartAction { get; set; } = new List<Delegate>();
    public List<Delegate> OnUseAction { get; set; } = new List<Delegate>();
    public List<Delegate> OnDieAction { get; set; } = new List<Delegate>();
    public List<Delegate> OnOtherAttackAction { get; set; } = new List<Delegate>();

    public Card(int mana, int hp, int attack, bool isSleeping = true, List<Delegate> startAction = null, List<Delegate> useAction = null, List<Delegate> dieAction = null, List<Delegate> attackProcessAction = null)
    {
      ManaCost = mana;
      HP = hp;
      MaxHP = hp;
      Attack = attack;
      IsSleeping = isSleeping;

      if (startAction != null)
        OnStartAction = startAction;
      if (useAction != null)
        OnUseAction = useAction;
      if (dieAction != null)
        OnDieAction = dieAction;
      if (attackProcessAction != null)
        OnOtherAttackAction = attackProcessAction;
    }

    public override string ToString()
    {
      return String.Format("Mana cost: {0}, HP: {1}, Attack: {2}", ManaCost, HP, Attack);
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;

      Card card2 = (Card)obj;

      return card2.ManaCost == this.ManaCost &&
        card2.HP == this.HP &&
        card2.Attack == this.Attack &&
        card2.IsSleeping == this.IsSleeping &&
        (card2.OnStartAction.SequenceEqual(this.OnStartAction)) &&
        (card2.OnDieAction.SequenceEqual(this.OnDieAction)) &&
        (card2.OnUseAction.SequenceEqual(this.OnUseAction)) &&
        (card2.OnOtherAttackAction.SequenceEqual(this.OnOtherAttackAction));
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    static public Card DimonCard()
    {
      var card = new Card(-5, 1, -1);
      card.IsSleeping = false;
      return card;
    }

    static public Card DimonStrongCard()
    {
      return new Card(-5, 10, -1);
    }

    static public Card LevCard()
    {
      return new Card(100, 100, 100);
    }

    static public Card LevBudgetCard()
    {
      return new Card(10, 100, 100);
    }
  }
}