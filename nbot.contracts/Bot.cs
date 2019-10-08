using System;
using System.Threading;

namespace nbot.contracts
{
    public abstract class Bot : IBot, IActions
    {
        private IActions actions;

        // TODO: make sure only framework call this method
        internal void SetActions(IActions actions)
        {
            this.actions = actions;
        }

        public abstract void PlayTurn();

        public void EndTurn()
        {
            throw new NotImplementedException();
        }

        public void Ahead(double d)
        {
            ThrowIfParameterIsNull(actions);

            actions.Ahead(d);
        }

        public void Back(double d)
        {
            ThrowIfParameterIsNull(actions);

            actions.Back(d);
        }

        public void Right(double d)
        {
            ThrowIfParameterIsNull(actions);

            actions.Right(d);
        }

        public void Left(double d)
        {
            ThrowIfParameterIsNull(actions);

            actions.Left(d);
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
