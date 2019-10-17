using System;

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
        private readonly IBot bot;
        private readonly IBotScheduler botScheduler;
        private BotStatus status = BotStatus.Initializing;

        public BotController(IBotScheduler botScheduler, IBot bot)
        {
            if (botScheduler is null)
            {
                throw new ArgumentNullException(nameof(botScheduler));
            }

            if (bot is null)
            {
                throw new ArgumentNullException(nameof(bot));
            }

            this.bot = bot;
            this.botScheduler = botScheduler;
        }

        public bool IsRunning => status == BotStatus.Running;
        public bool IsWaiting => status == BotStatus.Waiting;
        public bool IsAlive => status != BotStatus.Terminated;
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
    }
}
