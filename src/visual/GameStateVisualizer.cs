using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace tcg
{
  static class GameStateVisualizer
  {
    public static string OutputBoard(Player currPlayer){
        string toReturn = "";
        
        Hero currHero = currPlayer.Hero;

        toReturn += string.Format("Player {0} {1} HP\n", currPlayer.Id, currHero.HP);
        //toReturn += string.Format("Player {0} {1} HP\n", currPlayer.name, currHero.HP); //Когда будет готов Player.name

        List<Card> activeCards = currPlayer.ActiveCards;
        for (int j=0; j<activeCards.Count; j++){
          Card currCard = activeCards[j];

          toReturn += string.Format(currCard.ToString());
        }
        toReturn += "---------------------------------------\n";

        return toReturn;
    }

    public static string OutputHand(Player currPlayer){
      string toReturn = "";

      List<Card> cardsInHand = currPlayer.CardsInHand;

      for (int i =0; i< cardsInHand.Count; i++){
        Card currCard = cardsInHand[i];

          toReturn += currCard.ToString();
      }

      return toReturn;
    }
    public static string Visualize(GameState state)
    {
      string toReturn = "";

      for (int i = 0; i<state.Players.Length; i++){
        Player currPlayer = state.Players[i];

        if(currPlayer != state.CurrentPlayer)
          toReturn += OutputBoard(currPlayer);
      }
      toReturn += OutputBoard(state.CurrentPlayer);
      toReturn += OutputHand(state.CurrentPlayer);

      return toReturn;
    }
  }
}