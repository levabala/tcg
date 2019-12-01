using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  class Card : AlmostAbstractCard
  {
    new public Func<GameState, int[], GameState> StartAction { get; set; }
    new public Func<GameState, int[], GameState> UseAction { get; set; }
    new public Func<GameState, int[], GameState> DieAction { get; set; }
    new public Func<GameState, int[], GameState> AttackProcessAction { get; set; }

    public Card(int mana, int hp, int attack, Func<GameState, int[], GameState> startAction = null, Func<GameState, int[], GameState> useAction = null, Func<GameState, int[], GameState> dieAction = null, Func<GameState, int[], GameState> attackProcessAction = null) : base(mana, hp, attack, useAction, dieAction, attackProcessAction)
    {

    }

    static public Card DimonCard()
    {
      return new Card(-5, 1, -1);
    }
  }
}