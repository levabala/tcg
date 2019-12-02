using NUnit.Framework;
using tcg;
using System;
using System.Collections.Generic;

namespace tcgTests
{
  [TestFixture]
  public class CardSetTestsFixture
  {
    [Test]
    public void FlashHealTest()
    {
      Func<Card> testingCardGenerator = CardSet.Cards["Flash Heal"];

      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { testingCardGenerator() },
            new List<Card> { Card.DimonCard(),((Func<Card>)(() => {
              var card = Card.DimonStrongCard();
              // Init StrongDimon damaged for 6 points
              card.HP -= 6;
              return card;
            }))() }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard(), Card.DimonCard() }
          ) ,
        }
      );

      Host host = new Host(new MiddlewareLocal(), new List<Middleware> { });
      host.state = state;
      host.ProcessInput(0, "draw 0 0 1");

      GameState stateExpected = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), ((Func<Card>)(() => {
              var card = Card.DimonStrongCard();
              card.HP -= 6;
              // Flash Heal must heal selected card with 5 points
              card.HP += 5;
              return card;
            }))() }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard(), Card.DimonCard() }
          ) ,
        }
      );

      Assert.AreEqual(host.state, stateExpected);
    }
  }
}