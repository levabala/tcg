using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  class ActionSet
  {
    private static Player attacker;
    private static Player target;

    private static Card attackerCard;
    private static Card targetCard;

    public static Dictionary<ActionType, Func<GameState, GameState>> actions = new Dictionary<ActionType, Func<GameState, GameState>>
        {
            {ActionType.Attack, Attack },
            {ActionType.HealSelf, HealSelf},
            {ActionType.Die, Die},
            {ActionType.Print, Print},
            {ActionType.TakeCard, TakeCard}
        };

    private static GameState Attack(GameState state)
    {
      GetPlayersAndCards(state);

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
      GetPlayersAndCards(state);

      attackerCard.HP += attackerCard.UseAction.Power;

      return state;
    }

    private static GameState Die(GameState state)
    {
      GetPlayersAndCards(state);
      CheckAndPerformDeath(state);
      return state;
    }

    private static void CheckAndPerformDeath(GameState state)
    {
      foreach (var player in state.Players)
      {
        for (int i = 0; i < player.ActiveCards.Count; i++)
        {
          Card card = player.ActiveCards[i];
          if (card.HP <= 0)
          {
            if (card.DieAction != null)
            {
              ActionType type = card.DieAction.Type;
              state = actions[type](state);
            }
            player.ActiveCards.RemoveAt(i);
          }
        }
      }
    }

    private static GameState TakeCard(GameState state)
    {
      GetPlayersAndCards(state);
      if (attacker.CardInHand.Count < Player.MaxNumberCardInHand && attacker.CardSet.Count > 0)
      {
        var inHand = attacker.CardInHand;
        var inSet = attacker.CardSet;
        inHand.Add(inSet[inSet.Count - 1]);
        inSet.RemoveAt(inSet.Count - 1);

        // var playedCard = inHand[inHand.Count - 1];
        // if (inHand[inHand.Count - 1].StartAction != null)
        // {
        //   ActionType type = playedCard.StartAction.Type;
        //   state = actions[type](state);
        // }
      }
      return state;
    }

    private static GameState Print(GameState state)
    {
      Console.WriteLine("Hi");
      return state;
    }


    private static void GetPlayersAndCards(GameState state)
    {
      attacker = state.CurrentPlayer;
      target = state.Players[0].Id != attacker.Id ? state.Players[0] : state.Players[1];

      if (attacker.ActiveCards.Count > 0 && state.Attacker >= 0)
        attackerCard = attacker.ActiveCards[state.Attacker];
      if (target.ActiveCards.Count > 0 && state.Target >= 0)
        targetCard = target.ActiveCards[state.Target];
    }
  }
}
