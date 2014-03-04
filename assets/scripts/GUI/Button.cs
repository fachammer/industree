using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public GUISkin guiSkin;
    public Rect boundingRectangle;

    public Texture buttonTexture;
    public string buttonText;
    public bool hasTexture;

    private Rect buttonRectangle;

    public delegate void ButtonHandler(Button button);
    public event ButtonHandler ButtonDown = delegate(Button button) { };

    void OnGUI()
    {
        bool buttonPressed = GUI.Button(boundingRectangle, hasTexture ? new GUIContent(buttonTexture) : new GUIContent(buttonText), guiSkin.button);
        if (buttonPressed)
        {
            ButtonDown(this);
            Debug.Log("Button pressed");
        }
    }
}
