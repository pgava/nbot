using System;
using nbot.actions;

namespace nbot.contract
{
    public abstract class Bot : IBot, IActions
    {
        private IActions actions;

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
