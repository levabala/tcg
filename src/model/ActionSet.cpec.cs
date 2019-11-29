using NUnit.Framework;
using tcg;
using System;

namespace tcgTests
{
  [TestFixture]
  public class ActionSetTestsFixture
  {
    private GameState InitGameState()
    {
      Player player1 = new Player(1);
      Player player2 = new Player(2);

      player1.ActiveCards[0] = new Card(20, 20, 10);
      player2.ActiveCards[0] = new Card(10, 20, 5);

      GameState state = new GameState(player1, new Player[] { player1, player2 });

      state.Attacker = 0;
      state.Target = 0;

      return state;
    }

    [Test]
    public void TestAttackWithoutActions()
    {
      GameState state = InitGameState();

      GameState freshState = ActionSet.actions[ActionType.Attack](state);

      Card card1 = freshState.Players[0].ActiveCards[0];
      Card card2 = freshState.Players[1].ActiveCards[0];

      Assert.IsTrue(card1.HP == 15 && card2.HP == 10);
    }
  }

}