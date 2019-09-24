using System;
using System.Threading;

namespace nbot.contracts
{
    public abstract class Bot : IBot, IActions
    {
        private IActions actionsProvider;

        // TODO: make sure only framework call this method
        internal void SetActionsProvider(IActions actionProvider)
        {
            this.actionsProvider = actionProvider;
        }

        public abstract void PlayTurn();

        public void EndTurn()
        {
            throw new NotImplementedException();
        }

        public void Ahead(decimal d)
        {
            ThrowIfParameterIsNull(actionsProvider);

            actionsProvider.Ahead(d);
        }

        public void Back(decimal d)
        {
            ThrowIfParameterIsNull(actionsProvider);

            actionsProvider.Back(d);
        }

        public void Right(decimal d)
        {
            ThrowIfParameterIsNull(actionsProvider);

            actionsProvider.Right(d);
        }

        public void Left(decimal d)
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
