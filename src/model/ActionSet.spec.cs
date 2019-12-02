using NUnit.Framework;
using tcg;
using System;
using System.Collections.Generic;

namespace tcgTests
{
  [TestFixture]
  public class ActionSetTestsFixture
  {

    [Test]
    public void TakeCardTest()
    {
      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { Card.DimonCard() },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard() }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { }
          ) ,
        }
      );

      var takeCardAction = ActionSet.PackAction(state, ActionType.TakeCard);
      takeCardAction(state);

      GameState stateExpected = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { Card.DimonCard()},
            new List<Card> { Card.DimonCard(), Card.DimonCard() }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { }
          ) ,
        }
      );

      Assert.AreEqual(state, stateExpected);
    }

    public void DrawCardTest()
    {
      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { Card.DimonCard() },
            new List<Card> { Card.DimonCard(), Card.DimonCard() }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> {  }
          ) ,
        }
      );

      var drawCardAction = ActionSet.PackAction(state, ActionType.DrawCard, new int[] { 0 });
      drawCardAction(state);

      GameState stateExpected = new GameState(
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
            new List<Card> { }
          ) ,
        }
      );

      Assert.AreEqual(state, stateExpected);
    }
  }
}