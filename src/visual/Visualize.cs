namespace tcg
{
  static class Visualize
  {
    private static bool IsYourTurn(GameState state, int relativePlayer)
    {
      return state.CurrentPlayer.Id == relativePlayer;
    }
    public static string TurnStart(GameState state, int relativePlayer)
    {
      string turnStartNotification = IsYourTurn(state, relativePlayer) ? "Your turn!" : "Opponent's turn..";

      string result = string.Format(@"
      {0}
      ",
      turnStartNotification
      );

      return result;
    }

    private static string TotalBoardToString(GameState state, int relativePlayer)
    {
      return "board of two players";
    }

    private static string PlayerSideToString(GameState state, int relativePlayer)
    {
      return "one side of the board";
    }

    private static string CardToString(GameState state, int relativePlayer)
    {
      return "stringifies card (yep, it'll be here, not at Card.cs)";
    }

    private static string HeroToString(GameState state, int relativePlayer)
    {
      return "stringifies hero -> Enemy/You + hp, attack, mana";
    }

    private static string Winning(GameState state, int relativePlayer)
    {
      return "notifies about winning or loosing";
    }
  }
}