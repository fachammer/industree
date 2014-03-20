using UnityEngine;
using System;
using Industree.Logic;
using Industree.Logic.StateManager;
using Industree.Model.Actions;

namespace Industree.Facade.Internal
{
    internal class Player : MonoBehaviour, IPlayer
    {
        public int index;
        public int initialCredits;
        public int creditsUpInterval;
        public int creditsPerInterval;

        private IAction[] actions;
        private SelectionStateManager selectedActionController;
        private IActionInvoker actionInvoker;
        private CreditsManager creditsManager;

        public event Action<IPlayer, float> ActionInput;
        public event Action<IPlayer, IAction, float> ActionSuccess;
        public event Action<IPlayer, IAction, float> ActionFailure;
        public event Action<int, int> CreditsChange;

        public IAction[] Actions { get { return actions; } }
        public IAction SelectedAction { get { return actions[selectedActionController.SelectedActionIndex]; } }
        public int Credits { get { return creditsManager.Credits; } }
        public int Index { get { return index; } }

        private void Awake()
        {
            actions = transform.GetComponentsInChildren<Action>();
            Array.Sort<IAction>(actions, (a, b) => a.Index - b.Index);

            selectedActionController = new SelectionStateManager(actions.Length, 0);
            actionInvoker = new ActionInvoker();
            creditsManager = new CreditsManager(initialCredits);
        }

        public void SelectNextAction()
        {
            selectedActionController.SelectNextAction();
        }

        public void SelectPreviousAction()
        {
            selectedActionController.SelectPreviousAction();
        }

        public void IncreaseCredits(int amount)
        {
            creditsManager.IncreaseCreditsByAmount(amount);
        }
    }
}
