using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public string[] playersSelectInputNames;
    public string[] playersActionInputNames;
    public string pauseButton;
    public string exitButton;
    public string reloadButton;

    private float[] previousPlayerSelectInputAxes;
    private float[] previousPlayerActionInputAxes;

    public delegate void PlayerSelectInputHandler(int playerIndex, float selectDirection);
    public delegate void PlayerActionInputHandler(int playerIndex, float actionDirection);
    public delegate void GamePauseInputHandler();
    public delegate void GameExitInputHandler();
    public delegate void GameReloadInputHandler();

    public event PlayerSelectInputHandler PlayerSelectInput = delegate(int playerIndex, float selectDirection) {};
    public event PlayerActionInputHandler PlayerActionInput = delegate(int playerIndex, float actionDirection) {};
    public event GamePauseInputHandler GamePauseInput = delegate() {}; 
    public event GameExitInputHandler GameExitInput = delegate() {};
    public event GameReloadInputHandler GameReloadInput = delegate() {};

    private void Start()
    {
        previousPlayerSelectInputAxes = new float[playersSelectInputNames.Length];
        previousPlayerActionInputAxes = new float[playersSelectInputNames.Length];
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
            float playerSelectInputAxis;
            float playerActionInputAxis;

            float playerSelectDirection = Utilities.GetAxisRawDown(playersSelectInputNames[i], previousPlayerSelectInputAxes[i], out playerSelectInputAxis);
            float playerActionDirection = Utilities.GetAxisRawDown(playersActionInputNames[i], previousPlayerActionInputAxes[i], out playerActionInputAxis);

            if (playerSelectDirection != 0)
            {
                PlayerSelectInput(i, playerSelectDirection);
            }

            if (playerActionDirection != 0)
            {
                PlayerActionInput(i, playerActionDirection);
            }

            previousPlayerSelectInputAxes[i] = playerSelectInputAxis;
            previousPlayerActionInputAxes[i] = playerActionInputAxis;
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
