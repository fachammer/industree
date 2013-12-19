using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StartScreen:MonoBehaviour
{
    public Texture2D logo;
    public Texture2D description;

    public Vector2 positionLogo;
    public Vector2 positionDescription;
	
	public void Start(){
		Screen.lockCursor = true;
		Screen.showCursor = false;
	}

    public void Update()
    {		
        //Start game when space is pressed
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel(1);
        }

        //Ends the game on Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //Shows the Description and the logo
    public void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width-logo.width*1.8f,positionLogo.y,logo.width,logo.height),logo);
        GUI.DrawTexture(new Rect(Screen.width-description.width*1.8f, logo.height*1.4f, description.width, description.height), description);
    }
}

