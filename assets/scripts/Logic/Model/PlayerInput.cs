using UnityEngine;
using System.Collections.Generic;
using assets.scripts.Miscellaneous;
using System;

public class PlayerInput : MonoBehaviour
{
    public string selectInputName;
    public string actionInputName;

    private Player player;
    private float previousSelectInputAxis;
    private float previousActionInputAxis;

    public event System.Action<Player, float> PlayerActionSelectInput = (player, selectDirection) => {};
    public event System.Action<Player, float> PlayerActionInput = (player, actionDirection) => {};

    private void Awake(){
        player = GetComponent<Player>();
    }

    private void Update(){
        HandleAxis(selectInputName, ref previousSelectInputAxis, PlayerActionSelectInput);
        HandleAxis(actionInputName, ref previousActionInputAxis, PlayerActionInput);
    }

    private void HandleAxis(string axisName, ref float previousAxisValue, Delegate inputEvent)
    {
        float rawAxisValue;
        float axisValue = Utilities.GetAxisRawDown(axisName, previousAxisValue, out rawAxisValue);

        if (axisValue != 0)
        {
            inputEvent.DynamicInvoke(player, axisValue);
        }

        previousAxisValue = rawAxisValue;
    }
}
