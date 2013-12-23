using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public string[] playersSelectInputNames;
    public string[] playersCastInputNames;
    public string pauseButton;
    public string exitButton;
    public string reloadButton;

    public delegate void SelectHandler(int playerIndex, float selectDirection);
    public delegate void CastHandler(int playerIndex, float castDirection);
    public delegate void GamePauseInputHandler();
    public delegate void GameExitInputHandler();
    public delegate void GameReloadInputHandler();

    public event SelectHandler PlayerSelect = delegate(int playerIndex, float selectDirection) {};
    public event CastHandler PlayerCast = delegate(int playerIndex, float castDirection) {};
    public event GamePauseInputHandler GamePauseInput = delegate() {}; 
    public event GameExitInputHandler GameExitInput = delegate() {};
    public event GameReloadInputHandler GameReloadInput = delegate() {};

    private float[] previousPlayerSelectAxes;
    private float[] previousPlayerCastAxes;

    private void Start()
    {
        previousPlayerSelectAxes = new float[playersSelectInputNames.Length];
        previousPlayerCastAxes = new float[playersSelectInputNames.Length];
    }

    private void Update()
    {
        checkPlayersInput();
        checkPauseInput();
        checkExitInput();
        checkReloadInput();
    }

    private void checkPlayersInput(){
        for (int i = 0; i < playersSelectInputNames.Length; i++)
        {
            float playerSelectAxis;
            float playerCastAxis;

            float playerSelectDirection = Utilities.GetAxisRawDown(playersSelectInputNames[i], previousPlayerSelectAxes[i], out playerSelectAxis);
            float playerCastDirection = Utilities.GetAxisRawDown(playersCastInputNames[i], previousPlayerCastAxes[i], out playerCastAxis);

            if (playerSelectDirection != 0)
            {
                PlayerSelect(i, playerSelectDirection);
            }

            if (playerCastDirection != 0)
            {
                PlayerCast(i, playerCastDirection);
            }

            previousPlayerSelectAxes[i] = playerSelectAxis;
            previousPlayerCastAxes[i] = playerCastAxis;
        }
    }

    private void checkPauseInput(){
        if(Input.GetButtonDown(pauseButton)){
            GamePauseInput();
        }
    }

    private void checkExitInput(){
        if(Input.GetButtonDown(exitButton)){
            GameExitInput();
        }
    }

    private void checkReloadInput(){
        if(Input.GetButtonDown(reloadButton)){
            GameReloadInput();
        }
    }
}
