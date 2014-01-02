using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    public string[] playersSelectInputNames;
    public string[] playersActionInputNames;
    public string pauseButton;
    public string exitButton;
    public string reloadButton;

    private Player[] players;
    private float[] previousPlayerSelectInputAxes;
    private float[] previousPlayerActionInputAxes;
    private Dictionary<Player, Dictionary<Action, Rect>> actionSlots;
    private SelectedActionManager selectedActionManager;

    public delegate void PlayerSelectInputHandler(Player player, float selectDirection);
    public delegate void PlayerActionInputHandler(Player player, float actionDirection);
    public delegate void GamePauseInputHandler();
    public delegate void GameExitInputHandler();
    public delegate void GameReloadInputHandler();

    public event PlayerSelectInputHandler PlayerSelectInput = delegate(Player player, float selectDirection) {};
    public event PlayerActionInputHandler PlayerActionInput = delegate(Player player, float actionDirection) {};
    public event GamePauseInputHandler GamePauseInput = delegate() {}; 
    public event GameExitInputHandler GameExitInput = delegate() {};
    public event GameReloadInputHandler GameReloadInput = delegate() {};

    private void Awake(){
        players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
        selectedActionManager = GameObject.FindGameObjectWithTag(Tags.gameStateManager).GetComponent<SelectedActionManager>();
        
        previousPlayerSelectInputAxes = new float[playersSelectInputNames.Length];
        previousPlayerActionInputAxes = new float[playersSelectInputNames.Length];
    }

    private void Start(){
        actionSlots = GameObject.FindGameObjectWithTag(Tags.gui).GetComponent<ActionIconsGUI>().ActionSlots;
    }

    private void Update(){
        CheckPlayersInput();
        CheckPauseInput();
        CheckExitInput();
        CheckReloadInput();
    }

    private void CheckPlayersInput(){
        for (int i = 0; i < playersSelectInputNames.Length; i++){
            float playerSelectInputAxis;
            float playerActionInputAxis;

            float playerSelectDirection = Utilities.GetAxisRawDown(playersSelectInputNames[i], previousPlayerSelectInputAxes[i], out playerSelectInputAxis);
            float playerActionDirection = Utilities.GetAxisRawDown(playersActionInputNames[i], previousPlayerActionInputAxes[i], out playerActionInputAxis);

            if (playerSelectDirection != 0)
            {
                PlayerSelectInput(players[i], playerSelectDirection);
            }

            if (playerActionDirection != 0)
            {
                PlayerActionInput(players[i], playerActionDirection);
            }

            previousPlayerSelectInputAxes[i] = playerSelectInputAxis;
            previousPlayerActionInputAxes[i] = playerActionInputAxis;
        }
    }

    private void CheckPauseInput(){
        if(Input.GetButtonDown(pauseButton)){
            GamePauseInput();
        }
    }

    private void CheckExitInput(){
        if(Input.GetButtonDown(exitButton)){
            GameExitInput();
        }
    }

    private void CheckReloadInput(){
        if(Input.GetButtonDown(reloadButton)){
            GameReloadInput();
        }
    }
}
