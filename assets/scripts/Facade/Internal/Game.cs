using Industree.Graphics;
using Industree.Time;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("Assembly-CSharp-Editor")]

namespace Industree.Facade.Internal
{
    internal class Game : IGame
    {
        private bool playerWonGame;
        private IPlanet planet;

        public bool HasGameEnded { get; private set; }
        public bool HasGameStarted { get; private set; }
        public bool IsGamePaused { get; private set; }
        public bool PlayerWonGame { get { return playerWonGame; } }
        public ITexture PauseTexture { get { throw new System.NotImplementedException(); } }
        public ITexture WinTexture { get { throw new System.NotImplementedException(); } }
        public ITexture LoseTexture { get { throw new System.NotImplementedException(); } }
        public Rect ScreenBounds { get { throw new System.NotImplementedException(); } }

        public event System.Action GameStart = () => { };
        public event System.Action GamePause = () => { };
        public event System.Action GameResume = () => { };
        public event System.Action GameEnd = () => { };
        public event System.Action GameWin = () => { };
        public event System.Action GameLose = () => { };

        public Game(IPlanet planet)
        {
            HasGameStarted = false;
            playerWonGame = false;
            planet.MaximumPollution += () => EndGame(false);
            planet.ZeroPollution += () => EndGame(true);
        }

        private void EndGame(bool playerWonGame)
        {
            this.playerWonGame = playerWonGame;

            HasGameEnded = true;
            GameEnd();

            if (playerWonGame)
                GameWin();
            else
                GameLose();

            PauseGame();
        }

        public void StartGame()
        {
            HasGameStarted = true;
            GameStart();
        }

        public void PauseGame()
        {
            IsGamePaused = true;
            GamePause();
        }

        public void ResumeGame()
        {
            throw new System.NotImplementedException();
        }


        
    }
}