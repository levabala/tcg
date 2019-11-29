using NUnit.Framework;
using tcg;
using System;

namespace tcgTests
{
  [TestFixture]
  public class HostTestsFixture
  {
    private (MiddlewareLocal, MiddlewareLocal, Host) InitializePreset()
    {
      MiddlewareLocal mid1, mid2;
      (mid1, mid2) = MiddlewareLocal.GenerateLinkedMiddlewareLocalPair();

      Host host = new Host(mid1, mid2);

      return (mid1, mid2, host);
    }

    [Test]
    public void Initialize()
    {
      Middleware mid1, mid2;
      Host host;

      (mid1, mid2, host) = InitializePreset();
    }

    [Test]
    public void SendDataTest()
    {
      Middleware mid1, mid2;
      Host host;

      (mid1, mid2, host) = InitializePreset();


      Action<int, string> inputHandler = (playerIndex, input) =>
      {
        if (playerIndex == 0)
          Assert.AreEqual("player1 message", input);
        else if (playerIndex == 1)
          Assert.AreEqual("player2 message", input);
      };

      host.AddHandlerInput(inputHandler);

      mid1.SendData("player1 message");
      mid2.SendData("player2 message");
    }
  }
}