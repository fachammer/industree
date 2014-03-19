using UnityEngine;
using System.Collections.Generic;
using Industree.Miscellaneous;

public class GameControllerInput : MonoBehaviour
{
    public string pauseButton;
    public string exitButton;
    public string reloadButton;

    public event System.Action GamePauseInput = () => { }; 
    public event System.Action GameExitInput = () => { };
    public event System.Action GameReloadInput = () => { };

    private void Update(){
        CheckPauseInput();
        CheckExitInput();
        CheckReloadInput();
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

    public static GameControllerInput Get()
    {
        return GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameControllerInput>();
    }
}
