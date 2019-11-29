using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  class Player
  {
    public int Id { get; set; }

    public Hero hero { get; set; }
    public Card[] CardSet { get; set; }
    public Card[] CardInHand { get; set; }
    public Card[] ActiveCards { get; set; }

    public Player(int id)
    {
      this.Id = id;
      hero = new Hero(10, 10);
      CardSet = new Card[3];
      CardInHand = new Card[3];
      ActiveCards = new Card[3];
    }
  }
}
