using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace tcg
{
  static class GameStateVisualizer
  {
    public static string BoardToString(Player currPlayer)
    {
      string toReturn = "";

      Hero currHero = currPlayer.Hero;

      toReturn += string.Format("Player {0} {1} HP\n", currPlayer.Id, currHero.HP);
      //toReturn += string.Format("Player {0} {1} HP\n", currPlayer.name, currHero.HP); //Когда будет готов Player.name

      List<Card> activeCards = currPlayer.ActiveCards;
      for (int j = 0; j < activeCards.Count; j++)
      {
        Card currCard = activeCards[j];

        toReturn += string.Format(currCard.ToString());
      }
      toReturn += "---------------------------------------\n";

      return toReturn;
    }

    public static string HandToString(Player currPlayer)
    {
      string toReturn = "";

      List<Card> cardsInHand = currPlayer.CardsInHand;

      for (int i = 0; i < cardsInHand.Count; i++)
      {
        Card currCard = cardsInHand[i];

        toReturn += currCard.ToString();
      }

      return toReturn;
    }
    public static string GameStateToString(GameState state, int id)
    {
      string toReturn = string.Format("Turn of {0}\n", state.CurrentPlayer.Id);

      Player currPlayer = state.Players[id];
      for (int i = 0; i < state.Players.Length; i++)
      {
        Player player = state.Players[i];

        if (player.Id != id)
          toReturn += BoardToString(player);
      }
      toReturn += BoardToString(currPlayer);
      toReturn += HandToString(currPlayer);

      return toReturn;
    }
  }
}