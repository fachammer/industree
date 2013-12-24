using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public enum Side { left, right };
    
    public int credits;
    public int creditsUpInterval;
    public int creditsPerInterval;
    public Side side;
    public List<Interactive> interactiveList;
    public Texture2D iconCredit;
    public Texture2D interactiveCooldownOverlayIcon;
    public int iconSize = 20;
    public int iconTopOffset = 100;
    public string selectInputName;
    public string castInputName;

    private List<float> interactiveCoolDownTimers;
    private List<Rect> interactiveRect;
    private Rect creditRect;
    private List<Rect> interactiveCooldownOverlayRects;
    private int curSelected = 0;
    private InputManager inputManager;    

    private GameController gameController;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();

        gameController.GamePause += OnGamePause;
        gameController.GameResume += OnGameResume;
        gameController.GameEnd += OnGameEnd;

        interactiveRect = new List<Rect>();
        //Callculate the offset for each player
        int sideOffset = (side == Side.left) ? 0 : (Screen.width - iconSize);

        //Generate the Credit rect
        creditRect = new Rect(sideOffset, iconTopOffset - 30, iconSize, 30);

        interactiveCoolDownTimers = new List<float>();
        interactiveCooldownOverlayRects = new List<Rect>();

        //Fill the rectList 
        for (int i = 0; i < interactiveList.Count; i++)
        {
            Rect currentInteractiveRect = new Rect(sideOffset, iconTopOffset + iconSize * i, iconSize, iconSize);
            interactiveRect.Add(currentInteractiveRect);
            interactiveCooldownOverlayRects.Add(currentInteractiveRect);
            interactiveCoolDownTimers.Add(interactiveList[i].cooldownTime);
        }

        inputManager = GameObject.FindGameObjectWithTag(Tags.inputManager).GetComponent<InputManager>();

        inputManager.PlayerCast += OnInteractiveCast;

        Timer.Instantiate(creditsUpInterval, OnCreditsUpTimerTick);
    }

    private void OnGamePause(){
        enabled = false;
    }

    private void OnGameResume(){
        enabled = true;
    }

    private void OnGameEnd(bool win){
        enabled = false;
    }

    private void OnInteractiveCast(int playerIndex, float castDirection)
    {
        if (playerIndex == 0 && side == Side.left)
        {
            if (interactiveCoolDownTimers[curSelected] <= 0)
            {
                if (credits >= interactiveList[curSelected].cost && interactiveList[curSelected].performAction(this, castDirection))
                {
                    credits -= interactiveList[curSelected].cost;
                    interactiveCoolDownTimers[curSelected] = interactiveList[curSelected].cooldownTime;
                }
            }
        }
    }

    private void OnCreditsUpTimerTick(Timer timer){
        credits += creditsPerInterval;
    }

    private void Update()
    {
        updateCooldowns();
    }

    public void OnGUI()
    {
        /*
        GUI.skin.font = GameObject.FindGameObjectWithTag(Tags.style).GetComponent<Style>().font;
        GUI.Label(creditRect, credits.ToString());

        Rect r;
        if (side == Side.left) r = new Rect(creditRect.xMax, creditRect.height + iconCredit.height, iconCredit.width, iconCredit.height);
        else r = new Rect(creditRect.xMin - iconCredit.width, creditRect.height + iconCredit.height, iconCredit.width, iconCredit.height);
        GUI.DrawTexture(r, iconCredit);

        //Show the Actions
        for (int i = 0; i < interactiveList.Count; i++)
        {
            if (interactiveCoolDownTimers[i] > 0)
            {
                interactiveCooldownOverlayRects[i] = calculateCooldownOverlayRect(i);
                GUI.DrawTexture(interactiveCooldownOverlayRects[i], interactiveCooldownOverlayIcon);
            }
        }*/
    }

    private void updateCooldowns()
    {
        for (int i = 0; i < interactiveCoolDownTimers.Count; i++)
        {
            if (interactiveCoolDownTimers[i] > 0)
            {
                interactiveCoolDownTimers[i] -= Time.deltaTime;
            }
        }
    }

    private Rect calculateCooldownOverlayRect(int currentInteractiveIndex)
    {
        if (side == Side.left)
        {
            return new Rect(
                    interactiveCooldownOverlayRects[currentInteractiveIndex].x,
                    interactiveCooldownOverlayRects[currentInteractiveIndex].y,
                    interactiveRect[currentInteractiveIndex].width * interactiveCoolDownTimers[currentInteractiveIndex] /
                        interactiveList[currentInteractiveIndex].cooldownTime,
                    interactiveCooldownOverlayRects[currentInteractiveIndex].height);
        }
        else
        {
            float overlayWidth = interactiveRect[currentInteractiveIndex].width * interactiveCoolDownTimers[currentInteractiveIndex] / interactiveList[currentInteractiveIndex].cooldownTime;
            return new Rect(
                    interactiveRect[currentInteractiveIndex].x + (interactiveRect[currentInteractiveIndex].width - overlayWidth),
                    interactiveCooldownOverlayRects[currentInteractiveIndex].y,
                    overlayWidth,
                    interactiveCooldownOverlayRects[currentInteractiveIndex].height);
        }
    }

    public bool ActIfPossible(Interactive interactive, float castDirection){
        return false;
    }
}
