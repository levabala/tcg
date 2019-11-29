using NUnit.Framework;
using tcg;
using System;

namespace tcgTests
{
  [TestFixture]
  public class MiddlewareLocalTestsFixture
  {
    [Test]
    public void TestMessages()
    {
      MiddlewareLocal mid1, mid2;
      (mid1, mid2) = MiddlewareLocal.GenerateLinkedMiddlewareLocalPair();

      Action<string> mid1InputHandler = (input) => Assert.AreEqual(input, "player2's message");
      Action<string> mid2InputHandler = (input) => Assert.AreEqual(input, "player1's message");

      mid1.SetInputHandler(mid1InputHandler);
      mid2.SetInputHandler(mid2InputHandler);

      mid1.SendData("player1's message");
      mid2.SendData("player2's message");
    }
  }

}