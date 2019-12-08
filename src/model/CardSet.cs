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
      Starfire,
      GoldshireFootman
    }

    public static Dictionary<CardName, Func<Card>> Cards = new Dictionary<CardName, Func<Card>>() {
          {CardName.FlashHeal, () => new Card(
              "Flash Heal",
              1,
              0,
              0,
              description: "Heals 2 damage",
              startAction: new List<Delegate>() {(SpecifiedAction<int, int>)(
                (GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.Heal(state, playerIndex, cardIndex, 5, remainArgs)
                )}
            )
          },
          {CardName.IronforgeRifleman, () => new Card(
              "Ironforge Rifleman",
              3,
              2,
              2,
              description: "Battlecry: Deal 1 damage",
              startAction:new List<Delegate>() {(SpecifiedAction<int, int>)(
                (GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.DealDamage(state, playerIndex, cardIndex, 1, remainArgs)
                )}
            )
          },
          {CardName.VoodooDoctor, () => new Card(
              "Voodoo Doctor",
              1,
              1,
              2,
              description: "Battlecry: Heal 2 damage",
              startAction:new List<Delegate>() {(SpecifiedAction<int, int>)(
                (GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.Heal(state, playerIndex, cardIndex, 2, remainArgs)
                )}
            )
          },
          {CardName.Starfire, () => new Card(
            "Star Fire",
              6,
              0,
              0,
              description: "Deal 5 damage. Draw a card",
              startAction:new List<Delegate>() {
                (SpecifiedAction<int, int>)((GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.DealDamage(state, playerIndex, cardIndex, 5, remainArgs)
                ),
                (SpecifiedAction)((GameState state, int[] remainArgs) =>
                  ActionSet.DrawCard(state, remainArgs)
                ),
              }
            )
          },
          {CardName.GoldshireFootman, () => new Card(
            "Goldshire Footman",
              1,
              2,
              1,
              description: "Taunt",
              isTaunt: true
            )
          },
        };
  }
}