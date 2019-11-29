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
    public CardAction(int playerId, ActionType type, int inHandIndex) : base(playerId, type)
    {
      this.InHandIndex = inHandIndex;
    }
  }
}
