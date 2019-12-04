using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  class Card
  {
    public string Name { get; set; }
    public int ManaCost { get; set; }
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int Attack { get; set; }
    public bool IsSleeping { get; set; }
    public Delegate OnPlayAction { get; set; }
    public Delegate OnUseAction { get; set; }
    public Delegate OnDieAction { get; set; }
    public Delegate OnOtherAttackAction { get; set; }

    public Card(string name, int mana, int hp, int attack, bool isSleeping = true, Delegate startAction = null, Delegate useAction = null, Delegate dieAction = null, Delegate attackProcessAction = null)
    {
      Name = name;
      ManaCost = mana;
      HP = hp;
      MaxHP = hp;
      Attack = attack;
      IsSleeping = isSleeping;

      OnPlayAction = startAction;
      OnUseAction = useAction;
      OnDieAction = dieAction;
      OnOtherAttackAction = attackProcessAction;
    }

    // public Card(int mana, int hp, int attack, bool isSleeping, Delegate startAction = null, Delegate useAction = null, Delegate dieAction = null, Delegate attackProcessAction = null) : this(mana, hp, attack)
    // {
    //   IsSleeping = isSleeping;
    // } 

    public override string ToString()
    {
      return String.Format("{0} Mana cost: {1}, HP: {2}, Attack: {3}\n",Name, ManaCost, HP, Attack);
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
        (card2.OnPlayAction == this.OnPlayAction || card2.OnPlayAction.Equals(this.OnPlayAction)) &&
        (card2.OnDieAction == this.OnDieAction || card2.OnDieAction.Equals(this.OnDieAction)) &&
        (card2.OnUseAction == this.OnUseAction || card2.OnUseAction.Equals(this.OnUseAction)) &&
        (card2.OnOtherAttackAction == this.OnOtherAttackAction || card2.OnOtherAttackAction.Equals(this.OnOtherAttackAction));
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
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