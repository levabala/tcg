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
    public List<Card> CardSet { get; set; }
    public List<Card> CardInHand { get; set; }
    public List<Card> ActiveCards { get; set; }

    public Player(int id)
    {
      this.Id = id;
      hero = new Hero(10, 10);
      CardSet = new List<Card>();
      CardInHand = new List<Card>();
      ActiveCards = new List<Card>();
    }
  }
}
