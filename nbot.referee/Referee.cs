using System;
using System.Collections.Generic;
using nbot.contracts;

namespace nbot.referee
{
    public class Referee : IReferee
    {
        private IList<INBot> _bots = new List<INBot>();

        private IRandomBotsProvider RandomBotsProvider { get; }


        public Referee(IRandomBotsProvider randomBotsProvider)
        {
            if (randomBotsProvider == null)
            {
                throw new ArgumentNullException();
            }

            RandomBotsProvider = randomBotsProvider;
        }
        public void AddBot(INBot b)
        {
            if (b == null)
            {
                throw new ArgumentNullException();
            }

            _bots.Add(b);
        }

        public void AddBots(List<INBot> bots)
        {
            if (bots == null)
            {
                throw new ArgumentNullException();
            }

            bots.ForEach(b => AddBot(b));
        }

        public IEnumerable<INBot> GetBots()
        {
            return _bots;
        }

        public IEnumerable<INBot> GetRndBots()
        {
            if (_bots.Count < 2)
            {
                return _bots;
            }

            return RandomBotsProvider.RandomizeList(_bots);
        }
    }
}
