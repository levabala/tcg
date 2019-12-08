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

      var drawCardAction = ActionSet.PackAction(state, ActionType.DrawCard);
      drawCardAction(state);

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
            new List<Card> { }
          ) ,
        }
      );

      var playCardAction = ActionSet.PackAction(state, ActionType.PlayCard, new int[] { 0 });
      playCardAction(state);

      GameState stateExpected = new GameState(
       new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard(), Card.DimonCard() },
            ((Func<Hero>)(() => {
              var hero = Hero.CommonHero();
              hero.Mana -= Card.DimonCard().ManaCost;
              return hero;
            }))()
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { }
          ),
       }
     );

      Assert.AreEqual(state, stateExpected);
    }

    [Test]
    public void PlayCardWithNotEnoughManaTest1()
    {
      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { Card.LevCard() },
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

      var drawCardAction = ActionSet.PackAction(state, ActionType.PlayCard, new int[] { 0 });
      Assert.Catch(() => drawCardAction(state), "You don't have enough mana");
    }

    [Test]
    public void PlayCardWithNotEnoughManaTest2()
    {
      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { Card.LevBudgetCard(), Card.LevBudgetCard() },
            new List<Card> { }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { }
          ) ,
        }
      );

      var drawCardAction = ActionSet.PackAction(state, ActionType.PlayCard, new int[] { 0 });
      drawCardAction(state);
      Assert.Catch(() => drawCardAction(state), "You don't have enough mana");
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
            new List<Card> { Card.LevCard()}
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard()}
          ) ,
        }
      );

      var endTurnAction = ActionSet.PackAction(state, ActionType.EndTurn);
      endTurnAction(state);
      endTurnAction(state);

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
              c.IsSleeping = false;
              return c;
            }))()}
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
    public void TauntTest1()
    {
      Func<Card> testingCardGenerator = CardSet.Cards[CardSet.CardName.GoldshireFootman];

      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> {  },
            new List<Card> { Card.DimonCard() }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> {Card.LevBudgetCard(), testingCardGenerator() }
          ) ,
        }
      );

      var attackCardAction = ActionSet.PackAction(state, ActionType.Attack, new int[] { 0, 0 });
      Assert.Catch(() => attackCardAction(state), "You cannot attack this creature because of this player has a taunt");
    }

    [Test]
    public void TauntTest2()
    {
      Func<Card> testingCardGenerator = CardSet.Cards[CardSet.CardName.GoldshireFootman];

      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> {  },
            new List<Card> { Card.DimonCard() }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> {Card.LevBudgetCard(), testingCardGenerator() }
          ) ,
        }
      );

      var attackCardAction = ActionSet.PackAction(state, ActionType.Attack, new int[] { 0, 1 });
      attackCardAction(state);

      GameState stateExpected = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> {  },
            new List<Card> { ((Func<Card>)(() => {
              var card = Card.DimonCard();
              // Init StrongDimon damaged for 6 points
              card.HP -= testingCardGenerator().Attack;
              return card;
            }))() }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> {Card.LevBudgetCard(), ((Func<Card>)(() => {
              var card = testingCardGenerator();
              // Init StrongDimon damaged for 6 points
              card.HP -= Card.DimonCard().Attack;
              return card;
            }))() }
          ) ,
        }
      );

      Assert.AreEqual(state, stateExpected);
    }

    [Test]
    public void RandomActionTest1()
    {
      Func<Card> testingCardGenerator = CardSet.Cards[CardSet.CardName.MultiShot];

      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { testingCardGenerator() },
            new List<Card> { }
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
      host.ProcessInput(0, "play 0");

      var player = state.Players[0].Id != state.CurrentPlayer.Id ? state.Players[0] : state.Players[1];
      Assert.AreEqual(player.ActiveCards.Count, 1);
    }

    [Test]
    public void RandomActionTest2()
    {
      Func<Card> testingCardGenerator = CardSet.Cards[CardSet.CardName.MultiShot];

      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { testingCardGenerator() },
            new List<Card> { }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard() }
          ) ,
        }
      );

      var playCardAction = ActionSet.PackAction(state, ActionType.PlayCard, new int[] { 0 });
      Assert.Catch(() => playCardAction(state), "Not enough cards on table for this action");

    }
  }
}