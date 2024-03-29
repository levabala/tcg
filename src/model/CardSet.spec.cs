using NUnit.Framework;
using tcg;
using System;
using System.Collections.Generic;

namespace tcgTests
{
  [TestFixture]
  public class CardSetTestsFixture
  {
    [Test]
    public void FlashHealTest()
    {
      Func<Card> testingCardGenerator = CardSet.Cards[CardSet.CardName.FlashHeal];

      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { testingCardGenerator() },
            new List<Card> { Card.DimonCard(),((Func<Card>)(() => {
              var card = Card.DimonStrongCard();
              // Init StrongDimon damaged for 6 points
              card.HP -= 6;
              return card;
            }))() }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard(), Card.DimonCard() }
          ) ,
        }
      );

      Host host = new Host(new MiddlewareLocal(), new List<IMiddleware> { });
      host.state = state;
      host.ProcessInput(0, "play 0 0 1");

      GameState stateExpected = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), ((Func<Card>)(() => {
              var card = Card.DimonStrongCard();
              card.HP -= 6;
              // Flash Heal must heal selected card with 5 points
              card.HP += 5;
              return card;
            }))() },
            ((Func<Hero>)(() => {
              var hero = Hero.CommonHero();
              hero.Mana -= testingCardGenerator().ManaCost;
              return hero;
            }))()
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard(), Card.DimonCard() }
          ) ,
        }
      );

      Assert.AreEqual(host.state, stateExpected);
    }

    [Test]
    public void IronforgeRiflemanTest()
    {
      Func<Card> testingCardGenerator = CardSet.Cards[CardSet.CardName.IronforgeRifleman];

      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { testingCardGenerator() },
            new List<Card> { Card.DimonCard()}
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard(), Card.DimonStrongCard() }
          ) ,
        }
      );

      Host host = new Host(new MiddlewareLocal(), new List<IMiddleware> { });
      host.state = state;
      host.ProcessInput(0, "play 0 1 2");

      GameState stateExpected = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), testingCardGenerator()},
            ((Func<Hero>)(() => {
              var hero = Hero.CommonHero();
              hero.Mana -= testingCardGenerator().ManaCost;
              return hero;
            }))()
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard(), ((Func<Card>)(() => {
              var card = Card.DimonStrongCard(); 
              // Ironforge Rifleman must deal damage selected card with 1 points
              card.HP -= 1;
              return card;
            }))() }
          ) ,
        }
      );

      Assert.AreEqual(host.state, stateExpected);
    }

    [Test]
    public void VoodooDoctorTest()
    {
      Func<Card> testingCardGenerator = CardSet.Cards[CardSet.CardName.VoodooDoctor];

      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { testingCardGenerator() },
            new List<Card> { Card.DimonCard(),((Func<Card>)(() => {
              var card = Card.DimonStrongCard();
              // Init StrongDimon damaged for 6 points
              card.HP -= 6;
              return card;
            }))() }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard(), Card.DimonCard() }
          ) ,
        }
      );

      Host host = new Host(new MiddlewareLocal(), new List<IMiddleware> { });
      host.state = state;

      host.ProcessInput(0, "play 0 0 1");
      GameState stateExpected = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), ((Func<Card>)(() => {
              var card = Card.DimonStrongCard();
              card.HP -= 6;
              // Voodoo Doctor must heal selected card with 5 points
              card.HP += 2;
              return card;
            }))(), testingCardGenerator() },

            ((Func<Hero>)(() => {
              var hero = Hero.CommonHero();
              hero.Mana -= testingCardGenerator().ManaCost;
              return hero;
            }))()
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard(), Card.DimonCard() }
          ) ,
        }
      );

      Assert.AreEqual(host.state, stateExpected);
    }

    [Test]
    public void StarfireTest()
    {
      Func<Card> testingCardGenerator = CardSet.Cards[CardSet.CardName.Starfire];

      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { Card.DimonStrongCard(), Card.DimonCard() },
            new List<Card> { testingCardGenerator() },
            new List<Card> { Card.DimonCard(), Card.DimonCard() }
          ) ,
          new Player(
            1,
            new List<Card> {  },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard() }
          ) ,
        }
      );

      Host host = new Host(new MiddlewareLocal(), new List<IMiddleware> { });
      host.state = state;
      host.ProcessInput(0, "play 0 1 1");

      GameState stateExpected = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { Card.DimonCard() },
            new List<Card> { Card.DimonStrongCard(), },
            new List<Card> { Card.DimonCard(), Card.DimonCard()},
            ((Func<Hero>)(() => {
              var hero = Hero.CommonHero();
              hero.Mana -= testingCardGenerator().ManaCost;
              return hero;
            }))()
          ) ,
          new Player(
            1,
            new List<Card> {  },
            new List<Card> { },
            new List<Card> { Card.DimonCard() }
          ) ,
        }
      );

      Assert.AreEqual(host.state, stateExpected);
    }

    [Test]
    public void MultiShotTest()
    {
      Func<Card> testingCardGenerator = CardSet.Cards[CardSet.CardName.MultiShot];

      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { testingCardGenerator() },
            new List<Card> { }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { Card.DimonCard(), Card.DimonCard() }
          ) ,
        }
      );

      Host host = new Host(new MiddlewareLocal(), new List<MiddlewareLocal> { });
      host.state = state;
      host.ProcessInput(0, "play 0");

      GameState stateExpected = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { },
            ((Func<Hero>)(() => {
              var hero = Hero.CommonHero();
              hero.Mana -= testingCardGenerator().ManaCost;
              return hero;
            }))()
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { }
          ) ,
        }
      );

      Assert.AreEqual(host.state, stateExpected);
    }

    [Test]
    public void StonetuskBoarTest()
    {
      Func<Card> testingCardGenerator = CardSet.Cards[CardSet.CardName.StonetuskBoar];
      Func<Card> murloc = CardSet.Cards[CardSet.CardName.MurlocScout];

      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { testingCardGenerator() },
            new List<Card> { }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { murloc(), murloc() }
          ) ,
        }
      );

      Host host = new Host(new MiddlewareLocal(), new List<MiddlewareLocal> { });
      host.state = state;
      host.ProcessInput(0, "play 0");
      host.ProcessInput(0, "attack 0 0");

      GameState stateExpected = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { },
            ((Func<Hero>)(() => {
              var hero = Hero.CommonHero();
              hero.Mana -= testingCardGenerator().ManaCost;
              return hero;
            }))()
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> {murloc() }
          ),
        }
      );

      Assert.AreEqual(host.state, stateExpected);
    }
    [Test]
    public void MurlocTidehunterTest()
    {
      Func<Card> testingCardGenerator = CardSet.Cards[CardSet.CardName.MurlocTidehunter];
      Func<Card> murloc = CardSet.Cards[CardSet.CardName.MurlocScout];

      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { testingCardGenerator() },
            new List<Card> { }
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { }
          ) ,
        }
      );

      Host host = new Host(new MiddlewareLocal(), new List<MiddlewareLocal> { });
      host.state = state;
      host.ProcessInput(0, "play 0");
      host.ProcessInput(0, "attack 0 0");

      GameState stateExpected = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { testingCardGenerator(), murloc() },
            ((Func<Hero>)(() => {
              var hero = Hero.CommonHero();
              hero.Mana -= testingCardGenerator().ManaCost;
              return hero;
            }))()
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { }
          ),
        }
      );

      Assert.AreEqual(host.state, stateExpected);
    }

    [Test]
    public void ShatteredSunClericTest()
    {
      Func<Card> testingCardGenerator = CardSet.Cards[CardSet.CardName.ShatteredSunCleric];
      Func<Card> murloc = CardSet.Cards[CardSet.CardName.MurlocScout];

      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { testingCardGenerator() },
            new List<Card> { murloc()}
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { }
          ) ,
        }
      );

      Host host = new Host(new MiddlewareLocal(), new List<MiddlewareLocal> { });
      host.state = state;
      host.ProcessInput(0, "play 0 0 0");

      GameState stateExpected = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { ((Func<Card>)(() => {
              var card = murloc();
              card.HP += 1;
              card.Attack += 1;
              return card;
            }))() , testingCardGenerator()},
            ((Func<Hero>)(() => {
              var hero = Hero.CommonHero();
              hero.Mana -= testingCardGenerator().ManaCost;
              return hero;
            }))()
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { }
          ),
        }
      );

      Assert.AreEqual(host.state, stateExpected);
    }

    [Test]
    public void FrostwolfWarlordTest()
    {
      Func<Card> testingCardGenerator = CardSet.Cards[CardSet.CardName.FrostwolfWarlord];
      Func<Card> murloc = CardSet.Cards[CardSet.CardName.MurlocScout];

      GameState state = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { testingCardGenerator() },
            new List<Card> { murloc(), murloc()}
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { }
          ) ,
        }
      );

      Host host = new Host(new MiddlewareLocal(), new List<MiddlewareLocal> { });
      host.state = state;
      host.ProcessInput(0, "play 0");

      GameState stateExpected = new GameState(
        new Player[] {
          new Player(
            0,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { murloc(), murloc(), ((Func<Card>)(() => {
              var card = testingCardGenerator();
              card.HP += 2;
              card.Attack += 2;
              return card;
            }))()},
            ((Func<Hero>)(() => {
              var hero = Hero.CommonHero();
              hero.Mana -= testingCardGenerator().ManaCost;
              return hero;
            }))()
          ) ,
          new Player(
            1,
            new List<Card> { },
            new List<Card> { },
            new List<Card> { }
          ),
        }
      );

      Assert.AreEqual(host.state, stateExpected);
    }
  }
}