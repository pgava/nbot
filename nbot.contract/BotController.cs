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
        private readonly IActions actions;
        private readonly IBotPosition position;
        private BotStatus status = BotStatus.Initializing;

        public BotController(Bot bot, IBotScheduler botScheduler, IActions actions, IBotPosition position)
        {
            if (bot is null)
            {
                throw new ArgumentNullException(nameof(bot));
            }

            if (botScheduler is null)
            {
                throw new ArgumentNullException(nameof(botScheduler));
            }

            if (actions is null)
            {
                throw new ArgumentNullException(nameof(actions));
            }

            if (position is null)
            {
                throw new ArgumentNullException(nameof(position));
            }

            this.bot = bot;
            this.botScheduler = botScheduler;
            this.actions = actions;
            this.position = position;

            this.actions.SetPosition(position);
            this.bot.SetActions(this.actions);
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
            position.CalculateNextPosition();

            return position.Position;
        }
    }
}
