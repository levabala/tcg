using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using tcg;

namespace tcgTests
{
  [TestFixture]
  public class CardTestFixture
  {
    [Test]
    public void EqualityTest()
    {
      Card c1 = new Card("card1", 0, 0, 0);
      Card c1_but_another_instance = new Card("card1", 0, 0, 0);

      Assert.AreEqual(c1, c1_but_another_instance);
    }

    [Test]
    public void CloningTest()
    {
      Card c1 = new Card("card1", 0, 0, 0);
      Card c1_clone = c1.Clone();

      // by value
      Assert.AreEqual(c1, c1_clone);
      // by link
      Assert.AreNotSame(c1, c1_clone);

      // save card
      Dictionary<Card, Card> previousCards = new Dictionary<Card, Card>() {
        { c1, c1_clone}
      };

      // modify main card
      c1.HP += 3;
      Assert.AreNotEqual(c1, c1_clone);

      // get old version
      var restoredCard = previousCards[c1];
      Assert.AreSame(c1_clone, restoredCard);
      Assert.AreEqual(c1_clone, restoredCard);

      // now we have c1 (modified) and its' old clone
    }

    [Test]
    public void TostringTest(){
      Card cd1 = new Card("card", 0, 0, 0);

      string expected = "card Mana cost: 0, HP: 0, Attack: 0   \n";

      Assert.AreEqual(cd1.ToString(), expected);

      cd1.previous = cd1.Clone();

      cd1.HP -= 1;

      expected = "card Mana cost: 0, HP: -1 (-1), Attack: 0   \n";

      Assert.AreEqual(expected, cd1.ToString());

      cd1.HP += 3;

      expected = "card Mana cost: 0, HP: 2 (+2), Attack: 0   \n";

      Assert.AreEqual(expected, cd1.ToString());

      cd1.HP = 0;
      cd1.Attack -=1;

      expected = "card Mana cost: 0, HP: 0, Attack: -1 (-1)   \n";

      Assert.AreEqual(expected, cd1.ToString());

      cd1.Attack +=3;

      expected = "card Mana cost: 0, HP: 0, Attack: 2 (+2)   \n";

      Assert.AreEqual(expected, cd1.ToString());
    }
  }
}