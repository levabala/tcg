using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  static class GameLoop
  {
    public static GameState Execute(GameState state, Action<GameState> action)
    {
      var processChanges = ActionSet.PackAction(state, ActionType.SaveChanges);
      processChanges(state);

      // apply main action
      action(state);

      // apply death check action
      var processDeathAction = ActionSet.PackAction(state, ActionType.ProcessDeath);
      processDeathAction(state);

      return state;
    }
  }
}
