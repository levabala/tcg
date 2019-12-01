using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  class ActionSet
  {
    public static Dictionary<ActionType, Func<GameState, int[], GameState>> actions = new Dictionary<ActionType, Func<GameState, int[], GameState>>
        {
            {ActionType.Attack, Attack },
        };

    private static GameState Attack(GameState state, int[] args)
    {
      int attackerCardIndex = (int)args[0];
      int targetCardIndex = (int)args[1];

      // please, do not use "var"
      var attacker = state.CurrentPlayer;
      var target = state.Players[0].Id != attacker.Id ? state.Players[0] : state.Players[1];

      var attackerCard = attacker.ActiveCards[attackerCardIndex];
      var targetCard = target.ActiveCards[targetCardIndex];

      attackerCard.HP -= targetCard.Attack;
      targetCard.HP -= attackerCard.Attack;

      return state;
    }
  }
}
