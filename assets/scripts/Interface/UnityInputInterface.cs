using UnityEngine;
using System.Collections.Generic;
using assets.scripts.Miscellaneous;

public class UnityInputInterface : MonoBehaviour
{
    public string[] playersSelectInputNames;
    public string[] playersActionInputNames;
    public string pauseButton;
    public string exitButton;
    public string reloadButton;

    private Player[] players;
    private float[] previousPlayerSelectInputAxes;
    private float[] previousPlayerActionInputAxes;

    public event System.Action<Player, float> PlayerActionSelectInput = (player, selectDirection) => {};
    public event System.Action<Player, float> PlayerActionInput = (player, actionDirection) => {};
    public event System.Action GamePauseInput = () => {}; 
    public event System.Action GameExitInput = () => {};
    public event System.Action GameReloadInput = () => {};

    private void Awake(){
        players = Player.GetAll();
        
        previousPlayerSelectInputAxes = new float[playersSelectInputNames.Length];
        previousPlayerActionInputAxes = new float[playersSelectInputNames.Length];
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
                PlayerActionSelectInput(players[i], playerSelectDirection);
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

    public static UnityInputInterface Get()
    {
        return GameObject.FindGameObjectWithTag(Tags.userInterface).GetComponent<UnityInputInterface>();
    }
}
