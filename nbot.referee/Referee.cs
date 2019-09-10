using System;
using System.Collections.Generic;
using nbot.contracts;

namespace nbot.referee
{
    public class Referee : IReferee
    {
        private IList<IBot> bots = new List<IBot>();

        private IRandomBotsProvider RandomBotsProvider { get; }


        public Referee(IRandomBotsProvider randomBotsProvider)
        {
            if (randomBotsProvider == null)
            {
                throw new ArgumentNullException();
            }

            RandomBotsProvider = randomBotsProvider;
        }
        public void AddBot(IBot b)
        {
            if (b == null)
            {
                throw new ArgumentNullException();
            }

            bots.Add(b);
        }

        public void AddBots(List<IBot> bots)
        {
            if (bots == null)
            {
                throw new ArgumentNullException();
            }

            bots.ForEach(b => AddBot(b));
        }

        public IEnumerable<IBot> GetBots()
        {
            return bots;
        }

        public IEnumerable<IBot> GetRndBots()
        {
            if (bots.Count < 2)
            {
                return bots;
            }

            return RandomBotsProvider.RandomizeList(bots);
        }
    }
}
