using System;


namespace tcg
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      GameState state = InitGameState();

      Console.WriteLine(state.CurrentPlayer.CardInHand.Count);
      Console.WriteLine(state.CurrentPlayer.CardSet.Count);

      ActionSet.actions[ActionType.TakeCard](state);

      Console.WriteLine(state.CurrentPlayer.CardInHand.Count);
      Console.WriteLine(state.CurrentPlayer.CardSet.Count);
    }

    private static GameState InitGameState()
    {
      Player player1 = new Player(1);
      Player player2 = new Player(2);

      CardAction heal = new CardAction(1, ActionType.HealSelf, 0, 200);
      CardAction print = new CardAction(1, ActionType.Print, 0, 0);

      Card cardWithStartPrint = new Card(20, 20, 10, startAction: print);

      player1.CardSet.Add(cardWithStartPrint);

      GameState state = new GameState(player1, new Player[] { player1, player2 });

      state.Attacker = 0;
      state.Target = 0;

      return state;
    }
  }
}
