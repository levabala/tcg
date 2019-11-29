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
      MiddlewareLocal mid1, mid2, midHost;
      (mid1, mid2, midHost) = MiddlewareLocal.GenerateLinkedMiddlewareLocalTriple();

      Action<int, string> handleHostMessage = (i, input) => Assert.AreEqual(input, "host's message");
      mid1.AddInputHandler(handleHostMessage);
      mid2.AddInputHandler(handleHostMessage);

      midHost.AddInputHandler((i, input) =>
      {
        if (i == 0)
          Assert.AreEqual(input, "player1's message");
        else if (i == 1)
          Assert.AreEqual(input, "player2's message");
      });

      mid1.SendData("player1's message");
      mid2.SendData("player2's message");

      midHost.SendData("host's message");
    }
  }

}