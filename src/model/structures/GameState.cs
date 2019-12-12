using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  class GameState
  {
    public bool IsFinished { get; set; }
    public Player CurrentPlayer { get; set; }
    public Player[] Players { get; set; }
    public Dictionary<Card, Card> previousCardState { get; set; }
    public GameState(Player currentPlayer, Player[] players)
    {
      IsFinished = false;
      CurrentPlayer = currentPlayer;
      Players = players;
    }

    public GameState(Player[] players) : this(players[0], players)
    {

    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;

      GameState gameState = (GameState)obj;
      return gameState.IsFinished == this.IsFinished && gameState.CurrentPlayer.Equals(this.CurrentPlayer) && Enumerable.SequenceEqual(gameState.Players, this.Players);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
