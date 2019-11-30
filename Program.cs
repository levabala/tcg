using System;

namespace tcg
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      GameState state = InitGameState();

      GameState freshState = ActionSet.actions[ActionType.Attack](state);

      Card card1 = freshState.Players[0].ActiveCards[0];
      Card card2 = freshState.Players[1].ActiveCards[0];

      Console.WriteLine(card1);
      Console.WriteLine(card2);
      Console.WriteLine(freshState.Players[1].ActiveCards[0]);

      freshState = ActionSet.actions[ActionType.Die](state);

      Console.WriteLine(freshState.Players[0].ActiveCards.Length);
      Console.WriteLine(freshState.Players[1].ActiveCards[0]);
    }

    private static GameState InitGameState()
    {
      Player player1 = new Player(1);
      Player player2 = new Player(2);

      CardAction heal = new CardAction(1, ActionType.HealSelf, 0, 200);
      CardAction print = new CardAction(1, ActionType.Print, 0, 0);
      player1.ActiveCards[0] = new Card(20, 20, 20, useAction: heal);
      player2.ActiveCards[0] = new Card(10, 20, 5, dieAction: print);

      GameState state = new GameState(player1, new Player[] { player1, player2 });

      state.Attacker = 0;
      state.Target = 0;

      return state;
    }
  }
}
