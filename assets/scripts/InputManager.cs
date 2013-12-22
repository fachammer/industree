using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public Player[] players;

    public delegate void SelectHandler(Player player, float selectDirection);
    public delegate void CastHandler(Player player, float castDirection);
    public delegate void GamePauseInputHandler();
    public delegate void GameExitInputHandler();
    public delegate void GameReloadInputHandler();

    public event SelectHandler PlayerSelect = delegate(Player player, float selectDirection) {};
    public event CastHandler PlayerCast = delegate(Player player, float castDirection) {};
    public event GamePauseInputHandler GamePauseInput = delegate() {}; 
    public event GameExitInputHandler GameExitInput = delegate() {};
    public event GameReloadInputHandler GameReloadInput = delegate() {};

    private float[] previousPlayerSelectAxes;
    private float[] previousPlayerCastAxes;

    private const string PAUSE_BUTTON = "GamePause";
    private const string EXIT_BUTTON = "GameExit";
    private const string RELOAD_BUTTON = "GameReload";

    private void Start()
    {
        previousPlayerSelectAxes = new float[players.Length];
        previousPlayerCastAxes = new float[players.Length];
    }

    private void Update()
    {
        checkPlayersInput();
        checkPauseInput();
        checkExitInput();
        checkReloadInput();
    }

    private void checkPlayersInput(){
        for (int i = 0; i < players.Length; i++)
        {
            float playerSelectAxis;
            float playerCastAxis;

            float playerSelectDirection = Utilities.GetAxisRawDown(players[i].selectInputName, previousPlayerSelectAxes[i], out playerSelectAxis);
            float playerCastDirection = Utilities.GetAxisRawDown(players[i].castInputName, previousPlayerCastAxes[i], out playerCastAxis);

            if (playerSelectDirection != 0)
            {
                PlayerSelect(players[i], playerSelectDirection);
            }

            if (playerCastDirection != 0)
            {
                PlayerCast(players[i], playerCastDirection);
            }

            previousPlayerSelectAxes[i] = playerSelectAxis;
            previousPlayerCastAxes[i] = playerCastAxis;
        }
    }

    private void checkPauseInput(){
        if(Input.GetButtonDown(PAUSE_BUTTON)){
            GamePauseInput();
        }
    }

    private void checkExitInput(){
        if(Input.GetButtonDown(EXIT_BUTTON)){
            GameExitInput();
        }
    }

    private void checkReloadInput(){
        if(Input.GetButtonDown(RELOAD_BUTTON)){
            GameReloadInput();
        }
    }
}
