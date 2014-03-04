using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionInterface : MonoBehaviour
{
    private KeyboardInterface keyboardInterface;
    private ButtonInterface buttonInterface;

    public delegate void PlayerActionHandler(Player player, Action action, float actionDirection);
    public event PlayerActionHandler PlayerAction = delegate(Player player, Action action, float actionDirection) { };
}

