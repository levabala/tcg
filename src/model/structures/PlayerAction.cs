using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  class PlayerAction
  {
    public int PlayerId { get; set; }
    public Func<GameState, GameState> Action { get; set; }

    public PlayerAction(int playerId, Func<GameState, GameState> action)
    {
      this.PlayerId = playerId;
      this.Action = action;
    }

    public override bool Equals(object obj) {
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
