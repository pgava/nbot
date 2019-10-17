using System;

namespace nbot.actions
{
    public class Actions : IActions
    {
        private readonly IBotPosition position;

        public Actions(IBotPosition position)
        {
            if (position is null)
            {
                throw new ArgumentNullException(nameof(position));
            }

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