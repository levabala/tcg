using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  struct PlayerAction
  {
    public int PlayerId { get; set; }
    public ActionType Type { get; set; }

    public PlayerAction(int playerId, ActionType type)
    {
      this.PlayerId = playerId;
      this.Type = type;
    }
  }
}
