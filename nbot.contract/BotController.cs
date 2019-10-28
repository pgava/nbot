using System;
using nbot.actions;

namespace nbot.contract
{
    public enum BotStatus
    {
        Initializing,
        Waiting,
        Running,
        Terminated
    }

    public class BotController : IBotController
    {
        private readonly Bot bot;
        private readonly IBotScheduler botScheduler;
        private readonly IMoveActionsController moveController;
        private readonly IRocketActionsController rocketController;
        private BotStatus status = BotStatus.Initializing;

        public BotController(Bot bot, IBotScheduler botScheduler, IMoveActionsController moveController, IRocketActionsController rocketController)
        {
            if (bot is null)
            {
                throw new ArgumentNullException(nameof(bot));
            }

            if (botScheduler is null)
            {
                throw new ArgumentNullException(nameof(botScheduler));
            }

            if (moveController is null)
            {
                throw new ArgumentNullException(nameof(moveController));
            }

            if (rocketController is null)
            {
                throw new ArgumentNullException(nameof(rocketController));
            }

            this.bot = bot;
            this.botScheduler = botScheduler;
            this.moveController = moveController;
            this.rocketController = rocketController;

            this.bot.SetActions(this.moveController, this.rocketController);
        }

        public bool IsRunning => status == BotStatus.Running;
        public bool IsWaiting => status == BotStatus.Waiting;
        public bool IsAlive => status != BotStatus.Terminated;

        bool IBotController.IsRunning => throw new NotImplementedException();

        bool IBotController.IsWaiting => throw new NotImplementedException();

        bool IBotController.IsAlive => throw new NotImplementedException();

        public void Turn()
        {
            status = BotStatus.Running;

            while (true)
            {
                bot.PlayTurn();

                WaitForNextTurn();
            }
        }

        private void WaitForNextTurn()
        {
            status = BotStatus.Waiting;
            botScheduler.WaitForNextTurn();
        }

        public void Wakeup()
        {
            status = BotStatus.Running;
            botScheduler.Wakeup();
        }

        public Point CalculateNextPosition()
        {
            moveController.CalculateNextPosition();

            // TODO: define structure for rockets
            rocketController.CalculateTrajectories(moveController.Position);

            return moveController.Position.Point();
        }
    }
}
