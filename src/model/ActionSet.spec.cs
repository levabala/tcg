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
    public void DrawCardTest()
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

      var takeCardAction = ActionSet.PackAction(state, ActionType.DrawCard);
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

    [Test]
    public void PlayCardTest()
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

      var drawCardAction = ActionSet.PackAction(state, ActionType.PlayCard, new int[] { 0 });
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

    [Test]
    public void DeathCardTest()
    {
      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.LevCard(), Card.DimonCard(), Card.DimonCard() }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard(), Card.DimonCard() }
          ) ,
        }
      );

      var attackCardAction = ActionSet.PackAction(state, ActionType.Attack, new int[] { 0, 0 });
      attackCardAction(state);

      var processDeathAction = ActionSet.PackAction(state, ActionType.ProcessDeath);
      processDeathAction(state);

      GameState stateExpected = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { ((Func<Card>)(() => {
              var c = Card.LevCard();
              c.HP -= Card.DimonCard().Attack;
              return c;
            }))() , Card.DimonCard(), Card.DimonCard() }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard()}
          ) ,
        }
      );

      Assert.AreEqual(state, stateExpected);
    }
  }
}