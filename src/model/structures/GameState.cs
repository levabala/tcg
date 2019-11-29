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
    public int Target { get; set; }
    public int Attacker { get; set; }

    public GameState(Player currPlayer, Player[] players)
    {
      IsFinished = false;
      CurrentPlayer = currPlayer;
      Players = players;
      Target = -2; // -1 is Hero, so -2
      Attacker = -2;
    }
  }
}