using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  class Hero
  {
    public int HP {get; set;}
    public int Mana{get; set;}

    public Hero(int hp, int mana)
    {
      HP = hp;
      Mana = mana;
    }
  }
}
