using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  class CardAction : PlayerAction
  {
    public int InHandIndex { get; set; }
    public CardAction(int playerId, Func<GameState, int[], GameState> type, int inHandIndex) : base(type, playerId)
    {
      this.InHandIndex = inHandIndex;
    }
  }
}
