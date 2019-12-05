using System;
using System.Collections.Generic;

namespace tcg
{
  static class CardSet
  {
    public enum CardName
    {
      FlashHeal,
      IronforgeRifleman,
      VoodooDoctor,
    }

    public static Dictionary<CardName, Func<Card>> Cards = new Dictionary<CardName, Func<Card>>() {
          {CardName.FlashHeal, () => new Card(
              1,
              0,
              0,
              startAction:(SpecifiedAction<int, int>)(
                (GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.Heal(state, playerIndex, cardIndex, 5, remainArgs)
                )
            )
          },
          {CardName.IronforgeRifleman, () => new Card(
              3,
              2,
              2,
              startAction:(SpecifiedAction<int, int>)(
                (GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.DealDamage(state, playerIndex, cardIndex, 1, remainArgs)
                )
            )
          },
          {CardName.VoodooDoctor, () => new Card(
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
  }
}