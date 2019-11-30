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
            {ActionType.Die, Die},
            {ActionType.Print, Print}
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

    private static GameState Die(GameState state)
    {
      Player attacker = state.CurrentPlayer;
      Player target = state.Players[0].Id != attacker.Id ? state.Players[0] : state.Players[1];

      Card attackerCard = attacker.ActiveCards[state.Attacker];
      Card targetCard = target.ActiveCards[state.Target];

      if (attackerCard.HP <= 0)
      {
        if (attackerCard.DieAction != null)
        {
          ActionType type = attackerCard.DieAction.Type;
          state = actions[type](state);
        }
        attacker.ActiveCards[state.Attacker] = null;
      }

      if (targetCard.HP <= 0)
      {
        if (targetCard.DieAction != null)
        {
          ActionType type = targetCard.DieAction.Type;
          state = actions[type](state);
        }
        target.ActiveCards[state.Target] = null;
      }

      return state;
    }

    private static GameState Print(GameState state)
    {
      Console.WriteLine("Hi");
      return state;
    }
  }
}
