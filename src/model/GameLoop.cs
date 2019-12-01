using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  static class GameLoop
  {
    public static GameState Execute(GameState state, Func<GameState, GameState> action)
    {
      return action(state);
    }
  }
}
