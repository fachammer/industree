using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public enum Side { left, right };
    public int credits = 0;
    public int creditsPerSec = 1;

    public Side side;
    public List<Interactive> interactiveList;
    private List<float> interactiveCoolDownTimers;
    private List<Rect> interactiveRect;
    private Rect creditRect;

    public Texture2D iconSelected;
    public Texture2D iconCredit;
    public Texture2D interactiveCooldownOverlayIcon;
    public Texture2D interactiveDeniedOverlayIcon;
    private float[] drawRedCrossTimers;
    private float drawRedCrossOverlayTime = 0.5f;
    private List<Rect> interactiveCooldownOverlayRects;

    public int iconSize = 20;
    public int iconTopOffset = 100;
    private int curSelected = 0;

    private float lastTime = 0;

    private InputManager inputManager;

    public string selectInputName;
    public string castInputName;

    // Use this for initialization
    void Start()
    {
        interactiveRect = new List<Rect>();
        //Callculate the offset for each player
        int sideOffset = (side == Side.left) ? 0 : (Screen.width - iconSize);

        //Generate the Credit rect
        creditRect = new Rect(sideOffset, iconTopOffset - 30, iconSize, 30);

        interactiveCoolDownTimers = new List<float>();
        interactiveCooldownOverlayRects = new List<Rect>();
        drawRedCrossTimers = new float[interactiveList.Count];

        //Fill the rectList with 
        for (int i = 0; i < interactiveList.Count; i++)
        {
            Rect currentInteractiveRect = new Rect(sideOffset, iconTopOffset + iconSize * i, iconSize, iconSize);
            interactiveRect.Add(currentInteractiveRect);
            interactiveCooldownOverlayRects.Add(currentInteractiveRect);
            interactiveCoolDownTimers.Add(interactiveList[i].cooldownTime);
        }

        inputManager = GameObject.FindGameObjectWithTag(Tags.inputManager).GetComponent<InputManager>();

        inputManager.PlayerCast += OnInteractiveCast;
        inputManager.PlayerSelect += OnInteractiveSelection;
    }

    private void OnInteractiveSelection(Player player, float selectDirection)
    {
        if (player == this)
        {
            if (selectDirection < 0 && curSelected < interactiveList.Count - 1)
            {
                curSelected++;
            }
            else if (selectDirection > 0 && curSelected > 0)
            {
                curSelected--;
            }
        }
    }

    private void OnInteractiveCast(Player player, float castDirection)
    {
        if (player == this)
        {
            if (interactiveCoolDownTimers[curSelected] <= 0)
            {
                if (credits >= interactiveList[curSelected].cost && interactiveList[curSelected].performAction(this, castDirection))
                {
                    credits -= interactiveList[curSelected].cost;
                    interactiveCoolDownTimers[curSelected] = interactiveList[curSelected].cooldownTime;
                    drawRedCrossTimers[curSelected] = 0;
                }
                else
                {
                    drawRedCrossTimers[curSelected] = drawRedCrossOverlayTime;
                }
            }
        }
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag(Tags.pause).GetComponent<Pause>().paused ||
            GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>().gameEnded)
            return;

        createCredits();

        updateCooldowns();
        updateRedCrossTimers();
    }

    public void OnGUI()
    {
        GUI.skin.font = GameObject.FindGameObjectWithTag(Tags.style).GetComponent<Style>().font;
        GUI.Label(creditRect, credits.ToString());

        Rect r;
        if (side == Side.left) r = new Rect(creditRect.xMax, creditRect.height + iconCredit.height, iconCredit.width, iconCredit.height);
        else r = new Rect(creditRect.xMin - iconCredit.width, creditRect.height + iconCredit.height, iconCredit.width, iconCredit.height);
        GUI.DrawTexture(r, iconCredit);

        //Show the Actions
        for (int i = 0; i < interactiveList.Count; i++)
        {
            GUI.DrawTexture(interactiveRect[i], interactiveList[i].icon);
            GUI.Label(new Rect(
                interactiveRect[i].x + (side == Side.left ? interactiveRect[i].width + 10 : -50),
                interactiveRect[i].y + interactiveRect[i].height / 2,
                50, 50), interactiveList[i].cost.ToString());

            if (interactiveCoolDownTimers[i] > 0)
            {
                interactiveCooldownOverlayRects[i] = calculateCooldownOverlayRect(i);
                GUI.DrawTexture(interactiveCooldownOverlayRects[i], interactiveCooldownOverlayIcon);
            }

            if (drawRedCrossTimers[i] > 0)
            {
                GUI.DrawTexture(interactiveRect[i], interactiveDeniedOverlayIcon);
            }
        }

        //Schow the current Selected Interactive 
        GUI.DrawTexture(interactiveRect[curSelected], iconSelected);
    }

    //Every second x credits:
    public void createCredits()
    {
        if (Time.time > lastTime + 1)
        {
            credits += creditsPerSec;
            lastTime = Time.time;
        }
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

    private void updateRedCrossTimers()
    {
        for (int i = 0; i < drawRedCrossTimers.Length; i++)
        {
            if (drawRedCrossTimers[i] > 0)
            {
                drawRedCrossTimers[i] -= Time.deltaTime;
            }
        }
    }
}
