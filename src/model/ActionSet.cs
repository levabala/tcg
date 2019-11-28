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
        };

    private static GameState Attack(GameState state)
    {
      // please, do not use "var"
      var attacker = state.CurrentPlayer;
      var target = state.Players[0].Id != attacker.Id ? state.Players[0] : state.Players[1];

      var attackerCard = attacker.ActiveCards[state.Attacker];
      var targetCard = target.ActiveCards[state.Target];

      var newAttackerHP = attackerCard.HP - targetCard.Attack;
      var newTargetHP = targetCard.HP - attackerCard.Attack;

      attackerCard.HP -= targetCard.Attack;
      targetCard.HP -= attackerCard.Attack;

      // i suppose we can use Class instead of Card, Player etc
      // to access them by link
      // anyway, we are not goind to do completely immutable system
      // so we can use mutations where it's the easiest strategy
      attacker.ActiveCards[0] = new Card(attackerCard.ManaCost, newAttackerHP, attackerCard.Attack);
      target.ActiveCards[0] = new Card(targetCard.ManaCost, newTargetHP, targetCard.Attack);

      return state;
    }
  }
}
