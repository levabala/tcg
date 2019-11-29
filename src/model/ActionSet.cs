using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  class ActionSet
  {
    public static Dictionary<ActionType, Func<GameState, GameState>> actions = new Dictionary<ActionType, Func<GameState, GameState>>
        {
            {ActionType.Attack, Attack },
            {ActionType.HealSelf, HealSelf},
        };

    private static GameState Attack(GameState state)
    {
      Player attacker = state.CurrentPlayer;
      Player target = state.Players[0].Id != attacker.Id ? state.Players[0] : state.Players[1];

      Card attackerCard = attacker.ActiveCards[state.Attacker];
      Card targetCard = target.ActiveCards[state.Target];

      if (attackerCard.UseAction != null)
      {
        ActionType type = attackerCard.UseAction.Type;
        state = actions[type](state);
      }

      attackerCard.HP -= targetCard.Attack;
      targetCard.HP -= attackerCard.Attack;

      return state;
    }

    private static GameState HealSelf(GameState state)
    {
      Player attacker = state.CurrentPlayer;
      Player target = state.Players[0].Id != attacker.Id ? state.Players[0] : state.Players[1];

      Card attackerCard = attacker.ActiveCards[state.Attacker];
      Card targetCard = target.ActiveCards[state.Target];
      attackerCard.HP += attackerCard.UseAction.Power;
      return state;
    }
  }
}
