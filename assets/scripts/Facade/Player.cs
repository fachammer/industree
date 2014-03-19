using UnityEngine;
using System;
using Industree.Logic;
using Industree.Model.Actions;

namespace Industree.Facade.Internal
{
    public class Player : MonoBehaviour, IPlayer
    {
        public int index;
        public int initialCredits;
        public int creditsUpInterval;
        public int creditsPerInterval;

        private IAction[] actions;

        public IAction[] Actions { get { return actions; } }

        public IAction SelectedAction { get { return actions[selectedActionController.SelectedActionIndex]; } }

        public IActionInvoker ActionInvoker { get { return actionInvoker; } }

        public int Credits { get { return creditsManager.Credits; } }

        public int Index { get { return index; } }

        private SelectionStateManager selectedActionController;
        private IActionInvoker actionInvoker;
        private CreditsManager creditsManager;

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

        public static Player[] GetAll()
        {
            Player[] players = Array.ConvertAll(GameObject.FindGameObjectsWithTag(Tags.player), (gameObject) => gameObject.GetComponent<Player>());
            Array.Sort<Player>(players, (p1, p2) => p1.index - p2.index);
            return players;
        }

        public void IncreaseCredits(int amount)
        {
            creditsManager.IncreaseCreditsByAmount(amount);
        }

        public event Action<IPlayer, float> ActionInput;
        public event Action<IPlayer, IAction, float> ActionSuccess;
    }
}
