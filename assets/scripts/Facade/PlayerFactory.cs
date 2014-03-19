using Industree.Facade.Internal;
using System;
using System.Linq;
using UnityEngine;

namespace Industree.Facade
{
    public class PlayerFactory
    {
        public static IPlayer[] GetAll()
        {
            IPlayer[] players = Array.ConvertAll(GameObject.FindGameObjectsWithTag(Tags.player), (gameObject) => gameObject.GetComponent<Player>());
            Array.Sort<IPlayer>(players, (p1, p2) => p1.Index - p2.Index);
            return players;
        }

        public static IPlayer GetPlayer(int index)
        {
            return (from IPlayer player in GetAll()
                        where player.Index == index
                        select player).First();
        }
    }
}
