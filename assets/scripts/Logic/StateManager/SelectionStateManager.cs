using System;

namespace Industree.Logic.StateManager
{
    public class SelectionStateManager
    {
        public int NumberOfActions  {get; private set; }
        public int SelectedActionIndex { get; private set; }

        public SelectionStateManager(int numberOfActions, int selectedActionIndex = 0)
        {
            if (numberOfActions <= 0)
            {
                throw new ArgumentException("number of actions must be at least 1");
            }

            this.NumberOfActions = numberOfActions;
            this.SelectedActionIndex = selectedActionIndex;
        }

        public void SelectNextAction()
        {
            if (SelectedActionIndex == NumberOfActions - 1)
            {
                SelectedActionIndex = 0;
            }
            else
            {
                SelectedActionIndex++;
            }
        }

        public void SelectPreviousAction()
        {
            if (SelectedActionIndex == 0)
            {
                SelectedActionIndex = NumberOfActions - 1;
            }
            else
            {
                SelectedActionIndex--;
            }
        }
    }
}
