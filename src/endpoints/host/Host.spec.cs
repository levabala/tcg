using NUnit.Framework;

namespace tcgTests
{
  [TestFixture]
  public class tcgTestsFixture
  {

    [Test]
    public void JustATest()
    {
      Assert.Pass("done with it!");
    }


    [Test]
    public void JustAFailureTest()
    {
      Assert.Fail("fail with it!");
    }
  }
}