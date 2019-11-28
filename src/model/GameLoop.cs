using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
    class GameLoop
    {
        public PlayerAction GenerateAction(int playerId, ActionType action)
        {
            return new PlayerAction(playerId, action);
        }
    }
}
