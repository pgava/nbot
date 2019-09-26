using System;

namespace nbot.contracts
{
    public class ActionsProvider : IActionsProvider
    {
        private readonly IPositionProvider positionProvider;

        public ActionsProvider(IPositionProvider positionProvider)
        {
            if (positionProvider is null)
            {
                throw new ArgumentNullException(nameof(positionProvider));
            }

            this.positionProvider = positionProvider;
        }
        public void Ahead(double d)
        {
            positionProvider.SetMoveAhead(d);
        }
        public void Back(double d)
        {
            positionProvider.SetMoveBack(d);
        }
        public void Right(double d)
        {
            positionProvider.SetMoveRight(d);
        }
        public void Left(double d)
        {
            positionProvider.SetMoveLeft(d);
        }

        
    }
}