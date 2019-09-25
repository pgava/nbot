using System;
using System.Threading;

namespace nbot.contracts
{
    public abstract class Bot : IBot, IActionsProvider
    {
        private IActionsProvider actionsProvider;

        // TODO: make sure only framework call this method
        internal void SetActionsProvider(IActionsProvider actionProvider)
        {
            this.actionsProvider = actionProvider;
        }

        public abstract void PlayTurn();

        public void EndTurn()
        {
            throw new NotImplementedException();
        }

        public void Ahead(double d)
        {
            ThrowIfParameterIsNull(actionsProvider);

            actionsProvider.Ahead(d);
        }

        public void Back(double d)
        {
            ThrowIfParameterIsNull(actionsProvider);

            actionsProvider.Back(d);
        }

        public void Right(double d)
        {
            ThrowIfParameterIsNull(actionsProvider);

            actionsProvider.Right(d);
        }

        public void Left(double d)
        {
            ThrowIfParameterIsNull(actionsProvider);

            actionsProvider.Left(d);
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
