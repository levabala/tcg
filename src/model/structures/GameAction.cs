using System;
using System.Collections.Generic;
using System.Linq;

namespace tcg
{
  class GameAction
  {
    public Func<GameState, int[], GameState> Action { get; set; }

    public GameAction(Func<GameState, int[], GameState> action)
    {
      this.Action = action;
    }
  }
}