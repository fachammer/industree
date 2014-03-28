using UnityEngine;
using System;
using Industree.Logic;
using Industree.Logic.StateManager;
using Industree.Model.Actions;
using Industree.Data.View;
using Industree.View;
using Industree.Graphics;
using Industree.Input.Internal;

namespace Industree.Facade.Internal
{
    internal class Player : MonoBehaviour, IPlayer
    {
        public int index;
        public int initialCredits;
        public int creditsUpInterval;
        public int creditsPerInterval;
        public CreditsViewData creditsViewData;
        public SelectedActionViewData selectedActionViewData;
        public UnityAxis selectAxis;
        public UnityAxis actionAxis;

        private IAction[] actions;
        private SelectionStateManager selectedActionManager;
        private IActionInvoker actionInvoker;
        private CreditsManager creditsManager;
        private PlayerInput playerInput;

        private PlayerView view;

        public event Action<IPlayer, float> ActionInput;
        public event Action<IPlayer, IAction, float> ActionSuccess;
        public event Action<IPlayer, IAction, float> ActionFailure;
        public event Action<int, int> CreditsChange;

        public IAction[] Actions { get { return actions; } }
        public IAction SelectedAction { get { return actions[selectedActionManager.SelectedActionIndex]; } }
        public int Credits { get { return creditsManager.Credits; } }
        public int Index { get { return index; } }

        public Texture CreditsIcon { get { return creditsViewData.Icon; } }
        public Rect CreditsIconBounds { get { return creditsViewData.IconBounds; } }
        public Rect CreditsTextBounds { get { return creditsViewData.TextBounds; } }
        public Texture SelectedOverlayIcon { get { return selectedActionViewData.IconOverlay; } }

        public void SelectNextAction()
        {
            selectedActionManager.SelectNextAction();
        }

        public void SelectPreviousAction()
        {
            selectedActionManager.SelectPreviousAction();
        }

        public void IncreaseCredits(int amount)
        {
            creditsManager.IncreaseCreditsByAmount(amount);
        }

        private void Awake()
        {
            actions = transform.GetComponentsInChildren<Action>();
            Array.Sort<IAction>(actions, (a, b) => a.Index - b.Index);

            selectedActionManager = new SelectionStateManager(actions.Length, 0);
            actionInvoker = new ActionInvoker();
            creditsManager = new CreditsManager(initialCredits);
            playerInput = new PlayerInput(this, selectAxis, actionAxis);

            playerInput.PlayerActionSelectInput += OnPlayerActionSelectInput;
            playerInput.PlayerActionInput += OnPlayerActionInput;

            view = new PlayerView(this, GuiRendererFactory.GetResolutionIndependentRenderer(), creditsViewData.ViewSkin);
        }

        private void OnPlayerActionSelectInput(IPlayer player, float selectDirection)
        {
            if (selectDirection < 0)
            {
                selectedActionManager.SelectNextAction();
            }
            else
            {
                selectedActionManager.SelectPreviousAction();
            }
        }

        private void OnPlayerActionInput(IPlayer player, float actionDirection)
        {
            actionInvoker.Invoke(player, SelectedAction, actionDirection);
        }

        private void OnGUI()
        {
            view.Draw();
        }
    }
}
