using NUnit.Framework;
using tcg;

namespace tcgTests
{
  [TestFixture]
  public class HostTestsFixture
  {
    [Test]
    public void HandlingInputTest1()
    {
      Host h = new Host();

      ResponseType responseType;
      string responseMessage;
      (responseType, responseMessage) = h.HandleInput("attack 1 3");

      Assert.AreEqual(responseType, ResponseType.Success);
    }

    [Test]
    public void HandlingInputTest2()
    {
      Host h = new Host();

      ResponseType responseType;
      string responseMessage;
      (responseType, responseMessage) = h.HandleInput("qwe");

      Assert.AreEqual(responseType, ResponseType.Error);
    }
  }
}