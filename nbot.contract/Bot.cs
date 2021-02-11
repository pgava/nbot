using System;
using nbot.actions;
using nbot.common;

namespace nbot.contract
{
    public abstract class Bot : IBot
    {
        private IMoveActions moveActions;
        private IRocketActions rocketActions;

        internal void SetActions(IMoveActions moveActions, IRocketActions rocketActions)
        {
            Validation.ThrowIfArgumentIsNull(moveActions);
            Validation.ThrowIfArgumentIsNull(rocketActions);            

            this.moveActions = moveActions;
            this.rocketActions = rocketActions;
        }

        public abstract void PlayTurn();

        public void EndTurn()
        {
            throw new NotImplementedException();
        }

        public void Ahead(double d)
        {
            Validation.ThrowIfArgumentIsNull(moveActions);

            moveActions.MoveAhead(d);
        }

        public void Back(double d)
        {
            Validation.ThrowIfArgumentIsNull(moveActions);

            moveActions.MoveBack(d);
        }

        public void Right(double d)
        {
            Validation.ThrowIfArgumentIsNull(moveActions);

            moveActions.TurnRight(d);
        }

        public void Left(double d)
        {
            Validation.ThrowIfArgumentIsNull(moveActions);

            moveActions.TurnLeft(d);
        }

        public void Fire(double d)
        {
            Validation.ThrowIfArgumentIsNull(rocketActions);

            rocketActions.Fire();
        }

    }
}
