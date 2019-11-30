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
    public void TestAttackWithoutActions()
    {
      var player1Cards = new List<Card>() { new Card(20, 20, 10) };
      var player2Cards = new List<Card>() { new Card(20, 20, 5) };

      GameState state = InitGameState(player1Cards, player2Cards);

      GameState freshState = ActionSet.actions[ActionType.Attack](state);

      Card card1 = freshState.Players[0].ActiveCards[0];
      Card card2 = freshState.Players[1].ActiveCards[0];

      Assert.IsTrue(card1.HP == 15 && card2.HP == 10);
    }

    [Test]
    public void TestAttackWithUseAction()
    {
      CardAction heal = new CardAction(1, ActionType.HealSelf, 0, 200);
      Card cardWithHeal = new Card(20, 20, 10, useAction: heal);

      List<Card> player1Cards = new List<Card>() { cardWithHeal };
      List<Card> player2Cards = new List<Card>() { new Card(20, 20, 5) };

      GameState state = InitGameState(player1Cards, player2Cards);

      GameState freshState = ActionSet.actions[ActionType.Attack](state);

      Card card1 = freshState.Players[0].ActiveCards[0];
      Card card2 = freshState.Players[1].ActiveCards[0];

      Assert.IsTrue(card1.HP == 215 && card2.HP == 10);
    }

    [Test]
    public void TestAttackWithDeath()
    {
      List<Card> player1Cards = new List<Card>() { new Card(20, 20, 30) };
      List<Card> player2Cards = new List<Card>() { new Card(20, 20, 5) };

      GameState state = InitGameState(player1Cards, player2Cards);

      GameState freshState = ActionSet.actions[ActionType.Attack](state);
      freshState = ActionSet.actions[ActionType.Die](state);

      Player player1 = freshState.Players[0];
      Player player2 = freshState.Players[1];

      Assert.IsTrue(player1.ActiveCards.Count == 1 && player2.ActiveCards.Count == 0);
    }

    private GameState InitGameState(List<Card> player1Cards, List<Card> player2Cards)
    {

      Player player1 = new Player(1);
      Player player2 = new Player(2);

      player1.ActiveCards = player1Cards;
      player2.ActiveCards = player2Cards;

      GameState state = new GameState(player1, new Player[] { player1, player2 });

      state.Attacker = 0;
      state.Target = 0;

      return state;
    }
  }
}