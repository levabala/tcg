using NUnit.Framework;
using tcg;
using System;

namespace tcgTests
{
  [TestFixture]
  public class ClientTestsFixture
  {
    [Test]
    public void TestMessaging()
    {
      MiddlewareLocal mid1, mid2;
      (mid1, mid2) = MiddlewareLocal.GenerateLinkedMiddlewareLocalPair();

      Client c = new Client(mid1);

      mid2.AddInputHandler((senderId, input) => Assert.AreEqual(input, "client message"));

      c.SendCommand("client message");
    }
  }
}