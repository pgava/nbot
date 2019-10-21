using System;

namespace nbot.actions
{
    public class Actions : IMoveActions
    {
        private IBotPosition position;

        public void SetPosition(IBotPosition position)
        {
            this.position = position;
        }

        public void Ahead(double d)
        {
            position.SetMoveAhead(d);
        }

        public void Back(double d)
        {
            position.SetMoveBack(d);
        }

        public void Right(double d)
        {
            position.SetMoveRight(d);
        }

        public void Left(double d)
        {
            position.SetMoveLeft(d);
        }


    }
}