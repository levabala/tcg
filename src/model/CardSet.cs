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
              (SpecifiedAction<int, int>)(
                (GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.Heal(state, playerIndex, cardIndex, 5, remainArgs)
                )
            )
          }
        };

    // TODO: implement more cards
  }
}