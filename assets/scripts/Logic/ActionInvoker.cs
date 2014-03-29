using Industree.Facade;

namespace Industree.Logic
{
    public class ActionInvoker : IActionInvoker
    {
        public event System.Action<IPlayer, IAction, float> ActionSuccess = (player, action, actionDirection) => { };
        public event System.Action<IPlayer, IAction, float> ActionFailure = (player, action, actionDirection) => { };

        public void Invoke(IPlayer player, IAction action, float direction)
        {
            if (CanActionSucceed(player, action, direction)){
                action.Invoke(player, direction);
                player.DecreaseCredits(action.Cost);
                ActionSuccess(player, action, direction);
            }
            else
            {
                action.Fail(player, direction);
                ActionFailure(player, action, direction);
            }
        }

        private bool CanActionSucceed(IPlayer player, IAction action, float direction)
        {
            return player.Credits >= action.Cost && action.IsInvokable(player, direction) && !action.IsCoolingDown;
        }
    }
}
