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
      FrostwolfWarlord,
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
          {CardName.MultiShot, () => new Card(
            "MultiShot",
            4,
            0,
            0,
            startAction:new List<Delegate>() {
              (SpecifiedAction)((GameState state,  int[] remainArgs) =>
                ActionSet.PerformActionOnRandomCard(state, 0, (int)ActionType.DealDamage, 2, 3, remainArgs)
              )}
            )
          },
          {CardName.StonetuskBoar, () => new Card(
            "StonetuskBoar",
            1,
            1,
            1,
            isSleeping: false)
          },
          {CardName.MurlocScout, () => new Card(
            "MurlocScout",
            1,
            1,
            1)
          },
          {CardName.MurlocTidehunter, () => new Card(
            "MurlocTidehunter",
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
            "ShatteredSunCleric",
            3,
            2,
            3,
             startAction:new List<Delegate>() {
              (SpecifiedAction<int, int>)((GameState state, int playerIndex, int cardIndex, int[] remainArgs) =>
                  ActionSet.BuffCreature(state, playerIndex, cardIndex, 1, 1, remainArgs)
              )}
            )
          },
          {CardName.FrostwolfWarlord, () => new Card(
            "FrostwolfWarlord",
            5,
            4,
            4,
             startAction:new List<Delegate>() {
              (SpecifiedAction)((GameState state, int[] remainArgs) =>{
                var player = state.CurrentPlayer;
                return ActionSet.BuffCreature(state, player.Id, player.ActiveCards.Count - 1,
                  player.ActiveCards.Count - 1, player.ActiveCards.Count - 1, remainArgs);
              }
              )}
            )
          }
    };

    public static Card SpawnCard(CardName card)
    {
      return CardSet.Cards[card]();
    }
  }
}