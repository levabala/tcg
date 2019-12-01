using System;
using System.Collections.Generic;

namespace tcg
{
  static class CardSet
  {
    static Dictionary<string, AlmostAbstractCard> cards = new Dictionary<string, AlmostAbstractCard>() {
          {"Flash Heal", new AlmostAbstractCard(1, 0, 0, (state, args) => {
            // args: [playerIndexToHeal, cardIndexToHeal]
            int playerIndexToHeal = (int)args[0];
            int cardIndexToHeal = (int)args[1];

            Card card = state.Players[playerIndexToHeal].CardInHand[cardIndexToHeal];

            return state;
          })}
        };
  }
}