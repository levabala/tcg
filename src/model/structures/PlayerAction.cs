using System;
using System.Collections.Generic;
using System.Linq;

namespace tcg
{
  class PlayerAction : GameAction
  {
    public int PlayerId { get; set; }

    public PlayerAction(Func<GameState, int[], GameState> action, int playerId) : base(action)
    {
      this.PlayerId = playerId;
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;

      PlayerAction act2 = (PlayerAction)obj;
      return act2.PlayerId == this.PlayerId && act2.Action == this.Action;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
