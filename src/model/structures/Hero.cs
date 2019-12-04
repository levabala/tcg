using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  class Hero
  {
    public int HP { get; set; }
    public int Mana { get; set; }

    public Hero(int hp, int mana)
    {
      HP = hp;
      Mana = mana;
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;

      Hero hero2 = (Hero)obj;
      return hero2.HP == this.HP && hero2.Mana == this.Mana;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    static public Hero CommonHero()
    {
      return new Hero(10, 10);
    }
  }
}
