using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace tcg
{
  [TestFixture]
  static class GameStateVisualizerFixture
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

    [Test]
    public static void VisualizeTest()
    {     
      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { CardSet.Cards[CardSet.CardName.VoodooDoctor]() }, //in hand
            new List<Card> { CardSet.Cards[CardSet.CardName.IronforgeRifleman]() } // active
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { CardSet.Cards[CardSet.CardName.Starfire]() },
            new List<Card> {  }
          ) ,
        }
      );

      string toReturn = "";
      int id = 0;

      Player currPlayer = state.Players[id];
      for (int i = 0; i<state.Players.Length; i++){
        Player player = state.Players[i];

        if(player.Id != id)
          toReturn += OutputBoard(player);
      }
      toReturn += OutputBoard(currPlayer);
      toReturn += OutputHand(currPlayer);

      string ExpectedString = "Player 1 10 HP\n" + 
      "---------------------------------------\n" +
      "Player 0 10 HP\n" +
      "Ironforge Rifleman Mana cost: 3, HP: 2, Attack: 2   Battlecry: Deal 1 damage\n"+
      "---------------------------------------\n" +
      "Voodoo Doctor Mana cost: 1, HP: 1, Attack: 2   Battlecry: Heal 2 damage\n";

      Assert.AreEqual(toReturn, ExpectedString);
      //return toReturn;
      //String.Format("{0} Mana cost: {1}, HP: {2}, Attack: {3}   {4}\n", Name, ManaCost, HP, Attack, Description);
    }
  }
}