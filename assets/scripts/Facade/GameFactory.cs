using Industree.Facade.Internal;

namespace Industree.Facade
{
    public static class GameFactory
    {
        private static IGame instance;

        public static IGame GetGameInstance()
        {
            if(instance == null){
                IPlanet planet = PlanetFactory.GetPlanet();
                instance = new Game(planet);
            }

            return instance;
        }
    }
}
