using System;
using System.Collections.Generic;
using System.Linq;
using Pastel;
using System.Drawing;
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

    public Card previous { get; set; }
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
      string MPstring = string.Format("Mana cost: {0}\t", ManaCost).Pastel(Color.MediumTurquoise);
      string HPstring =  string.Format("HP: {0}", HP).Pastel(Color.HotPink);
      string ATKstring = string.Format("Attack: {0}", Attack).Pastel(Color.Gold);
      if (this.previous == null)
        return string.Format("{0,-20} {1} {2}\t\t {3}\n\t{4}\n", Name, MPstring, HPstring, ATKstring, Description);
      else if (this.previous.Equals(this))
        return string.Format("{0,-20} {1} {2}+0\t\t {3}+0\n\t{4}\n", Name, MPstring, HPstring, ATKstring, Description);
      else{

        if (this.HP != this.previous.HP)
          if(this.HP > this.previous.HP)
            HPstring += string.Format(" (+{0})", this.HP - this.previous.HP).Pastel(Color.Lime);
          else
            HPstring += string.Format(" (-{0})", this.previous.HP - this.HP).Pastel(Color.Crimson);
        
        HPstring += ",\t";

        if (this.Attack != this.previous.Attack)
          if(this.Attack > this.previous.Attack)
            ATKstring += string.Format(" (+{0})", this.Attack - this.previous.Attack).Pastel(Color.Lime);
          else
            ATKstring += string.Format(" (-{0})", this.previous.Attack - this.Attack).Pastel(Color.Crimson);
        
        ATKstring += ",\t";

        return string.Format("{0,-16} {1} {2}\t\t {3}\n\t{4}\n", Name, MPstring, HPstring, ATKstring, Description);
      }
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