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
    public int Power { get; set; }

    public CardAction(int playerId, ActionType type, int inHandIndex, int power) : base(playerId, type)
    {
      this.InHandIndex = inHandIndex;
      Power = power;
    }
  }
}
