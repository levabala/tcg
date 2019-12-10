using NUnit.Framework;
using tcg;
using System;
using System.Threading;

namespace tcgTests
{
  [TestFixture]
  public class MiddlewareNetworkTestsFixture
  {
    [Test]
    public void TestMessages()
    {
      var port1 = 3001;
      var mid1 = new MiddlewareNetwork("127.0.0.1", port1, true);

      var port2 = 3002;
      var mid2 = new MiddlewareNetwork("127.0.0.1", port2, true);

      mid1.ConnectMiddleware(mid2);
      mid2.ConnectMiddleware(mid1);

      int messagesCame = 0;
      mid1.AddInputHandler((i, s) =>
      {
        Assert.AreEqual(s, "message from mid2");
        messagesCame++;
      });
      mid2.AddInputHandler((i, s) =>
      {
        Assert.AreEqual(s, "message from mid1");
        messagesCame++;
      });

      mid1.SendData("message from mid1");
      mid2.SendData("message from mid2");

      Thread.Sleep(200);
      Assert.AreEqual(2, messagesCame);
    }

  }
}