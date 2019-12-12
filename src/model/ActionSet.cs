using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  static class ActionSet
  {
    static private (int[], int[]) SplitArgs(int[] args, int actualArgsCount)
    {
      int[] actualArgs = args.Take(actualArgsCount).ToArray();
      int[] remainArgs = args.Skip(actualArgsCount).Take(args.Length - actualArgsCount).ToArray();

      return (actualArgs, remainArgs);
    }

    static private int GetActualArgsCount(Delegate action)
    {
      switch (action)
      {
        case SpecifiedAction a:
          return 0;
        case SpecifiedAction<int> a:
          return 1;
        case SpecifiedAction<int, int> a:
          return 2;
        case SpecifiedAction<int, int, int> a:
          return 3;
        case SpecifiedAction<int, int, int, int> a:
          return 4;
        case SpecifiedAction<int, int, int, int, int> a:
          return 5;
        case SpecifiedAction<int, int, int, int, int, int> a:
          return 6;
        case SpecifiedAction<int, int, int, int, int, int, int> a:
          return 7;
        case SpecifiedAction<int, int, int, int, int, int, int, int> a:
          return 8;
        case SpecifiedAction<int, int, int, int, int, int, int, int, int> a:
          return 9;
        default:

          throw new ArgumentException("Cannot find a type of the action");
      }
    }

    public static Action<GameState> PackAction(GameState state, Delegate action, int[] args = null)
    {
      if (args == null)
        args = new int[0];

      int[] actualArgs, remainArgs;
      int actualArgsCount = GetActualArgsCount(action);
      switch (actualArgsCount)
      {
        case 0:
          (actualArgs, remainArgs) = ActionSet.SplitArgs(args, 0);
          return state => ((SpecifiedAction)action)(state, remainArgs);
        case 1:
          (actualArgs, remainArgs) = ActionSet.SplitArgs(args, 1);
          return state => ((SpecifiedAction<int>)action)(state, args[0], remainArgs);
        case 2:
          (actualArgs, remainArgs) = ActionSet.SplitArgs(args, 2);
          return state => ((SpecifiedAction<int, int>)action)(state, args[0], args[1], remainArgs);
        case 3:
          (actualArgs, remainArgs) = ActionSet.SplitArgs(args, 3);
          return state => ((SpecifiedAction<int, int, int>)action)(state, args[0], args[1], args[2], remainArgs);
        case 4:
          (actualArgs, remainArgs) = ActionSet.SplitArgs(args, 4);
          return state => ((SpecifiedAction<int, int, int, int>)action)(state, args[0], args[1], args[2], args[3], remainArgs);
        // case 5:
        //   return state => ((SpecifiedAction<int, int, int, int, int>)action)(state, args[0], args[1], args[2], args[3], args[4], remainArgs);
        // case 6:
        //   return state => ((SpecifiedAction<int, int, int, int, int, int>)action)(state, args[0], args[1], args[2], args[3], args[4], args[5], remainArgs);
        // case 7:
        //   return state => ((SpecifiedAction<int, int, int, int, int, int, int>)action)(state, args[0], args[1], args[2], args[3], args[4], args[5], args[6], remainArgs);
        // case 8:
        //   return state => ((SpecifiedAction<int, int, int, int, int, int, int, int>)action)(state, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], remainArgs);
        // case 9:
        //   return state => ((SpecifiedAction<int, int, int, int, int, int, int, int, int>)action)(state, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], remainArgs);

        default:
          throw new ArgumentException("Invalid number of args");
      }
    }

    public static Action<GameState> PackAction(GameState state, ActionType type, int[] args = null)
    {
      return PackAction(state, Actions[type], args);
    }

    public static void PackActionAndExecute(GameState state, ActionType type, int[] args = null)
    {
      PackAction(state, Actions[type], args)(state);
    }

    public static void PackActionAndExecute(GameState state, Delegate action, int[] args = null)
    {
      PackAction(state, action, args)(state);
    }

    public static SpecifiedAction<int, int> Attack = (GameState state, int attackerCardIndex, int targetCardIndex, int[] remainArguments) =>
    {
      var attackerPlayer = state.CurrentPlayer;
      var targetPlayer = state.Players[0].Id != attackerPlayer.Id ? state.Players[0] : state.Players[1];

      var attackerCard = attackerPlayer.ActiveCards[attackerCardIndex];
      var targetCard = targetPlayer.ActiveCards[targetCardIndex];

      if (attackerCard.IsSleeping)
      {
        throw new ArgumentException("This creature is sleeping");
      }

      if (!targetCard.IsTaunt)
        foreach (Card c in targetPlayer.ActiveCards)
          if (c.IsTaunt)
            throw new ArgumentException("You cannot attack this creature because of this player has a taunt");

      attackerCard.HP -= targetCard.Attack;
      targetCard.HP -= attackerCard.Attack;

      return state;
    };

    public static SpecifiedAction<int, int, int> Heal = (state, playerIndex, cardIndex, healAmount, remainArguments) =>
    {
      Card card = state.Players[playerIndex].ActiveCards[cardIndex];
      card.HP = Math.Min(card.HP + healAmount, card.MaxHP);

      return state;
    };

    public static SpecifiedAction<int, int, int> DealDamage = (state, playerIndex, cardIndex, damageAmount, remainArguments) =>
    {
      Card card = state.Players[playerIndex].ActiveCards[cardIndex];
      card.HP = card.HP - damageAmount;

      return state;
    };

    public static SpecifiedAction DrawCard = (state, remainArguments) =>
    {
      var cardToDraw = state.CurrentPlayer.CardSet[0];
      state.CurrentPlayer.CardSet.RemoveAt(0);
      state.CurrentPlayer.CardsInHand.Add(cardToDraw);

      return state;
    };

    public static SpecifiedAction<int> PlayCard = (state, cardIndex, remainArguments) =>
    {
      var cardToPlay = state.CurrentPlayer.CardsInHand[cardIndex];
      if (cardToPlay.ManaCost > state.CurrentPlayer.Hero.Mana)
      {
        throw new ArgumentException("You don't have enough mana");
      }
      state.CurrentPlayer.CardsInHand.RemoveAt(cardIndex);
      state.CurrentPlayer.ActiveCards.Add(cardToPlay);

      state.CurrentPlayer.Hero.Mana -= cardToPlay.ManaCost;

      foreach (Delegate action in cardToPlay.OnStartAction)
        PackActionAndExecute(state, action, remainArguments);

      return state;
    };

    public static SpecifiedAction ProcessDeath = (state, _) =>
    {
      foreach (Player p in state.Players)
      {
        List<Card> cardsToRemove = new List<Card>();
        p.ActiveCards.ForEach(card => { if (card.HP <= 0) cardsToRemove.Add(card); });

        foreach (Card c in cardsToRemove)
          p.ActiveCards.Remove(c);
      }

      return state;
    };

    public static SpecifiedAction EndTurn = (state, _) =>
    {
      // only for 2 players
      var notCurrentPlayer = state.Players[0].Id != state.CurrentPlayer.Id ? state.Players[0] : state.Players[1];
      state.CurrentPlayer = notCurrentPlayer;

      var wakeUpAction = ActionSet.PackAction(state, ActionType.WakeUpCreatures);
      wakeUpAction(state);

      return state;
    };

    public static SpecifiedAction WakeUpAllCreatures = (state, _) =>
    {
      foreach (var player in state.Players)
      {
        player.ActiveCards.ForEach(card => card.IsSleeping = false);
      }

      return state;
    };

    public static SpecifiedAction<int, int, int, int> PerformActionOnRandomCard = (state, onSelf, action, actionAmount, power, remainArguments) =>
    {
      var player = onSelf == 0 ?
        (state.Players[0].Id != state.CurrentPlayer.Id ? state.Players[0] : state.Players[1]) :
        state.CurrentPlayer;

      return PerformActionOnCardWithPlayer(state, player, action, actionAmount, power);
    };

    private static GameState PerformActionOnCardWithPlayer(GameState state, Player player, int action, int targetAmount, int power)
    {
      Random rnd = new Random();
      if (player.ActiveCards.Count < targetAmount)
      {
        throw new ArgumentException("Not enough cards on table for this action");
      }

      var cardIndexes = Enumerable.Range(0, player.ActiveCards.Count).ToList();
      for (var i = 0; i < targetAmount; i++)
      {
        var randomIndex = rnd.Next(cardIndexes.Count);
        var randomCard = cardIndexes[randomIndex];
        cardIndexes.RemoveAt(randomIndex);
        var packedAction = ActionSet.PackAction(state, Actions[(ActionType)action], new int[3] { player.Id, randomCard, power });
        packedAction(state);
      }
      return state;
    }

    public static SpecifiedAction<int> Summon = (state, creatureName, remainArguments) =>
    {
      var creature = CardSet.Cards[(CardSet.CardName)creatureName];
      state.CurrentPlayer.ActiveCards.Add(creature());

      return state;
    };

    public static SpecifiedAction<int, int, int, int> BuffCreature = (state, playerId, cardIndex, attackBuff, hpBuff, remainArguments) =>
    {
      var player = state.Players[playerId];
      var creature = player.ActiveCards[cardIndex];

      creature.Attack += attackBuff;
      creature.HP += hpBuff;

      return state;
    };

    public static SpecifiedAction SaveChanges = (state, _) =>
    {
      for (int i=0; i<state.Players.Length; i++){
        Player currrentPlayer = state.Players[i];

        for (int j=0; j<currrentPlayer.ActiveCards.Count; j++){
          Card currentCard = currrentPlayer.ActiveCards[j];

          currentCard.previous = currentCard.Clone();
        }
      }

      return state;
    };
    public static Dictionary<ActionType, Delegate> Actions = new Dictionary<ActionType, Delegate>() {
        {ActionType.Attack, Attack},
        {ActionType.Heal, Heal},
        {ActionType.DrawCard, DrawCard},
        {ActionType.PlayCard, PlayCard},
        {ActionType.ProcessDeath, ProcessDeath},
        {ActionType.DealDamage, DealDamage},
        {ActionType.EndTurn, EndTurn},
        {ActionType.WakeUpCreatures, WakeUpAllCreatures},
        {ActionType.PerformActionOnRandomCard, PerformActionOnRandomCard},
        {ActionType.BuffCreature, BuffCreature},
        {ActionType.SaveChanges, SaveChanges}
      };
  }
}
