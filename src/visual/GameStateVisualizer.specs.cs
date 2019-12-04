using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace tcg
{
  [TestFixture]
  static class GameStateVisualizerFixture
  {
    [Test]
    public static void VisualizeTest()
    {     
      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard(), Card.DimonCard() }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard(), Card.DimonCard() }
          ) ,
        }
      );
      string toReturn = "";

      for (int i = 0; i<state.Players.Length; i++){
        Player currPlayer = state.Players[i];
        Hero currHero = state.Players[i].Hero;

        if (currPlayer.Id != state.CurrentPlayer.Id){
          toReturn += string.Format("Player {0} {1} HP\n", currPlayer.Id, currHero.HP);
        //toReturn += string.Format("Player {0} {1} HP\n", currPlayer.name, currHero.HP); //Когда будет готов Player.name

        List<Card> currCardList = new List<Card>();
        for (int j=0; j<currCardList.Count; j++){
          Card currCard = currCardList[j];

          toReturn += string.Format("{0} {1} HP {2} ATK\n", currCard.ToString(), currCard.HP, currCard.Attack);
          //toReturn += string.Format("{0} {1} HP {2} ATK\n", currCard.name, currCard.HP, currCard.Attack); // Когда будет Card.name 
        }
        toReturn += "---------------------------------------";
        }
      }

      Assert.AreNotEqual(toReturn, "");
      //return toReturn;
    }
  }
}