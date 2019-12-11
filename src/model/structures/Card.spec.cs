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

  }
}