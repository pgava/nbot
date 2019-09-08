using System;
using System.Collections.Generic;
using nbot.contracts;

namespace nbot.referee
{
    public class Referee : IReferee
    {
        private IList<IBot> _bots = new List<IBot>();

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

            _bots.Add(b);
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
            return _bots;
        }

        public IEnumerable<IBot> GetRndBots()
        {
            if (_bots.Count < 2)
            {
                return _bots;
            }

            return RandomBotsProvider.RandomizeList(_bots);
        }
    }
}
