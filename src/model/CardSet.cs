using System;
using System.Collections.Generic;

namespace tcg
{
  static class CardSet
  {
    public static Dictionary<string, Func<Card>> Cards = new Dictionary<string, Func<Card>>() {
          {"Flash Heal", () => new Card(
              1,
              0,
              0,
              startAction:(SpecifiedAction<int, int>)(
                (GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.Heal(state, playerIndex, cardIndex, 5, remainArgs)
                )
            )
          },
          {"Ironforge Rifleman", () => new Card(
              3,
              2,
              2,
              startAction:(SpecifiedAction<int, int>)(
                (GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.DealDamage(state, playerIndex, cardIndex, 1, remainArgs)
                )
            )
          },
          {"Voodoo Doctor", () => new Card(
              1,
              1,
              2,
              startAction:(SpecifiedAction<int, int>)(
                (GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.Heal(state, playerIndex, cardIndex, 2, remainArgs)
                )
            )
          },
        };

    // TODO: implement more cards 
  }
}