using NUnit.Framework;
using NSubstitute;
using Industree.Time;

namespace Industree.Facade.Internal.Test
{
    public class GameTest
    {
        [Test]
        public void GivenGameControllerIsInstantiatedWhenPollutionReachesMaximumLevelThenHasGameEndedIsTrue()
        {
            IPlanet planet = Substitute.For<IPlanet>();
            Game game = new Game(planet);

            planet.MaximumPollutionReached += Raise.Event<System.Action>();

            Assert.That(game.HasGameEnded);
        }

        [Test]
        public void WhenGameControllerIsInstantiatedThenHasGameStartedIsFalse()
        {
            Game game = new Game(Substitute.For<IPlanet>());

            Assert.That(!game.HasGameStarted);
        }

        [Test]
        public void GivenGameControllerIsInstantiatedWhenStartGameIsCalledThenGameStartedIsTrue()
        {
            Game game = new Game(Substitute.For<IPlanet>());

            game.StartGame();

            Assert.That(game.HasGameStarted);
        }

        [Test]
        public void GivenGameControllerIsInstantiatedWhenStartGameIsCalledThenGameStartEventIsThrown()
        {
            Game game = new Game(Substitute.For<IPlanet>());
            game.GameStart += Assert.Pass;

            game.StartGame();

            Assert.Fail();
            
        }

        [Test]
        public void GivenGameControllerIsInstantiatedWhenPollutionReachesZeroThenHasGameEndedIsTrue()
        {
            IPlanet planet = Substitute.For<IPlanet>();
            Game game = new Game(planet);

            planet.ZeroPollutionReached += Raise.Event<System.Action>();

            Assert.That(game.HasGameEnded);
        }

        [Test]
        public void GivenGameControllerIsInstantiatedWhenPauseGameIsCalledThenIsGamePausedIsTrue()
        {
            Game game = new Game(Substitute.For<IPlanet>());

            game.PauseGame();

            Assert.That(game.IsGamePaused);
        }

        [Test]
        public void GivenGameControllerIsInstantiatedWhenPollutionReachesZeroThenPlayerWonGameIsTrue()
        {
            IPlanet planet = Substitute.For<IPlanet>();
            Game game = new Game(planet);

            planet.ZeroPollutionReached += Raise.Event<System.Action>();

            Assert.That(game.PlayerWonGame);
        }

        [Test]
        public void GivenGameControllerIsInstantiatedWhenPollutionReachesMaximumLevelThenPlayerWonGameIsFalse()
        {
            IPlanet planet = Substitute.For<IPlanet>();
            Game game = new Game(planet);

            planet.MaximumPollutionReached += Raise.Event<System.Action>();

            Assert.That(!game.PlayerWonGame);
        }

        [Test]
        public void WhenGameControllerIsInstantiatedThenPlayerWonGameIsFalse()
        {
            Game game = new Game(Substitute.For<IPlanet>());

            Assert.That(!game.PlayerWonGame);
        }

        [Test]
        public void GivenGameControllerIsInstantiatedWhenPollutionReachesZeroThenGameEndEventIsThrown()
        {
            IPlanet planet = Substitute.For<IPlanet>();
            Game game = new Game(planet);
            game.GameEnd += Assert.Pass;

            planet.ZeroPollutionReached += Raise.Event<System.Action>();

            Assert.Fail();
        }

        [Test]
        public void GivenGameControllerIsInstantiatedWhenPollutionReachesMaximumLevelThenGameEndEventIsThrown()
        {
            IPlanet planet = Substitute.For<IPlanet>();
            Game game = new Game(planet);
            game.GameEnd += Assert.Pass;

            planet.MaximumPollutionReached += Raise.Event<System.Action>();

            Assert.Fail();
        }

        [Test]
        public void GivenGameControllerIsInstantiatedWhenPollutionReachesZeroThenGameWinEventIsThrown()
        {
            IPlanet planet = Substitute.For<IPlanet>();
            Game game = new Game(planet);
            game.GameWin += Assert.Pass;

            planet.ZeroPollutionReached += Raise.Event<System.Action>();

            Assert.Fail();
        }

        [Test]
        public void GivenGameControllerIsInstantiatedWhenPollutionReachesMaximumLevelThenGameLoseEventIsThrown()
        {
            IPlanet planet = Substitute.For<IPlanet>();
            Game game = new Game(planet);
            game.GameLose += Assert.Pass;

            planet.MaximumPollutionReached += Raise.Event<System.Action>();

            Assert.Fail();
        }

        [Test]
        public void GivenGameControllerIsInstantiatedWhenPauseGameIsCalledThenGamePauseEventIsThrown()
        {
            Game game = new Game(Substitute.For<IPlanet>());
            game.GamePause += Assert.Pass;

            game.PauseGame();

            Assert.Fail();
        }
    }
}
