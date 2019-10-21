using System;
using nbot.actions;

namespace nbot.contract
{
    public abstract class Bot : IBot
    {
        private IMoveActions moveActions;

        internal void SetActions(IMoveActions moveActions)
        {
            this.moveActions = moveActions;
        }

        public abstract void PlayTurn();

        public void EndTurn()
        {
            throw new NotImplementedException();
        }

        public void Ahead(double d)
        {
            ThrowIfParameterIsNull(moveActions);

            moveActions.SetMoveAhead(d);
        }

        public void Back(double d)
        {
            ThrowIfParameterIsNull(moveActions);

            moveActions.SetMoveBack(d);
        }

        public void Right(double d)
        {
            ThrowIfParameterIsNull(moveActions);

            moveActions.SetMoveRight(d);
        }

        public void Left(double d)
        {
            ThrowIfParameterIsNull(moveActions);

            moveActions.SetMoveLeft(d);
        }

        private void ThrowIfParameterIsNull<T>(T parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
