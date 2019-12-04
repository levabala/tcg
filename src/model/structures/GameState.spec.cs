using NUnit.Framework;
using tcg;
using System;
using System.Collections.Generic;

namespace tcgTests
{
  [TestFixture]
  public class GameStateFixture
  {
    [Test]
    public void EqualityTest()
    {
      GameState gm1 = new GameState(
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
      GameState gm2 = new GameState(
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
      GameState gm3 = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard() }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard(), Card.DimonCard() }
          ) ,
        }
      );
      GameState gm4 = new GameState(
              new Player[] {
          new Player(
            3,
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

      GameState gm5 = new GameState(
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
          new Player(
            20,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard(), Card.DimonCard() }
          )
              }
            );

      Assert.AreEqual(gm1, gm2);
      Assert.AreNotEqual(gm1, gm3);
      Assert.AreNotEqual(gm1, gm4);
      Assert.AreNotEqual(gm1, gm5);
    }
  }
}