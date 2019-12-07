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
      GoldshireFootman,
      MultiShot,
      StonetuskBoar,
      MurlocScout,
      MurlocTidehunter,
      ShatteredSunCleric,
    }

    public static Dictionary<CardName, Func<Card>> Cards = new Dictionary<CardName, Func<Card>>() {
          {CardName.FlashHeal, () => new Card(
              1,
              0,
              0,
              startAction: new List<Delegate>() {(SpecifiedAction<int, int>)(
                (GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.Heal(state, playerIndex, cardIndex, 5, remainArgs)
                )}
            )
          },
          {CardName.IronforgeRifleman, () => new Card(
              3,
              2,
              2,
              startAction:new List<Delegate>() {(SpecifiedAction<int, int>)(
                (GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.DealDamage(state, playerIndex, cardIndex, 1, remainArgs)
                )}
            )
          },
          {CardName.VoodooDoctor, () => new Card(
              1,
              1,
              2,
              startAction:new List<Delegate>() {(SpecifiedAction<int, int>)(
                (GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.Heal(state, playerIndex, cardIndex, 2, remainArgs)
                )}
            )
          },
          {CardName.Starfire, () => new Card(
              6,
              0,
              0,
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
              1,
              2,
              1,
              isTaunt: true
            )
          },
          {CardName.MultiShot, () => new Card(
            4,
            0,
            0,
            startAction:new List<Delegate>() {
              (SpecifiedAction)((GameState state,  int[] remainArgs) =>
                ActionSet.PerformRandomAction(state, 0, (int)ActionType.DealDamage, 2, 3, remainArgs)
              )}
            )
          },
          {CardName.StonetuskBoar, () => new Card(
            1,
            1,
            1,
            isSleeping: false)
          },
          {CardName.MurlocScout, () => new Card(
            1,
            1,
            1)
          },
          {CardName.MurlocTidehunter, () => new Card(
            1,
            1,
            2,
            startAction:new List<Delegate>() {
              (SpecifiedAction)((GameState state,  int[] remainArgs) =>
                ActionSet.Summon(state, (int)CardSet.CardName.MurlocScout, remainArgs)
              )}
            )
          },
          {CardName.ShatteredSunCleric, () => new Card(
            3,
            2,
            3,
             startAction:new List<Delegate>() {
              (SpecifiedAction<int, int>)((GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.BuffCreature(state, playerIndex, cardIndex, 1, 1, remainArgs)
              )}
            )
          },
    };
  }
}