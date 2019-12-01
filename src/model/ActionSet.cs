using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  static class ActionSet
  {
    public static Func<GameState, GameState> PackAction(GameState state, ActionType type, int[] args)
    {
      switch (args.Length)
      {
        case 0:
          return state => ((SpecifiedAction)actions[type])(state);
        case 1:
          return state => ((SpecifiedAction<int>)actions[type])(state, args[0]);
        case 2:
          return state => ((SpecifiedAction<int, int>)actions[type])(state, args[0], args[1]);
        case 3:
          return state => ((SpecifiedAction<int, int, int>)actions[type])(state, args[0], args[1], args[2]);
        default:
          throw new ArgumentException("Invalid number of args");
      }
    }

    public static SpecifiedAction<int, int> Attack = (GameState state, int attackerCardIndex, int targetCardIndex) =>
    {
      var attacker = state.CurrentPlayer;
      var target = state.Players[0].Id != attacker.Id ? state.Players[0] : state.Players[1];

      var attackerCard = attacker.ActiveCards[attackerCardIndex];
      var targetCard = target.ActiveCards[targetCardIndex];

      attackerCard.HP -= targetCard.Attack;
      targetCard.HP -= attackerCard.Attack;

      return state;
    };

    public static SpecifiedAction<int, int, int> Heal = (state, playerIndex, cardIndex, healAmount) =>
    {
      Card card = state.Players[playerIndex].CardInHand[cardIndex];
      card.HP = Math.Min(card.HP + healAmount, card.MaxHP);

      return state;
    };

    static Dictionary<ActionType, Delegate> actions = new Dictionary<ActionType, Delegate>() {
        {ActionType.Attack, Attack},
        {ActionType.Heal, Heal},
      };
  }
}
