using System;
using System.Collections.Generic;
using UnityEngine;  

namespace assets.scripts.View
{
    public class SelectActionView : MonoBehaviour
    {
        private UnityInputInterface unityInputInterface;

        public event System.Action<Player, float> ActionSelectInput = (Player player, float selectDirection) => { };

        private void Awake()
        {
            unityInputInterface = UnityInputInterface.Get();
            unityInputInterface.PlayerActionSelectInput += OnPlayerActionSelectInput;
        }

        private void OnPlayerActionSelectInput(Player player, float selectDirection)
        {
            ActionSelectInput(player, selectDirection);
        }

        private void OnGUI()
        {
            // Draw currently selected Actions
        }
    }
}
