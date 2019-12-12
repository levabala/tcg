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
    public bool IsTaunt { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Delegate> OnStartAction { get; set; } = new List<Delegate>();
    public List<Delegate> OnUseAction { get; set; } = new List<Delegate>();
    public List<Delegate> OnDieAction { get; set; } = new List<Delegate>();

    public Card(string name, int mana, int hp, int attack, bool isSleeping = true, bool isTaunt = false, string description = "", List<Delegate> startAction = null, List<Delegate> useAction = null, List<Delegate> dieAction = null)
    {
      Name = name;
      ManaCost = mana;
      HP = hp;
      MaxHP = hp;
      Attack = attack;
      IsSleeping = isSleeping;
      IsTaunt = isTaunt;
      Description = description;

      if (startAction != null)
        OnStartAction = startAction;
      if (useAction != null)
        OnUseAction = useAction;
      if (dieAction != null)
        OnDieAction = dieAction;
    }

    public override string ToString()
    {
      return String.Format("{0}\t\tMana cost: {1}\tHP: {2}\tAttack: {3}\t{4}\n", Name, ManaCost, HP, Attack, Description);
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
        card2.IsTaunt == this.IsTaunt &&
        (card2.OnStartAction.SequenceEqual(this.OnStartAction)) &&
        (card2.OnDieAction.SequenceEqual(this.OnDieAction)) &&
        (card2.OnUseAction.SequenceEqual(this.OnUseAction));
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public Card Clone()
    {
      return new Card(Name, ManaCost, HP, Attack, IsSleeping, IsTaunt, Description, OnStartAction, OnUseAction, OnDieAction);
    }

    static public Card DimonCard()
    {
      var card = new Card("Dimon", -5, 1, -1);
      card.IsSleeping = false;
      return card;
    }

    static public Card DimonStrongCard()
    {
      return new Card("Strong Dimon", -5, 10, -1);
    }

    static public Card LevCard()
    {
      return new Card("Lev", 100, 100, 100);
    }

    static public Card LevBudgetCard()
    {
      return new Card("Budget Lev", 10, 100, 100);
    }
  }
}