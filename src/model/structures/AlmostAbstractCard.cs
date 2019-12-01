using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  class AlmostAbstractCard
  {
    public int ManaCost { get; set; }
    public int HP { get; set; }
    public int Attack { get; set; }
    public Func<GameState, int[], GameState> StartAction { get; set; }
    public Func<GameState, int[], GameState> UseAction { get; set; }
    public Func<GameState, int[], GameState> DieAction { get; set; }
    public Func<GameState, int[], GameState> AttackProcessAction { get; set; }

    public AlmostAbstractCard(int mana, int hp, int attack, Func<GameState, int[], GameState> startAction = null, Func<GameState, int[], GameState> useAction = null, Func<GameState, int[], GameState> dieAction = null, Func<GameState, int[], GameState> attackProcessAction = null)
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

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;

      AlmostAbstractCard card2 = (AlmostAbstractCard)obj;

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