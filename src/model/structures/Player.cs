﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  class Player
  {
    public int Id { get; set; }

    public Hero Hero { get; set; }
    public List<Card> CardSet { get; set; }
    public List<Card> CardInHand { get; set; }
    public List<Card> ActiveCards { get; set; }

    public Player(int id, List<Card> cardSet, List<Card> cardsInHand, List<Card> activeCards)
    {
      this.Id = id;
      Hero = new Hero(10, 10);

      this.CardSet = cardSet;
      this.CardInHand = cardsInHand;
      this.ActiveCards = activeCards;
    }

    public Player(int id)
    {
      this.Id = id;
      Hero = new Hero(10, 10);

      this.CardSet = new List<Card>();
      this.CardInHand = new List<Card>();
      this.ActiveCards = new List<Card>();
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;

      Player player2 = (Player)obj;
      return player2.Id == this.Id && player2.Hero.Equals(this.Hero) && Enumerable.SequenceEqual(player2.CardSet, this.CardSet) && Enumerable.SequenceEqual(player2.ActiveCards, this.ActiveCards) && Enumerable.SequenceEqual(player2.CardInHand, this.CardInHand);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
