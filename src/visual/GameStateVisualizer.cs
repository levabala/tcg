using System;
using System.Linq;
using NUnit.Framework;
using Pastel;
using System.Drawing;
using System.Collections.Generic;

namespace tcg
{
  static class GameStateVisualizer
  {
    public static string BoardToString(Player currPlayer, bool enemy = true)
    {
      string toReturn = "";

      Hero currHero = currPlayer.Hero;

      if (enemy)
        toReturn += string.Format("-1) Enemy {0} HP\n\n",currHero.HP).Pastel(Color.Violet);
      else
        toReturn += string.Format("You {0} HP\n\n", currHero.HP);

      List<Card> activeCards = currPlayer.ActiveCards;
      for (int j = 0; j < activeCards.Count; j++)
      {
        Card currCard = activeCards[j];

        toReturn += string.Format("{0}) ", j) + currCard.ToString();
      }
      toReturn += "---------------------------------------------------------------\n";

      return toReturn;
    }

    public static string HandToString(Player currPlayer)
    {
      string toReturn = "";

      List<Card> cardsInHand = currPlayer.CardsInHand;

      for (int i = 0; i < cardsInHand.Count; i++)
      {
        Card currCard = cardsInHand[i];

        toReturn += string.Format("{0}) ", i) + currCard.ToString();
      }

      return toReturn;
    }
    public static string GameStateToString(GameState state, int id)
    {
      string toReturn = "";

      Player currPlayer = state.Players[id];
      for (int i = 0; i < state.Players.Length; i++)
      {
        Player player = state.Players[i];

        if (player.Id != id)
          toReturn += BoardToString(player);
      }
      if (id == state.CurrentPlayer.Id)
        toReturn += "Your turn!\n".Pastel(Color.Lime);
      else
        toReturn += "Enemy's turn!\n".Pastel(Color.Red);
      toReturn += BoardToString(currPlayer, enemy : false);
      toReturn += HandToString(currPlayer);

      return toReturn;
    }
  }
}