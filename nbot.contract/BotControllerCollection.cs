using System;
using System.Collections.Generic;

namespace nbot.contract
{
    public class BotControllerCollection : IBotControllerCollection
    {
        private IList<IBotController> bots = new List<IBotController>();

        private IRandomBots RandomBotsProvider { get; }

        public BotControllerCollection(IRandomBots randomBotsProvider)
        {
            if (randomBotsProvider == null)
            {
                throw new ArgumentNullException();
            }

            RandomBotsProvider = randomBotsProvider;
        }

        public void AddBot(IBotController b)
        {
            if (b == null)
            {
                throw new ArgumentNullException();
            }

            bots.Add(b);
        }

        public void AddBots(List<IBotController> bots)
        {
            if (bots == null)
            {
                throw new ArgumentNullException();
            }

            bots.ForEach(b => AddBot(b));
        }

        public IEnumerable<IBotController> GetBots()
        {
            return bots;
        }

        public IEnumerable<IBotController> GetRndBots()
        {
            if (bots.Count < 2)
            {
                return bots;
            }

            return RandomBotsProvider.RandomizeList(bots);
        }

    }
}